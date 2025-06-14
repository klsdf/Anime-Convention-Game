using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作者：龚科翰
/// 日期：2025-05-06
/// 功能：墙面家具放置系统
/// </summary>
public class WallFurniturePlacementSystem : MonoBehaviour
{
   public LayerMask wallLayer;
    public float detectionDistance = 2f;
    public bool drawDebugRays = true;
    public bool diagnoseOnStart = true;
    
    void Start()
    {
        if (diagnoseOnStart)
        {
            DiagnoseAllWallFurniture();
        }
    }
    
    [ContextMenu("诊断所有墙面家具")]
    public void DiagnoseAllWallFurniture()
    {
        FurnitureInstance[] allFurniture = FindObjectsOfType<FurnitureInstance>();
        int wallFurnitureCount = 0;
        int detectedWallCount = 0;
        
        Debug.Log($"<color=cyan>开始诊断所有墙面家具...</color>");
        
        foreach (FurnitureInstance furniture in allFurniture)
        {
            if (furniture.data != null && furniture.data.type == FurnitureType.Wall)
            {
                wallFurnitureCount++;
                DiagnoseFurniture(furniture);
                
                if (FindWallForFurniture(furniture, false))
                {
                    detectedWallCount++;
                }
            }
        }
        
        Debug.Log($"<color=cyan>诊断完成：找到{wallFurnitureCount}个墙面家具，其中{detectedWallCount}个成功检测到墙面</color>");
    }
    
    /// <summary>
    /// 诊断单个家具
    /// </summary>
    public void DiagnoseFurniture(FurnitureInstance furniture)
    {
        if (furniture == null || furniture.data == null)
        {
            Debug.LogError("家具为空或缺少数据！");
            return;
        }
        
        Debug.Log($"检查家具: {furniture.name} ({furniture.data.displayName})");
        Debug.Log($"  - 类型: {furniture.data.type}");
        Debug.Log($"  - 位置: {furniture.transform.position}");
        Debug.Log($"  - 旋转: {furniture.transform.eulerAngles}");
        Debug.Log($"  - 放置层: {LayerMaskToString(furniture.data.placementLayer)}");
        
        // 检查与墙面的关系
        if (furniture.data.type == FurnitureType.Wall)
        {
            FindWallForFurniture(furniture, true);
        }
    }
    
    /// <summary>
    /// 尝试为家具找到最近的墙面
    /// </summary>
    bool FindWallForFurniture(FurnitureInstance furniture, bool logDetails)
    {
        if (furniture == null) return false;
        
        // 向六个方向发射射线
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward, Vector3.back,
            Vector3.right, Vector3.left,
            Vector3.up, Vector3.down
        };
        
        string[] dirNames = new string[]
        {
            "前", "后", "右", "左", "上", "下"
        };
        
        bool foundWall = false;
        Transform nearestWall = null;
        Vector3 wallNormal = Vector3.zero;
        float minDistance = float.MaxValue;
        int hitDirection = -1;
        
        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 dir = directions[i];
            
            // 绘制调试射线
            if (drawDebugRays)
            {
                Debug.DrawRay(furniture.transform.position, dir * detectionDistance, Color.yellow, 5f);
            }
            
            if (Physics.Raycast(furniture.transform.position, dir, out RaycastHit hit, detectionDistance, wallLayer))
            {
                if (logDetails)
                {
                    Debug.Log($"  - 方向({dirNames[i]})：<color=green>检测到墙面</color> {hit.transform.name}，距离: {hit.distance}，法线: {hit.normal}");
                }
                
                if (hit.distance < minDistance)
                {
                    minDistance = hit.distance;
                    nearestWall = hit.transform;
                    wallNormal = hit.normal;
                    hitDirection = i;
                    foundWall = true;
                }
            }
            else if (logDetails)
            {
                Debug.Log($"  - 方向({dirNames[i]})：<color=red>未检测到墙面</color>");
            }
        }
        
        if (foundWall && logDetails)
        {
            Debug.Log($"  - <color=green>最近墙面</color>: {nearestWall.name}，方向: {dirNames[hitDirection]}，距离: {minDistance}，法线: {wallNormal}");
        }
        else if (logDetails)
        {
            Debug.Log($"  - <color=red>未找到任何墙面！</color> 请检查墙面层级设置和家具位置");
        }
        
        return foundWall;
    }
    
    /// <summary>
    /// 转换LayerMask为可读字符串
    /// </summary>
    string LayerMaskToString(LayerMask mask)
    {
        string result = "";
        for (int i = 0; i < 32; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                if (result != "")
                    result += ", ";
                result += LayerMask.LayerToName(i);
            }
        }
        return result == "" ? "无" : result;
    }
    
    void OnDrawGizmos()
    {
        if (!Application.isPlaying || !drawDebugRays) return;
        
        // 查找所有墙面家具并绘制检测线
        FurnitureInstance[] allFurniture = FindObjectsOfType<FurnitureInstance>();
        
        foreach (FurnitureInstance furniture in allFurniture)
        {
            if (furniture.data != null && furniture.data.type == FurnitureType.Wall)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(furniture.transform.position, 0.1f);
                
                // 绘制六个方向的检测线
                Vector3[] directions = new Vector3[]
                {
                    Vector3.forward, Vector3.back,
                    Vector3.right, Vector3.left,
                    Vector3.up, Vector3.down
                };
                
                foreach (Vector3 dir in directions)
                {
                    if (Physics.Raycast(furniture.transform.position, dir, out RaycastHit hit, detectionDistance, wallLayer))
                    {
                        // 命中墙面，绘制绿色线
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(furniture.transform.position, hit.point);
                        Gizmos.DrawSphere(hit.point, 0.05f);
                        
                        // 绘制法线方向
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(hit.point, hit.point + hit.normal * 0.2f);
                    }
                    else
                    {
                        // 未命中墙面，绘制红色线
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(furniture.transform.position, furniture.transform.position + dir * detectionDistance);
                    }
                }
            }
        }
    }
}

