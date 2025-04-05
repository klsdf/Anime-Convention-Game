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
    [SerializeField] private FurnitureInstance selectedFurniture;
    [SerializeField] private Vector3Int lastCellPos;
    [SerializeField] private bool isValidPosition;

    void Update()
    {
        HandleSelection();
        HandleMovement();
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
                var furniture = hit.collider.GetComponent<FurnitureInstance>();
                if (furniture != null)
                {
                    selectedFurniture = furniture;
                    lastCellPos = floorGrid.WorldToCell(hit.point);
                    Debug.Log($"选中: {furniture.data.displayName}");

                    // 初始位置校正
                    CorrectInitialPosition();
                }
            }
        }
    }
    /// <summary>
    /// 检查是否会超出地板边界
    /// </summary>
    bool IsOutOfBounds(Vector3Int baseCell, Vector2Int furnitureSize)
    {
        // 计算家具占用的所有网格
        for (int x = 0; x < furnitureSize.x; x++)
        {
            for (int z = 0; z < furnitureSize.y; z++)
            {
                Vector3Int checkCell = baseCell + new Vector3Int(x, 0, z);
                
                // 检查是否超出地板范围
                if (checkCell.x < 0 || checkCell.x >= floorSize.x || 
                    checkCell.z < 0 || checkCell.z >= floorSize.y)
                {
                    return true;
                }
            }
        }
        return false;
    }


    /// <summary>
    /// 处理家具移动逻辑
    /// </summary>
    void HandleMovement()
    {
        // 如果未选中家具或未按住鼠标左键，则返回
        if (selectedFurniture == null || !Input.GetMouseButton(0)) return;
        
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), 
            out RaycastHit hit, Mathf.Infinity, floorLayer))
        {
            Vector3Int currentCell = floorGrid.WorldToCell(hit.point);
            // 检查是否超出地板边界
            if (IsOutOfBounds(currentCell, selectedFurniture.data.gridSize))
            {
                SetInvalidState();
                return;
            }

            // 如果当前位置与上次位置不同，则更新位置
            if (currentCell != lastCellPos)
            {
                // 关键修改：确保家具完全露出地板
                Vector3 snappedPos = floorGrid.GetCellCenterWorld(currentCell);
                snappedPos.y = GetCorrectedYPosition(selectedFurniture);

                selectedFurniture.transform.position = snappedPos;
                lastCellPos = currentCell;

                CheckPlacementValidity();
            }
        }

/// <summary>
/// 设置无效状态
/// </summary>
    void SetInvalidState()
    {
        isValidPosition = false;
        UpdateVisualFeedback();
        
        // 可选：添加红色闪烁效果
        if (selectedFurniture != null)
        {
            StartCoroutine(FlashRed(selectedFurniture));
        }
    }

    /// <summary>
    /// 添加红色闪烁效果
    /// </summary>  
    System.Collections.IEnumerator FlashRed(FurnitureInstance furniture)
    {
        var renderer = furniture.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = originalColor;
        }
    }


        // 如果家具允许旋转，则处理旋转逻辑
        if (selectedFurniture.data.allowRotation && Input.GetKeyDown(KeyCode.R))
        {
            selectedFurniture.transform.Rotate(0, 90, 0);
            // 旋转后立即校正高度   
            selectedFurniture.transform.position = new Vector3(
                selectedFurniture.transform.position.x,
                GetCorrectedYPosition(selectedFurniture),
                selectedFurniture.transform.position.z
            );
            CheckPlacementValidity();
        }
    }

    /// <summary>
    /// 检查放置位置是否有效
    /// </summary>
    void CheckPlacementValidity()
    {
        if (selectedFurniture == null) return;

        // 检查边界
        Vector3Int baseCell = floorGrid.WorldToCell(selectedFurniture.transform.position);
        if (IsOutOfBounds(baseCell, selectedFurniture.data.gridSize))
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

