using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemPrefab; // 要生成的物体预制件
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public bool hasGravity; // 是否具有重力

    private Image image;

    private Vector2 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 开始拖动时，使图标透明，并阻止与其他 UI 元素的交互
        //canvasGroup.alpha = 0.6f;
        //canvasGroup.blocksRaycasts = false;
        startPos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {

        // 拖动时，更新图标位置
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        else
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out pos);
            rectTransform.anchoredPosition = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 结束拖动时，恢复图标透明度，并允许与其他 UI 元素的交互
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;

        // 在拖拽结束的位置生成物体
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f)); // 假设物体生成在距离相机10个单位的位置
        GameObject obj =  Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = image.sprite;
        obj.GetComponent<Rigidbody2D>().gravityScale = hasGravity ? 1f : 0f; // 是否具有重力
        rectTransform.anchoredPosition = startPos;
    }
}