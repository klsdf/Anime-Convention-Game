using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 作者：龚科翰
/// 日期：2025-03-31
/// 功能：家具放置系统
/// </summary>
public class FloorFurniturePlacementSystem : MonoBehaviour
{
    [Header("核心设置")]
    public Camera mainCamera;
    public Grid floorGrid; // 地板网格
    public LayerMask floorLayer; // 地板层级
    public LayerMask furnitureLayer; // 家具层级

    [Header("边界设置")]
    public Vector2Int floorSize = new Vector2Int(10, 10); // 地板网格尺寸(x,z)

    [Header("运行时状态")]
    
    [SerializeField] private Vector3Int lastCellPos;
    [SerializeField] private bool isValidPosition;
    private FurnitureInstance selectedFurniture;

    [Header("碰撞检测设置")]       //1.2版本新增
    public float overlapTestHeight = 1f; // 检测高度范围
    public float overlapTestInset = 0.05f; // 边界内缩量防止边缘粘连

    [Header("位置调整设置")]       //1.2版本新增
    public float adjustmentStep = 0.5f; // 每次调整的步长
    public int maxAdjustmentAttempts = 8; // 最大尝试次数
    public float searchRadius = 2f; // 搜索半径 

    void Update()
    {
       HandleSelection();
        // 只在按住鼠标时处理移动
        if (Input.GetMouseButton(0) && selectedFurniture != null)
        {
            HandleMovement();
        }
        // 鼠标松开时放置家具
        else if (selectedFurniture != null && !Input.GetMouseButton(0))
        {
            PlaceFurniture();
        }
    }

