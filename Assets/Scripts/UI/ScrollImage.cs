using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024/8/13

public class ScrollImage : MonoBehaviour
{
    public float speed = 100f; // 滚动速度
    private RectTransform rectTransform;
    private float canvasWidth;
    private bool movingLeft = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasWidth = rectTransform.parent.GetComponent<RectTransform>().rect.width;
    }

    void Update()
    {
        float imageWidth = rectTransform.rect.width;

        if (movingLeft)
        {
            // 向左移动
            rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // 如果图片移动到最左边，改为向右移动
            if (rectTransform.anchoredPosition.x <= -20f) // 可以根据需要调整这个值
            {
                movingLeft = false;
            }
        }
        else
        {
            // 向右移动
            rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;

            // 如果图片移动到最右边，改为向左移动
            if (rectTransform.anchoredPosition.x >= 20f) // 可以根据需要调整这个值
            {
                movingLeft = true;
            }
        }
    }
}