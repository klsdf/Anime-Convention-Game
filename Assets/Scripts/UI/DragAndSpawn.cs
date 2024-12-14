using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 处理物品的拖拽和生成
/// </summary>
public class DragAndSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemPrefab;

    private RectTransform rectTransform;
    private Image icon;
    private Vector2 offset;
    private Vector2 startAnchoredPos;
    public TMP_Text text;

    public void Init(AssertItem item)
    {
        if (item == null)
        {
            Debug.LogError("AssertItem is null");
            return;
        }

        if (item.icon != null)
        {
            icon.sprite = item.icon;
        }

        if (item.prefab == null)
        {
            Debug.LogError("Prefab is null in AssertItem");
            return;
        }

        itemPrefab = item.prefab;
        text.text = item.assertType.ToString();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        startAnchoredPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.isDragingObject = true;
        offset = (Vector2)rectTransform.anchoredPosition - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Instance.isDragingObject = false;

        // 获取当前场景对象
        GameObject currentScene = SceneController.Instance.GetCurrentSceneObject();
        if (currentScene != null && itemPrefab != null)
        {
            // 计算生成位置
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
            
            // 生成物体并设置父对象为当前场景
            GameObject obj = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            if (obj != null)
            {
                obj.transform.SetParent(currentScene.transform);
            }
            else
            {
                Debug.LogError("Failed to instantiate object");
            }
        }
        else
        {
            Debug.LogWarning("No active scene found or itemPrefab is null");
        }
        
        // 重置UI位置
        rectTransform.anchoredPosition = startAnchoredPos;
    }
}