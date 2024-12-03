using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCameraFollow : MonoBehaviour
{


    private Camera  mycamera;
    //public Transform[] targets;
    public Transform nowTarget;
    private int index = 0;


    public float smoothTime = 0.3f;
    public float yThreshold = -1f;
    private float velocityX;
    private float velocityY;
    


    public SpriteRenderer boundSprite; // 新增：限制边界的 Sprite
    
    // 用于存储相机的移动限制
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


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

    // 计算相机的移动边界
    private void CalculateBounds()
    {
        float verticalSize = mycamera.orthographicSize;
        float horizontalSize = verticalSize * Screen.width / Screen.height;

        // 获取sprite的边界
        float spriteWidth = boundSprite.bounds.size.x;
        float spriteHeight = boundSprite.bounds.size.y;

        // 计算相机可移动的最大范围
        minX = boundSprite.transform.position.x - (spriteWidth/2 - horizontalSize);
        maxX = boundSprite.transform.position.x + (spriteWidth/2 - horizontalSize);
        minY = boundSprite.transform.position.y - (spriteHeight/2 - verticalSize);
        maxY = boundSprite.transform.position.y + (spriteHeight/2 - verticalSize);
    }

    public float dragSpeed = 20; // 拖动速度

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
}