    /// <summary>
    /// 处理家具选择逻辑
    /// </summary>
    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, furnitureLayer))
            {
                // 修改这里：从碰撞体所在物体向上查找FurnitureInstance
                var furniture = hit.collider.GetComponentInParent<FurnitureInstance>();
                
                if (furniture != null)
                {
                    selectedFurniture = furniture;
                    lastCellPos = floorGrid.WorldToCell(hit.point);
                    Debug.Log($"选中: {furniture.data.displayName}");

                    CorrectInitialPosition();
                }
                else
                {
                    Debug.LogError($"在{hit.collider.gameObject.name}上未找到FurnitureInstance组件");
                }
            }
        }   
    }
    /// <summary>
    /// 考虑旋转状态的边界检查
    /// </summary>
    bool IsOutOfBounds(Vector3Int baseCell, Vector2Int furnitureSize, Quaternion rotation)
    {
        bool isRotated = Mathf.Abs(rotation.eulerAngles.y % 180) > 45;
        Vector2Int actualSize = isRotated ? new Vector2Int(furnitureSize.y, furnitureSize.x) : furnitureSize;

        for (int x = 0; x < actualSize.x; x++)
        {
            for (int z = 0; z < actualSize.y; z++)
            {
                Vector3Int checkCell = baseCell + new Vector3Int(x, 0, z);
                if (checkCell.x < 0 || checkCell.x >= floorSize.x || 
                    checkCell.z < 0 || checkCell.z >= floorSize.y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ResetSelectedFurniture()
    {
        selectedFurniture = null;
    }   
    
    /// <summary>
    /// 设置无效状态
    /// </summary>
    void SetInvalidState()
    {
        isValidPosition = false;
        UpdateVisualFeedback();
        
        // 可选：添加红色闪烁效果
        /*
        if (selectedFurniture != null)
        {
            StartCoroutine(FlashRed(selectedFurniture));
        }
        */
    }

    
    /// <summary>
    /// 处理家具移动逻辑
    /// </summary>
    void HandleMovement()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), 
            out RaycastHit hit, Mathf.Infinity, floorLayer))
        {
            Vector3Int currentCell = floorGrid.WorldToCell(hit.point);
            
            // 边界检查
            if (IsOutOfBounds(currentCell, selectedFurniture.data.gridSize, selectedFurniture.transform.rotation))
            {
                SetInvalidState();
                return;
            }

            // 位置更新
            if (currentCell != lastCellPos)
            {
                Vector3 snappedPos = floorGrid.GetCellCenterWorld(currentCell);
                snappedPos.y = GetCorrectedYPosition(selectedFurniture);
                selectedFurniture.transform.position = snappedPos;
                lastCellPos = currentCell;
                
                CheckPlacementValidity();
            }

            // 旋转处理
            if (selectedFurniture.data.allowRotation && Input.GetKeyDown(KeyCode.R))
            {
                RotateFurniture();
            }
        }
    }

    /// <summary>
    /// 旋转家具并校正位置
    /// </summary>
    void RotateFurniture()
    {
        selectedFurniture.transform.Rotate(0, 90, 0);
        
        // 旋转后校正位置
        Vector3 currentPos = selectedFurniture.transform.position;
        selectedFurniture.transform.position = new Vector3(
            currentPos.x,
            GetCorrectedYPosition(selectedFurniture),
            currentPos.z
        );
        
        CheckPlacementValidity();
    }
    
    
    /// <summary>
    /// 放置家具到当前位置
    /// </summary>
    void PlaceFurniture()
    {
        if (selectedFurniture == null) return;
        
        // 检查当前位置是否有效
        CheckPlacementValidity();
        
        if (isValidPosition)
        {
            // 有效位置 - 直接确认放置
            selectedFurniture = null;
        }
        else
        {
            // 无效位置 - 启动持续调整协程
            FurnitureInstance furnitureToAdjust = selectedFurniture;
            selectedFurniture = null; // 立即释放选中状态以避免冲突
            
            StartCoroutine(ContinuousPositionAdjustment(furnitureToAdjust, success => {
                if (success)
                {
                    Debug.Log($"家具 {furnitureToAdjust.data.displayName} 已放置");
                }
                else
                {
                    Debug.LogWarning($"家具 {furnitureToAdjust.data.displayName} 无法放置");
                    // 可以在这里添加放置失败的额外处理
                }
            }));
        }
    }

    /// <summary>
    /// 检查家具放置是否有效
    /// </summary>
    void CheckPlacementValidity()
    {
        if (selectedFurniture == null) return;

        // 基础检查：边界和碰撞
        Vector3Int baseCell = floorGrid.WorldToCell(selectedFurniture.transform.position);
        if (IsOutOfBounds(baseCell, selectedFurniture.data.gridSize, selectedFurniture.transform.rotation))
        {
            isValidPosition = false;
            UpdateVisualFeedback();
            return;
        }

        // 1.2版本新增：精确物体级碰撞检测
        isValidPosition = !CheckFurnitureObjectOverlap(selectedFurniture);
        UpdateVisualFeedback();
    }

    /// <summary>
    /// 检查家具对象实际模型是否重叠（1.2版本新增）
    /// </summary>
    private Collider[] overlapResults = new Collider[20];

    bool CheckFurnitureObjectOverlap(FurnitureInstance furniture)
    {
        if (furniture == null || furniture.mainCollider == null) 
        {
            Debug.LogWarning("家具或主碰撞体为空，无法检查重叠");
            return false;
        }
        
        // 创建一个稍微放大的检测盒，确保能检测到接近的物体
        Bounds bounds = furniture.mainCollider.bounds;
        
        // 调整检测高度，确保捕获整个家具高度
        float detectionHeight = bounds.size.y;
        
        // 预分配数组避免GC
        Collider[] results = new Collider[20];
        
        // 进行整体检测
        int hitCount = Physics.OverlapBoxNonAlloc(
            bounds.center,
            bounds.extents * 0.95f, // 略微缩小以避免边缘误判
            results,
            furniture.transform.rotation,
            furnitureLayer
        );
        
        // 检查每个碰撞体
        bool hasOverlap = false;
        string overlapInfo = "";
        
        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = results[i];
            if (hit == null) continue;
            
            // 排除自身和子对象
            if (hit.transform == furniture.mainCollider.transform || 
                hit.transform.IsChildOf(furniture.transform))
            {
                continue;
            }
            
            // 获取碰撞体所属的家具实例
            FurnitureInstance otherFurniture = hit.GetComponentInParent<FurnitureInstance>();
            if (otherFurniture != null && otherFurniture != furniture)
            {
                hasOverlap = true;
                overlapInfo += $"{otherFurniture.name}, ";
            }
        }
        
        // 如果检测到重叠，输出详细信息
        if (hasOverlap)
        {
            Debug.LogWarning($"家具 {furniture.name} 与 {overlapInfo} 重叠");
        }
        
        return hasOverlap;
    }
    /// <summary>
    /// 辅助方法：检查结果数组中是否有与当前家具重叠的其他家具
    /// </summary>
    bool CheckResults(Collider[] results, int hitCount, FurnitureInstance currentFurniture)
    {
        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = results[i];
            
            // 排除自身和子对象
            if (hit == null ||
                hit.gameObject == currentFurniture.gameObject ||
                hit.transform.IsChildOf(currentFurniture.transform))
            {
                continue;
            }
            
            // 查找命中对象的家具实例
            FurnitureInstance otherFurniture = hit.GetComponentInParent<FurnitureInstance>();
            if (otherFurniture != null && otherFurniture != currentFurniture)
            {
                Debug.Log($"家具 {currentFurniture.name} 与 {otherFurniture.name} 重叠", hit.gameObject);
                return true;
            }
        }
        
        return false;
    }



     /// <summary>
    /// 计算家具正确的Y轴位置（确保完全露出）
    /// </summary>
    float GetCorrectedYPosition(FurnitureInstance furniture)
    {
        // 获取家具底部到pivot的距离
        float bottomOffset = GetBottomOffset(furniture);
        return floorGrid.transform.position.y + bottomOffset;
    }

    /// <summary>
    /// 获取家具底部到pivot点的垂直距离
    /// </summary>
    float GetBottomOffset(FurnitureInstance furniture)
    {
        Collider collider = furniture.GetComponent<Collider>();
        if (collider == null) return 0;

        // 计算从pivot到碰撞体底部的距离
        return collider.bounds.extents.y + (furniture.transform.position.y - collider.bounds.min.y);
    }

    /// <summary>
    /// 修正初始位置
    /// </summary>
    void CorrectInitialPosition()
    {
        Vector3 correctedPos = selectedFurniture.transform.position;
        correctedPos.y = GetCorrectedYPosition(selectedFurniture);
        selectedFurniture.transform.position = correctedPos;
    }

    /// <summary>
    /// 获取家具的半包围盒大小
    /// </summary>
    Vector3 GetFurnitureHalfExtents()
    {
        // 根据家具尺寸和旋转计算实际占用空间
        Vector3 size = new Vector3(
            selectedFurniture.data.gridSize.x * floorGrid.cellSize.x,
            0.1f, // 检测高度
            selectedFurniture.data.gridSize.y * floorGrid.cellSize.z
        )*1f;// * 0.5f;

        // 考虑旋转后的实际占用
        if (Mathf.RoundToInt(selectedFurniture.transform.eulerAngles.y / 90) % 2 != 0)
        {
            size = new Vector3(size.z, size.y, size.x);
        }

        return size;
    }
    

    /// <summary>
    /// 检查当前位置是否有效
    /// </summary>
    
    void UpdateVisualFeedback()
    {
        if (selectedFurniture == null) return;
        
        // 实际项目中替换为材质变化或UI提示
        Debug.Log(isValidPosition ? "位置有效" : "位置无效（超出边界或碰撞）");
    }

    /// <summary>
    /// 释放家具
    /// </summary>
    void ReleaseFurniture()
    {
        if (selectedFurniture != null)
        {
            if (!isValidPosition)
            {
                TryAdjustPosition();
            }
            selectedFurniture = null;
        }
    }

    /// <summary>
    /// 尝试调整家具位置
    /// </summary>
    private bool TryAdjustPosition()
    {
        if (selectedFurniture == null) return false;

        // 保存原始位置和旋转
        Vector3 originalPos = selectedFurniture.transform.position;
        Quaternion originalRot = selectedFurniture.transform.rotation;
        Vector3Int originalCell = floorGrid.WorldToCell(originalPos);
        
        // 用于记录最佳找到的位置
        Vector3 bestPosition = originalPos;
        float closestDistance = float.MaxValue;
        bool foundValidPosition = false;

        // 创建更全面的搜索模式 - 螺旋形搜索
        List<Vector3Int> searchOffsets = GenerateSpiralOffsets(Mathf.CeilToInt(searchRadius / adjustmentStep));

        // 测试原始位置
        CheckPlacementValidity();
        if (isValidPosition)
        {
            return true; // 原始位置已经有效，不需要调整
        }

        // 尝试不同位置
        foreach (Vector3Int offset in searchOffsets)
        {
            // 计算测试位置
            Vector3Int testCell = originalCell + offset;
            Vector3 testPos = floorGrid.CellToWorld(testCell);
            testPos.y = GetCorrectedYPosition(selectedFurniture); // 确保Y轴正确
            
            // 移动家具到测试位置
            selectedFurniture.transform.position = testPos;
            
            // 检查此位置是否有效
            CheckPlacementValidity();
            
            if (isValidPosition)
            {
                float currentDistance = Vector3.Distance(testPos, originalPos);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    bestPosition = testPos;
                    foundValidPosition = true;
                }
            }
            
            // 如果是特殊项目 - 也可以尝试旋转
            if (selectedFurniture.data.allowRotation && !foundValidPosition)
            {
                // 尝试90度旋转
                for (int rot = 1; rot <= 3; rot++) // 测试90°, 180°, 270°
                {
                    selectedFurniture.transform.position = testPos;
                    selectedFurniture.transform.rotation = originalRot * Quaternion.Euler(0, rot * 90, 0);
                    
                    CheckPlacementValidity();
                    if (isValidPosition)
                    {
                        float rotPenalty = rot * 0.5f; // 增加一点旋转的"成本"，优先选择不旋转的解决方案
                        float currentDistance = Vector3.Distance(testPos, originalPos) + rotPenalty;
                        if (currentDistance < closestDistance)
                        {
                            closestDistance = currentDistance;
                            bestPosition = testPos;
                            foundValidPosition = true;
                            
                            // 记录下旋转状态 - 这里可以添加代码保存最佳旋转
                            break; // 找到有效旋转就退出旋转循环
                        }
                    }
                }
                
                // 恢复原始旋转以便继续测试
                selectedFurniture.transform.rotation = originalRot;
            }
            
            // 如果找到非常接近的有效位置，可以提前结束搜索
            if (foundValidPosition && closestDistance < adjustmentStep * 2)
            {
                break;
            }
        }

        // 应用最佳找到的位置，或者恢复原位
        if (foundValidPosition)
        {
            selectedFurniture.transform.position = bestPosition;
            lastCellPos = floorGrid.WorldToCell(bestPosition);
            
            // 重新检查最终位置
            CheckPlacementValidity();
            if (isValidPosition)
            {
                Debug.Log($"已调整到最近有效位置，偏移量: {(bestPosition - originalPos)}");
                return true;
            }
            else
            {
                // 异常情况：找到的位置实际上无效
                Debug.LogError("找到的位置验证失败，可能是检测逻辑不一致");
                selectedFurniture.transform.position = originalPos;
                selectedFurniture.transform.rotation = originalRot;
                return false;
            }
        }
        else
        {
            // 未找到有效位置
            selectedFurniture.transform.position = originalPos;
            selectedFurniture.transform.rotation = originalRot;
            Debug.LogWarning("未找到有效位置，已返回原位");
            
            // 最终验证原始位置 - 只是为了视觉更新
            CheckPlacementValidity();
            return false;
        }
    }
    /// <summary>
    /// 生成螺旋形搜索模式的偏移量
    /// </summary>
    private List<Vector3Int> GenerateSpiralOffsets(int maxRadius)
    {
        List<Vector3Int> offsets = new List<Vector3Int>();
        
        // 添加中心点
        offsets.Add(new Vector3Int(0, 0, 0));
        
        // 生成螺旋形搜索模式
        for (int layer = 1; layer <= maxRadius; layer++)
        {
            // 添加水平和垂直方向
            for (int x = -layer; x <= layer; x++)
            {
                offsets.Add(new Vector3Int(x, 0, -layer)); // 上边
                offsets.Add(new Vector3Int(x, 0, layer));  // 下边
            }
            
            for (int z = -layer + 1; z <= layer - 1; z++)
            {
                offsets.Add(new Vector3Int(-layer, 0, z)); // 左边
                offsets.Add(new Vector3Int(layer, 0, z));  // 右边
            }
        }
        
        // 按照距离排序，确保先检查近处的位置
        offsets.Sort((a, b) => 
            (a.x * a.x + a.z * a.z).CompareTo(b.x * b.x + b.z * b.z)
        );
        
        return offsets;
    }
    /// <summary>
    /// 1.3版本新增
    /// 持续调整家具位置直到找到无碰撞位置或达到最大尝试次数
    /// </summary>
    public IEnumerator ContinuousPositionAdjustment(FurnitureInstance furniture, System.Action<bool> onComplete = null)
    {
        if (furniture == null)
        {
            Debug.LogError("调整失败：家具为空");
            onComplete?.Invoke(false);
            yield break;
        }
        
        // 保存原始位置和旋转
        Vector3 originalPosition = furniture.transform.position;
        Quaternion originalRotation = furniture.transform.rotation;
        
        // 最大尝试次数和计数器
        int maxAttempts = 30; // 增加尝试次数
        int attemptCount = 0;
        bool foundValidPosition = false;
        
        // 记录已尝试位置
        HashSet<Vector3Int> triedPositions = new HashSet<Vector3Int>();
        Vector3Int originalCell = floorGrid.WorldToCell(originalPosition);
        
        // 生成搜索位置 - 使用更大的搜索范围
        List<Vector3Int> searchPositions = new List<Vector3Int>();
        
        // 首先添加正交方向（上下左右）
        for (int distance = 1; distance <= 5; distance++)
        {
            searchPositions.Add(new Vector3Int(distance, 0, 0));    // 右
            searchPositions.Add(new Vector3Int(-distance, 0, 0));   // 左
            searchPositions.Add(new Vector3Int(0, 0, distance));    // 上
            searchPositions.Add(new Vector3Int(0, 0, -distance));   // 下
        }
        
        // 然后添加对角线方向
        for (int distance = 1; distance <= 4; distance++)
        {
            searchPositions.Add(new Vector3Int(distance, 0, distance));     // 右上
            searchPositions.Add(new Vector3Int(-distance, 0, distance));    // 左上
            searchPositions.Add(new Vector3Int(distance, 0, -distance));    // 右下
            searchPositions.Add(new Vector3Int(-distance, 0, -distance));   // 左下
        }
        
        // 按距离排序
        searchPositions.Sort((a, b) => 
            (a.x * a.x + a.z * a.z).CompareTo(b.x * b.x + b.z * b.z)
        );
        
        // 先测试原始位置
        furniture.transform.position = originalPosition;
        furniture.transform.rotation = originalRotation;
        
        // 检查原始位置是否有效
        bool overlapping = CheckFurnitureObjectOverlap(furniture);
        if (!overlapping)
        {
            Debug.Log("原始位置已经有效，无需调整");
            onComplete?.Invoke(true);
            yield break;
        }
        
        // 添加原始位置到已尝试集合
        triedPositions.Add(originalCell);
        
        // 主调整循环
        while (attemptCount < maxAttempts && !foundValidPosition)
        {
            attemptCount++;
            
            // 选择下一个位置尝试
            Vector3Int nextOffset = Vector3Int.zero;
            bool foundNextPosition = false;
            
            foreach (var offset in searchPositions)
            {
                Vector3Int testCell = originalCell + offset;
                if (!triedPositions.Contains(testCell))
                {
                    nextOffset = offset;
                    triedPositions.Add(testCell);
                    foundNextPosition = true;
                    break;
                }
            }
            
            if (!foundNextPosition)
            {
                Debug.LogWarning("已尝试所有可能位置");
                break;
            }
            
            // 计算新位置的世界坐标
            Vector3Int newCell = originalCell + nextOffset;
            Vector3 newPosition = floorGrid.CellToWorld(newCell);
            newPosition.y = GetCorrectedYPosition(furniture); // 确保Y轴正确
            
            // 移动到新位置
            furniture.transform.position = newPosition;
            
            // 执行多次重叠检测，确保结果一致
            bool isOverlapping = true;
            
            // 进行3次检测，确保结果稳定
            for (int i = 0; i < 3; i++)
            {
                isOverlapping = CheckFurnitureObjectOverlap(furniture);
                if (isOverlapping) break;
                yield return new WaitForSeconds(0.02f); // 短暂等待物理系统更新
            }
            
            // 如果位置有效（无重叠），则标记成功
            if (!isOverlapping)
            {
                foundValidPosition = true;
                Debug.Log($"在第{attemptCount}次尝试后找到无重叠位置: {nextOffset}");
                break;
            }
            
            // 添加短暂延迟，使调整过程可见
            if (attemptCount % 3 == 0)
            {
                // 每3次尝试更新一次视觉效果
                float t = (float)attemptCount / maxAttempts;
            }
            
            yield return new WaitForSeconds(0.05f);
        }
        
        // 最终验证 - 确保位置真的有效
        if (foundValidPosition)
        {
            // 再次检查当前位置确保无重叠
            bool finalCheck = CheckFurnitureObjectOverlap(furniture);
            if (finalCheck)
            {
                // 如果最终检查仍然检测到重叠，调整失败
                Debug.LogError("最终检查仍然检测到重叠，调整失败");
                furniture.transform.position = originalPosition;
                furniture.transform.rotation = originalRotation;
                foundValidPosition = false;
            }
        }
        
        // 处理调整结果
        if (foundValidPosition)
        {
            Debug.Log("成功调整到无重叠位置");
            
            // 更新最后的网格位置
            lastCellPos = floorGrid.WorldToCell(furniture.transform.position);
        }
        else
        {
            Debug.LogWarning("调整失败，无法找到有效位置");
            // 恢复原始位置
            furniture.transform.position = originalPosition;
            furniture.transform.rotation = originalRotation;
            
            
        }
    
    // 调用完成回调
    onComplete?.Invoke(foundValidPosition);
    }
}

