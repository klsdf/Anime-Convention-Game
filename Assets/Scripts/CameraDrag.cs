using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//作者：闫辰祥
//创建时间: 2024年8月12日

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 0.1f; // 拖动速度
    public float zoomSpeed = 0.5f; // 缩放速度
    public float minZoom = 5f; // 最小缩放限制
    public float maxZoom = 20f; // 最大缩放限制

    private Vector3 dragOrigin; // 拖动起点
    private bool isDragging = false; // 是否正在拖动

    void Update()
    {  // 检查是否点击在 UI 上
        if (GameManager.Instance.isDragingObject == true)
        {
            return; // 如果在 UI 上，则不进行拖动和缩放
        }
        HandleMouseInput();
        HandleTouchInput();
    }

    void HandleMouseInput()
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

    void HandleTouchInput()
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
