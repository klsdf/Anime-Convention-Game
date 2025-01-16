using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机跟随控制器
/// 实现相机平滑跟随目标物体的功能
/// </summary>
public class DemoCameraFollow : MonoBehaviour
{
    /// <summary>
    /// 相机组件引用
    /// </summary>
    private Camera mycamera;

    /// <summary>
    /// 当前跟随的目标
    /// </summary>
    public Transform nowTarget;

    /// <summary>
    /// 目标索引
    /// </summary>
    private int index = 0;

    /// <summary>
    /// 平滑移动时间
    /// </summary>
    public float smoothTime = 0.3f;

    /// <summary>
    /// Y轴阈值
    /// </summary>
    public float yThreshold = -1f;

    /// <summary>
    /// X轴速度
    /// </summary>
    private float velocityX;

    /// <summary>
    /// Y轴速度
    /// </summary>
    private float velocityY;

    /// <summary>
    /// 边界精灵渲染器
    /// </summary>
    public SpriteRenderer boundSprite;

    /// <summary>
    /// 相机移动的最小X坐标
    /// </summary>
    private float minX;

    /// <summary>
    /// 相机移动的最大X坐标
    /// </summary>
    private float maxX;

    /// <summary>
    /// 相机移动的最小Y坐标
    /// </summary>
    private float minY;

    /// <summary>
    /// 相机移动的最大Y坐标
    /// </summary>
    private float maxY;

    /// <summary>
    /// 初始化相机设置
    /// </summary>
    private void Start()
    {
        mycamera = GetComponent<Camera>();
        mycamera.orthographicSize = 3;
        //nowTarget = targets[index];
        
        if (boundSprite != null)
        {
            CalculateBounds();
        }
    }

    /// <summary>
    /// 计算相机的移动边界
    /// </summary>
    private void CalculateBounds()
    {
        float verticalSize = mycamera.orthographicSize;
        float horizontalSize = verticalSize * Screen.width / Screen.height;

        float spriteWidth = boundSprite.bounds.size.x;
        float spriteHeight = boundSprite.bounds.size.y;

        // Calculate boundaries based on sprite edges and camera view size
        minX = boundSprite.transform.position.x - spriteWidth/2 + horizontalSize;
        maxX = boundSprite.transform.position.x + spriteWidth/2 - horizontalSize;
        minY = boundSprite.transform.position.y - spriteHeight/2 + verticalSize;
        maxY = boundSprite.transform.position.y + spriteHeight/2 - verticalSize;
    }

    /// <summary>
    /// 相机拖动速度
    /// </summary>
    public float dragSpeed = 20;

    /// <summary>
    /// 拖动起始点
    /// </summary>
    private Vector3 dragOrigin;

    private void Update()
    {
        // if (Input.GetMouseButtonDown(2)) // 检测鼠标中键按下
        // {
        //     dragOrigin = Input.mousePosition; // 记录鼠标位置
        // }

        //  if (Input.GetMouseButton(2)) // 检测鼠标中键持续按下
        // {
        //     Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin); // 计算鼠标移动的距离

        //     Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0); // 计算摄像机移动的距离

        //     transform.Translate(move, Space.Self); // 在摄像机的局部坐标系下移动

        //     dragOrigin = Input.mousePosition; // 更新鼠标位置
        // }


        // if (Input.GetKey(KeyCode.LeftBracket))
        // {
        //     mycamera.orthographicSize -= 0.1f;
        // }
        // else if (Input.GetKey(KeyCode.RightBracket))
        // {
        //     mycamera.orthographicSize += 0.1f;
        // }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    index = (index + 1) % targets.Length;
        //    nowTarget.gameObject.SetActive(false);
        //    nowTarget = targets[index];
  
           
        //    nowTarget.gameObject.SetActive(true);
        //}

        //当按下[时，相机缩小，当按下]时，相机放大
        // if (Input.GetKeyDown(KeyCode.LeftBracket))

        // CheckMouseScroll();
    }

    public float zoomSpeed = 20f; // 缩放速度
    public float minZoom = 1.0f; // 最小缩放
    public float maxZoom = 50f; // 最大缩放
    private void CheckMouseScroll()
    {

        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        mycamera.orthographicSize -= scrollData * zoomSpeed;
        mycamera.orthographicSize = Mathf.Clamp(mycamera.orthographicSize, minZoom, maxZoom);
    }

    private void FixedUpdate()
    {
        if (nowTarget == null) return;

        var posX = Mathf.SmoothDamp(transform.position.x, nowTarget.position.x, ref velocityX, smoothTime);
        var posY = Mathf.SmoothDamp(transform.position.y, nowTarget.position.y, ref velocityY, smoothTime);

        // 如果设置了边界sprite，则限制相机位置
        if (boundSprite != null)
        {
            // 重新计算边界（因为正交大小可能会改变）
            CalculateBounds();
            
            // 限制相机位置在边界内
            posX = Mathf.Clamp(posX, minX, maxX);
            posY = Mathf.Clamp(posY, minY, maxY);
        }

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public void SetBoundSprite(SpriteRenderer sprite)
    {
        boundSprite = sprite;
    }
}
