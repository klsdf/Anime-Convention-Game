using UnityEngine;
using DG.Tweening;
//作者：闫辰祥
//创建时间: 2024/8/13

public class ScrollImage : MonoBehaviour
{
    public float speed = 100f; // 滚动速度
    private RectTransform rectTransform;
    private float canvasWidth;
    private bool movingLeft = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasWidth = rectTransform.rect.width;
        float imageWidth = rectTransform.rect.width;
        if(movingLeft){
            var scroll_anim = DOTween.To(() => rectTransform.anchoredPosition, value => rectTransform.anchoredPosition = value
            , new Vector2(-(imageWidth-canvasWidth), 0), 30f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            scroll_anim.Restart();
        }
    }

    // void Update()
    // {
    //     float imageWidth = rectTransform.rect.width;

    //     if (movingLeft)
    //     {
    //         // 向左移动
    //         rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

    //         // 如果图片的右边缘到达 Canvas 的左边界，反向移动
    //         if (rectTransform.anchoredPosition.x <= -(canvasWidth - imageWidth) / 2)
    //         {
    //             movingLeft = false;
    //         }
    //     }
    //     else
    //     {
    //         // 向右移动
    //         rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;

    //         // 如果图片的左边缘到达 Canvas 的右边界，反向移动
    //         if (rectTransform.anchoredPosition.x >= (canvasWidth - imageWidth) / 2)
    //         {
    //             movingLeft = true;
    //         }
    //     }
    // }
}