using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//作者：闫辰祥
//创建时间: 2024年8月12日

/// <summary>
/// 相机拖拽控制器
/// 实现相机的拖拽移动功能
/// </summary>
public class CameraDrag : MonoBehaviour
{
    /// <summary>
    /// 相机移动速度
    /// </summary>
    [SerializeField]
    private float moveSpeed = 5f;

    /// <summary>
    /// 相机拖动速度
    /// </summary>
    public float dragSpeed = 0.1f;

    /// <summary>
    /// 相机缩放速度
    /// </summary>
    public float zoomSpeed = 0.5f;

    /// <summary>
    /// 最小缩放限制
    /// </summary>
    public float minZoom = 5f;

    /// <summary>
    /// 最大缩放限制
    /// </summary>
    public float maxZoom = 20f;

    /// <summary>
    /// 拖动起始点
    /// </summary>
    private Vector3 dragOrigin;

    /// <summary>
    /// 是否正在拖动标志
    /// </summary>
    private bool isDragging = false;

    /// <summary>
    /// 初始化相机设置
    /// </summary>
    private void Start()
    {
        // 现有代码...
    }

    /// <summary>
    /// 每帧更新相机位置
    /// </summary>
    private void Update()
    {
        // 检查是否点击在 UI 上
        if (GameManager.Instance.isDragingObject == true)
        {
            return; // 如果在 UI 上，则不进行拖动和缩放
        }
        HandleMouseInput();
        HandleTouchInput();
    }

    /// <summary>
    /// 处理相机拖拽移动
    /// </summary>
    /// <param name="dragDelta">拖拽位移向量</param>
    private void HandleCameraDrag(Vector2 dragDelta)
    {
        // 现有代码...
    }

    /// <summary>
    /// 处理鼠标输入
    /// 包括鼠标拖动和滚轮缩放功能
    /// </summary>
    private void HandleMouseInput()
    {
        // 鼠标拖动输入
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;
            Camera.main.transform.position += difference * dragSpeed;
        }

        // 鼠标滚轮缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //print(scroll);
        if (scroll != 0.0f)
        {

            float newSize = Camera.main.orthographicSize - scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    /// <summary>
    /// 处理触摸输入
    /// 包括单指拖动和双指缩放功能
    /// </summary>
    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            // 单指拖动
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 currentPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 difference = dragOrigin - currentPos;
                Camera.main.transform.position += difference * dragSpeed;
            }
        }
        else if (Input.touchCount == 2)
        {
            // 双指缩放
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            float newSize = Camera.main.orthographicSize - difference * zoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    /// <summary>
    /// 检查指针是否在UI元素上
    /// </summary>
    /// <returns>如果指针在UI元素上返回true，否则返回false</returns>
    private bool IsPointerOverUIObject()
    {
        // 检测鼠标是否悬停在 UI 元素上
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
