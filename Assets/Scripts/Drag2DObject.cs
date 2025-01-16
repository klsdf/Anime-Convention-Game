using UnityEngine;
//作者：闫辰祥
//创建时间: 2024年8月9日

/// <summary>
/// 2D物体拖拽控制器
/// 实现物体的拖拽功能
/// </summary>
public class Drag2DObject : MonoBehaviour
{
    /// <summary>
    /// 鼠标点击位置与物体中心的偏移量
    /// </summary>
    private Vector3 offset;

    /// <summary>
    /// 主相机引用
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// 是否正在拖拽
    /// </summary>
    private bool isDragging = false;

    /// <summary>
    /// 初始化组件引用
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// 处理鼠标按下事件
    /// </summary>
    private void OnMouseDown()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        isDragging = true;
        GameManager.Instance.isDragingObject = true;
    }

    /// <summary>
    /// 处理鼠标拖拽事件
    /// </summary>
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    /// <summary>
    /// 处理鼠标释放事件
    /// </summary>
    private void OnMouseUp()
    {
        isDragging = false;
        GameManager.Instance.isDragingObject = false;
    }

    /// <summary>
    /// 获取鼠标在世界坐标中的位置
    /// </summary>
    /// <returns>返回鼠标位置的世界坐标</returns>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}