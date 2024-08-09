using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 飞船移动 : MonoBehaviour
{
   
    public float speed = 1.0f;  // 你可以在Unity的Inspector面板中设置这个变量的值，表示物体的移动速度

    void Update()
    {
        // 使物体向左移动
        transform.position += Vector3.left * speed * Time.deltaTime;

        // 当物体的x坐标小于-7时，把物体传送到x=7的位置
        if (transform.position.x < -8)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = 10;
            transform.position = newPosition;
        }
    }
}
