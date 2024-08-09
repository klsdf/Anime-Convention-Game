using UnityEngine;

public class Drag2DObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // 计算鼠标点击位置和物体中心之间的偏移量
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        // 更新物体位置
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        // 停止拖动
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 获取鼠标在世界坐标中的位置
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.nearClipPlane; // 确保我们在2D空间中，所以Z轴设为0

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}