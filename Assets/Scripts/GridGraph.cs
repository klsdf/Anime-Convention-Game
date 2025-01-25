/// @file GridGraph.cs
/// @brief 网格图类，负责创建和显示网格。
/// @author 闫辰祥
/// @date 2024年12月8日

using UnityEngine;
using UnityEngine.UI;

/// @class GridGraph
/// @brief 处理网格的创建和显示。
public class GridGraph : MonoBehaviour
{
    [SerializeField] private int rows = 10;
    [SerializeField] private int columns = 10;
    [SerializeField] private float cellSize = 50f;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Color gridLineColor = new Color(0.3f, 0.3f, 0.3f, 1f); // 网格线颜色
    [SerializeField] private float gridLineWidth = 1f; // 网格线宽度

    /// @brief 当脚本启用时调用。
    /// @details 创建网格并创建网格线。
    private void Start()
    {
        CreateGrid();
        CreateGridLines();
    }

    /// @brief 创建网格。
    /// @details 创建所有单元格并设置它们的大小和位置。
    private void CreateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // 创建单元格
                GameObject cell = Instantiate(cellPrefab, transform);
                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                
                // 设置单元格大小和位置
                rectTransform.sizeDelta = new Vector2(cellSize, cellSize);
                rectTransform.anchoredPosition = new Vector2(j * cellSize, -i * cellSize);
                
                // 添加点击事件
                Button button = cell.GetComponent<Button>();
                int row = i;
                int col = j;
                button.onClick.AddListener(() => OnCellClicked(row, col));
            }
        }

        // 设置容器大小
        RectTransform graphTransform = GetComponent<RectTransform>();
        graphTransform.sizeDelta = new Vector2(columns * cellSize, rows * cellSize);
    }

    /// @brief 处理单元格点击事件。
    /// @details 当单元格被点击时调用。
    /// @param row 点击的行索引。
    /// @param col 点击的列索引。
    private void OnCellClicked(int row, int col)
    {
        Debug.Log($"点击了单元格: ({row}, {col})");
    }

    /// @brief 创建网格线。
    /// @details 创建水平线和垂直线。
    private void CreateGridLines()
    {
        // 创建水平线
        for (int i = 0; i <= rows; i++)
        {
            GameObject horizontalLine = new GameObject($"HorizontalLine_{i}", typeof(Image));
            horizontalLine.transform.SetParent(transform, false);
            
            Image lineImage = horizontalLine.GetComponent<Image>();
            lineImage.color = gridLineColor;
            
            RectTransform rectTransform = horizontalLine.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.sizeDelta = new Vector2(0, gridLineWidth);
            rectTransform.anchoredPosition = new Vector2(0, -i * cellSize);
        }

        // 创建垂直线
        for (int j = 0; j <= columns; j++)
        {
            GameObject verticalLine = new GameObject($"VerticalLine_{j}", typeof(Image));
            verticalLine.transform.SetParent(transform, false);
            
            Image lineImage = verticalLine.GetComponent<Image>();
            lineImage.color = gridLineColor;
            
            RectTransform rectTransform = verticalLine.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.sizeDelta = new Vector2(gridLineWidth, 0);
            rectTransform.anchoredPosition = new Vector2(j * cellSize, 0);
        }
    }
} 