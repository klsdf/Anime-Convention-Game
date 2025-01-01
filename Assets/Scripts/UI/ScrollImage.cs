using UnityEngine;
using DG.Tweening;
//作者：闫辰祥
//创建时间: 2024/8/13

public class ScrollImage : MonoBehaviour
{
    public float speed = 0.5f; // 滚动速度
    private RectTransform rectTransform;
    private float canvasWidth;
    private bool movingLeft = false;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        float imageWidth = rectTransform.rect.width;
        canvasWidth = rectTransform.parent.GetComponent<RectTransform>().rect.width;
        
        var scrollAnim = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, 
        rectTransform.anchoredPosition-new Vector2(imageWidth-canvasWidth, 0), 10/speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        scrollAnim.Restart();
    }
}