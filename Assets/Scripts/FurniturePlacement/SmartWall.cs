using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 墙面类 - 增强版支持网格自动对齐
/// </summary>
public class SmartWall : MonoBehaviour
{
    [Header("自动检测设置")]
    [SerializeField] private bool autoDetectWallFace = true;
    [SerializeField] private float wallThickness = 0.1f;
    
    [Header("墙面设置")]
    public Vector2 wallSize = new Vector2(5, 3);  // 墙面尺寸(宽, 高)
    public bool useGridSnapping = true;           // 是否使用网格吸附
    public float gridSize = 0.5f;                // 网格大小
    
    [Header("网格引用")]
    public Transform wallGrid;                    // 墙面网格对象引用
    
    [Header("状态")]
    [SerializeField] private Vector3 wallNormal;  // 墙面法线
    [SerializeField] private bool hasDetectedWall = false;
    
    void OnEnable()
    {
        if (autoDetectWallFace && !hasDetectedWall)
        {
            DetectWallFace();
        }

    }
    
    /// <summary>
    /// 自动检测墙面的最大面并设置朝向
    /// </summary>
    public void DetectWallFace()
    {
        // 1. 获取模型的所有网格
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError($"物体 {name} 没有MeshFilter组件或Mesh为空!");
            return;
        }
        
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        
        // 2. 找出最大面的法线
        Vector3 maxFaceNormal = Vector3.zero;
        float maxFaceArea = 0f;
        
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // 获取三角形的顶点
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];
            
            // 计算三角形面积
            Vector3 side1 = v2 - v1;
            Vector3 side2 = v3 - v1;
            Vector3 normal = Vector3.Cross(side1, side2);
            float area = normal.magnitude * 0.5f;
            
            // 标准化法线
            normal.Normalize();
            
            // 寻找相同方向的所有三角形，累计面积
            float totalFaceArea = 0f;
            for (int j = 0; j < triangles.Length; j += 3)
            {
                Vector3 vj1 = vertices[triangles[j]];
                Vector3 vj2 = vertices[triangles[j + 1]];
                Vector3 vj3 = vertices[triangles[j + 2]];
                
                Vector3 normalJ = Vector3.Cross(vj2 - vj1, vj3 - vj1).normalized;
                
                // 如果法线方向相近，累计面积
                if (Vector3.Dot(normal, normalJ) > 0.9f)
                {
                    float areaJ = Vector3.Cross(vj2 - vj1, vj3 - vj1).magnitude * 0.5f;
                    totalFaceArea += areaJ;
                }
            }
            
            // 更新最大面
            if (totalFaceArea > maxFaceArea)
            {
                maxFaceArea = totalFaceArea;
                maxFaceNormal = normal;
            }
        }
        
        // 3. 计算墙面尺寸
        CalculateWallSize(mesh, maxFaceNormal);
        
        // 4. 设置墙面法线
        wallNormal = transform.TransformDirection(maxFaceNormal);
        
        // 5. 创建或更新墙面网格
        SetupWallGrid();
        
        hasDetectedWall = true;
        Debug.Log($"墙面 {name} 已自动检测: 法线={wallNormal}, 尺寸={wallSize}");
    }
    
    /// <summary>
    /// 计算墙面尺寸
    /// </summary>
    private void CalculateWallSize(Mesh mesh, Vector3 faceNormal)
    {
        Vector3[] vertices = mesh.vertices;
        
        // 找到与法线垂直的两个轴
        Vector3 right = Vector3.zero;
        if (Mathf.Abs(faceNormal.y) > 0.9f)
        {
            // 如果法线接近上方或下方，使用X轴作为右方向
            right = new Vector3(1, 0, 0);
        }
        else
        {
            // 否则使用上方向与法线的叉积作为右方向
            right = Vector3.Cross(Vector3.up, faceNormal).normalized;
        }
        
        Vector3 up = Vector3.Cross(faceNormal, right).normalized;
        
        // 投影所有顶点到这两个轴，找出最大范围
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        
        foreach (Vector3 vertex in vertices)
        {
            // 计算顶点在平面上的投影
            float projRight = Vector3.Dot(vertex, right);
            float projUp = Vector3.Dot(vertex, up);
            
            minX = Mathf.Min(minX, projRight);
            maxX = Mathf.Max(maxX, projRight);
            minY = Mathf.Min(minY, projUp);
            maxY = Mathf.Max(maxY, projUp);
        }
        
        // 设置墙面尺寸
        wallSize = new Vector2( maxY - minY, maxX - minX);
    }
    
    /// <summary>
    /// 设置墙面网格
    /// </summary>
    private void SetupWallGrid()
    {
        // 如果没有网格引用，创建一个新的网格对象
        if (wallGrid == null)
        {
            GameObject gridObj = new GameObject(name + "_Grid");
            wallGrid = gridObj.transform;
            wallGrid.SetParent(transform);
        }
        
        // 找到面的中心点
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 center = bounds.center;
            
            // 沿法线方向偏移一点，避免Z冲突
            Vector3 offset = wallNormal * wallThickness * 0.5f;
            wallGrid.position = center + offset;
            
            // 设置网格朝向与墙面法线一致
            wallGrid.rotation = Quaternion.LookRotation(wallNormal);
            
            // 设置网格尺寸
            wallGrid.localScale = new Vector3(wallSize.x, wallSize.y, 0.01f);
        }
    }
    
    /// <summary>
    /// 验证墙面网格是否正确设置
    /// </summary>
    public bool ValidateWallGrid()
    {
        if (wallGrid == null)
        {
            Debug.LogError($"墙面 {name} 没有分配网格对象！");
            return false;
        }
        
        // 检查网格朝向是否与墙面法线一致
        float directionDifference = Vector3.Angle(wallGrid.forward, wallNormal);
        if (directionDifference > 5f)
        {
            Debug.LogError($"墙面 {name} 的网格朝向与墙面法线不一致！角度差: {directionDifference}");
            return false;
        }
        
        return true;
    }
    
    void OnDrawGizmosSelected()
    {
        // 如果已经检测到墙面，绘制墙面法线
        if (hasDetectedWall)
        {
            Gizmos.color = Color.blue;
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 center = renderer.bounds.center;
                Gizmos.DrawLine(center, center + wallNormal * 1f);
                
                // 绘制墙面范围
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(
                    center + wallNormal * wallThickness * 0.5f, 
                    Quaternion.LookRotation(wallNormal),
                    Vector3.one
                );
                Gizmos.matrix = rotationMatrix;
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(wallSize.x, wallSize.y, 0.01f));
                
                // 如果使用网格，绘制网格线
                if (useGridSnapping)
                {
                    Gizmos.color = new Color(0, 1, 1, 0.3f);
                    for (float x = -wallSize.x/2; x <= wallSize.x/2; x += gridSize)
                    {
                        Gizmos.DrawLine(
                            new Vector3(x, -wallSize.y/2, 0),
                            new Vector3(x, wallSize.y/2, 0)
                        );
                    }
                    
                    for (float y = -wallSize.y/2; y <= wallSize.y/2; y += gridSize)
                    {
                        Gizmos.DrawLine(
                            new Vector3(-wallSize.x/2, y, 0),
                            new Vector3(wallSize.x/2, y, 0)
                        );
                    }
                }
                
                // 恢复默认矩阵
                Gizmos.matrix = Matrix4x4.identity;
            }
        }
        
        // 检查并可视化网格
        if (wallGrid != null)
        {
            bool isValid = ValidateWallGrid();
            Gizmos.color = isValid ? Color.green : Color.red;
            Gizmos.DrawSphere(wallGrid.position, 0.05f);
        }
    }
    
    /// <summary>
    /// 获取网格世界坐标中的最近网格点
    /// </summary>
    public Vector3 GetNearestGridPoint(Vector3 worldPosition)
    {
        if (wallGrid == null) return worldPosition;
        
        // 将世界坐标转换为网格的局部坐标
        Vector3 localPos = wallGrid.InverseTransformPoint(worldPosition);
        
        // 将局部坐标四舍五入到最近的网格点
        localPos.x = Mathf.Round(localPos.x / gridSize) * gridSize;
        localPos.y = Mathf.Round(localPos.y / gridSize) * gridSize;
        localPos.z = 0; // 确保位于网格平面上
        
        // 将网格局部坐标转换回世界坐标
        return wallGrid.TransformPoint(localPos);
    }
}

#if UNITY_EDITOR
/// <summary>
/// 智能墙面类的自定义编辑器
/// </summary>
[CustomEditor(typeof(SmartWall))]
public class SmartWallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SmartWall wall = (SmartWall)target;
        
        // 绘制默认检查器
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        // 添加检测墙面按钮
        if (GUILayout.Button("检测墙面并设置网格", GUILayout.Height(30)))
        {
            wall.DetectWallFace();
            // 标记为已修改，确保保存场景时保存更改
            EditorUtility.SetDirty(wall);
            if (wall.wallGrid != null)
                EditorUtility.SetDirty(wall.wallGrid.gameObject);
        }
        
        // 检查网格是否正确设置
        bool isValid = wall.ValidateWallGrid();
        
        EditorGUILayout.Space();
        if (isValid)
        {
            EditorGUILayout.HelpBox("墙面网格已正确设置。", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("墙面网格未正确设置！请点击上方按钮进行检测和设置。", MessageType.Warning);
        }
    }
}
#endif