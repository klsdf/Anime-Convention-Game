using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 墙面类 - 增强版支持网格自动对齐
/// </summary>
public class Wall: MonoBehaviour
{
    [Header("墙面设置")]
    public Vector2 wallSize = new Vector2(5, 3);  // 墙面尺寸(宽, 高)
    public bool useGridSnapping = true;           // 是否使用网格吸附
    public float gridSize = 0.25f;                // 网格大小
    
    [Header("网格设置")]
    public Transform wallGrid;                    // 墙面网格对象引用
    
    /// <summary>
    /// 对齐网格到墙面
    /// </summary>
    public void AlignGridToWall()
    {
        if (wallGrid == null)
        {
            Debug.LogError("请先分配网格对象！");
            return;
        }
        
        // 设置网格位置与墙面位置相同
        wallGrid.position = transform.position;
        
        // 设置网格旋转与墙面旋转相同
        wallGrid.rotation = transform.rotation;
        
        // 设置网格比例以匹配墙面尺寸
        wallGrid.localScale = new Vector3(wallSize.x, wallSize.y, 1f);
        
        Debug.Log($"已将网格 {wallGrid.name} 对齐到墙面 {name}");
    }
    
    /// <summary>
    /// 验证墙面网格是否与墙面重叠
    /// </summary>
    public bool ValidateWallGrid()
    {
        if (wallGrid == null)
        {
            Debug.LogError($"墙面 {name} 没有分配网格对象！");
            return false;
        }
        
        // 检查网格位置是否与墙面位置对齐
        float positionDifference = Vector3.Distance(wallGrid.position, transform.position);
        if (positionDifference > 0.01f)
        {
            Debug.LogError($"墙面 {name} 的网格位置与墙面位置不一致！差距: {positionDifference}");
            return false;
        }
        
        // 检查网格旋转是否与墙面旋转对齐
        float rotationDifference = Quaternion.Angle(wallGrid.rotation, transform.rotation);
        if (rotationDifference > 0.1f)
        {
            Debug.LogError($"墙面 {name} 的网格旋转与墙面旋转不一致！角度差: {rotationDifference}");
            return false;
        }
        
        // 检查网格比例是否与墙面尺寸匹配
        if (Mathf.Abs(wallGrid.localScale.x - wallSize.x) > 0.01f ||
            Mathf.Abs(wallGrid.localScale.y - wallSize.y) > 0.01f)
        {
            Debug.LogError($"墙面 {name} 的网格比例与墙面尺寸不匹配！");
            return false;
        }
        
        return true;
    }
    
    void Awake()
    {
        // 在运行时检查网格是否与墙面重叠
        if (!ValidateWallGrid())
        {   
            // 自动尝试对齐网格
            if (wallGrid != null)
            {
                AlignGridToWall();
                Debug.Log("已自动对齐网格到墙面");
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // 绘制墙面边界
        Gizmos.color = Color.cyan;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(wallSize.x, wallSize.y, 0.1f));
        
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
        
        // 检查并可视化网格是否与墙面重叠
        if (wallGrid != null)
        {
            bool isValid = ValidateWallGrid();
            Gizmos.color = isValid ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
            
            // 绘制网格与墙面之间的连线
            Gizmos.DrawLine(transform.position, wallGrid.position);
        }
    }
}

#if UNITY_EDITOR
/// <summary>
/// 墙面类的自定义编辑器
/// </summary>
[CustomEditor(typeof(Wall))]
public class WallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Wall wall = (Wall)target;
        
        // 绘制默认检查器
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        // 添加对齐网格按钮
        if (GUILayout.Button("对齐网格到墙面", GUILayout.Height(30)))
        {
            wall.AlignGridToWall();
            // 标记为已修改，确保保存场景时保存更改
            EditorUtility.SetDirty(wall.wallGrid);
            EditorUtility.SetDirty(wall);
        }
        
        // 检查网格是否对齐
        bool isAligned = wall.ValidateWallGrid();
        
        EditorGUILayout.Space();
        if (isAligned)
        {
            EditorGUILayout.HelpBox("网格已正确对齐到墙面。", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("网格未正确对齐到墙面！请点击上方按钮进行对齐。", MessageType.Warning);
        }
    }
}
#endif