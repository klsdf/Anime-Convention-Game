using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemPrefab; // 要生成的物体预制件

    private RectTransform rectTransform;

    private Image icon;
    private Vector2 offset; // 新增：用于存储鼠标点击位置与物体位置的偏移
    private Vector2 startAnchoredPos; // 改用 Vector2 存储锚点位置

    public void Init(AssertItem item)
    {
        if (item.icon != null)
        {
            icon.sprite = item.icon;
        }
        itemPrefab = item.prefab;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        startAnchoredPos = rectTransform.anchoredPosition; // 保存初始锚点位置
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.isDragingObject = true;
        // 使用 anchoredPosition
        offset = (Vector2)rectTransform.anchoredPosition - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 使用 anchoredPosition
        rectTransform.anchoredPosition = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Instance.isDragingObject = false;

        // 在拖拽结束的位置生成物体
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
        GameObject obj = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        
        // 重置到初始锚点位置
        rectTransform.anchoredPosition = startAnchoredPos;
    }
}