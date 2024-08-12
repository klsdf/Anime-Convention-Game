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
    


    private void Start()
    {
        mycamera = GetComponent<Camera>();
        mycamera.orthographicSize = 4;
        //nowTarget = targets[index];
    }
    public float dragSpeed = 20; // 拖动速度

    private Vector3 dragOrigin;
    private void Update()
    {
        if (Input.GetMouseButtonDown(2)) // 检测鼠标中键按下
        {
            dragOrigin = Input.mousePosition; // 记录鼠标位置
        }

         if (Input.GetMouseButton(2)) // 检测鼠标中键持续按下
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin); // 计算鼠标移动的距离

            Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0); // 计算摄像机移动的距离

            transform.Translate(move, Space.Self); // 在摄像机的局部坐标系下移动

            dragOrigin = Input.mousePosition; // 更新鼠标位置
        }


        if (Input.GetKey(KeyCode.LeftBracket))
        {
            mycamera.orthographicSize -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.RightBracket))
        {
            mycamera.orthographicSize += 0.1f;
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    index = (index + 1) % targets.Length;
        //    nowTarget.gameObject.SetActive(false);
        //    nowTarget = targets[index];
  
           
        //    nowTarget.gameObject.SetActive(true);
        //}

        //当按下[时，相机缩小，当按下]时，相机放大
        // if (Input.GetKeyDown(KeyCode.LeftBracket))

        CheckMouseScroll();
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
        var posX = Mathf.SmoothDamp(transform.position.x, nowTarget.position.x, ref velocityX, smoothTime);
        var posY = transform.position.y;
        if (nowTarget.transform.position.y > yThreshold)
        {
             posY = Mathf.SmoothDamp(transform.position.y, nowTarget.position.y, ref velocityY, smoothTime);
        }
        else
        {
            posY = Mathf.SmoothDamp(transform.position.y, yThreshold, ref velocityY, smoothTime);
        }

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
