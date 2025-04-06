using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 作者：龚科翰
/// 日期：2025-03-31
/// 功能：家具放置系统
/// </summary>
public class FurniturePlacementSystem : MonoBehaviour
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
        if (isValidPosition)
        {
            // 有效位置 - 确认放置
            Debug.Log($"家具 {selectedFurniture.data.displayName} 已放置在 {lastCellPos}");
            
            // 这里可以添加放置音效或其他反馈
            if (selectedFurniture.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.white; // 恢复原始颜色
            }
        }
        else
        {
            // 无效位置 - 返回原位
            selectedFurniture.transform.position = floorGrid.CellToWorld(lastCellPos);
            Debug.LogWarning("无效位置，已返回原位");
        }
        
        selectedFurniture = null; // 释放选中状态
    }


    /// <summary>
    /// 检查放置位置是否有效
    /// </summary>
    void CheckPlacementValidity()
    {
        if (selectedFurniture == null) return;

        // 检查边界
        Vector3Int baseCell = floorGrid.WorldToCell(selectedFurniture.transform.position);
        if (IsOutOfBounds(baseCell, selectedFurniture.data.gridSize, selectedFurniture.transform.rotation))
        {
            isValidPosition = false;
            UpdateVisualFeedback();
            return;
        }

        // 检查与其他家具的碰撞
        Collider[] colliders = Physics.OverlapBox(
            selectedFurniture.transform.position,
            GetFurnitureHalfExtents(),
            selectedFurniture.transform.rotation,
            furnitureLayer
        );

        isValidPosition = colliders.All(c => c.gameObject != selectedFurniture.gameObject);
        UpdateVisualFeedback();
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
        ) * 0.5f;

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

    void ReleaseFurniture()
    {
        if (selectedFurniture != null)
        {
            if (!isValidPosition)
            {
                // 位置无效时返回原位
                selectedFurniture.transform.position = floorGrid.CellToWorld(lastCellPos);
            }
            selectedFurniture = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (selectedFurniture == null) return;

        // 绘制家具底部位置参考线
        Gizmos.color = Color.green;
        float bottomY = GetCorrectedYPosition(selectedFurniture);
        Vector3 size = new Vector3(selectedFurniture.data.gridSize.x, 0.01f, selectedFurniture.data.gridSize.y);
        Gizmos.DrawCube(
            new Vector3(
                selectedFurniture.transform.position.x,
                bottomY,
                selectedFurniture.transform.position.z
            ),
            size
        );
    }
}

