using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 拖拽和生成管理器
/// 处理游戏对象的拖拽和生成逻辑
/// </summary>
public class DragAndSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 可拖拽物体的预制体
    /// </summary>
    [SerializeField]
    private GameObject draggablePrefab;

    /// <summary>
    /// UI矩形变换组件
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Canvas组件引用
    /// </summary>
    private Canvas canvas;

    /// <summary>
    /// Canvas组组件引用
    /// </summary>
    private CanvasGroup canvasGroup;

    /// <summary>
    /// 图片组件引用
    /// </summary>
    private Image image;

    /// <summary>
    /// 拖拽起始位置
    /// </summary>
    private Vector2 startPos;

    /// <summary>
    /// 初始化组件引用
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 处理拖拽开始事件
    /// </summary>
    /// <param name="eventData">拖拽事件数据</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.isDragingObject = true;
        // 开始拖动时，使图标透明，并阻止与其他 UI 元素的交互
        //canvasGroup.alpha = 0.6f;
        //canvasGroup.blocksRaycasts = false;
        startPos = rectTransform.anchoredPosition;
    }

    /// <summary>
    /// 处理拖拽过程中的事件
    /// </summary>
    /// <param name="eventData">拖拽事件数据</param>
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

    /// <summary>
    /// 处理拖拽结束事件
    /// </summary>
    /// <param name="eventData">拖拽事件数据</param>
    public void OnEndDrag(PointerEventData eventData)
    {

        GameManager.Instance.isDragingObject = false;

        // 结束拖动时，恢复图标透明度，并允许与其他 UI 元素的交互
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;

        // 在拖拽结束的位置生成物体
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f)); // 假设物体生成在距离相机10个单位的位置
        GameObject obj =  Instantiate(draggablePrefab, spawnPosition, Quaternion.identity);

        //if ( image.sprite !=null )
        //    obj.GetComponent<SpriteRenderer>().sprite = image.sprite;
        //obj.GetComponent<Rigidbody2D>().gravityScale = hasGravity ? 1f : 0f; // 是否具有重力
        rectTransform.anchoredPosition = startPos;
    }
}