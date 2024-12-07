using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024/8/13

public class ScrollImage : MonoBehaviour
{
    public float speed = 10f; // 滚动速度
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent.GetComponent<RectTransform>();
        
    }

    void Update()
    {

            rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

    }
}
