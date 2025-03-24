//作者：闫辰祥
//2025.2.7


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 可以被拖拽的UI元素
/// </summary>
public class SaveItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
   private Transform originalParent;
    private Vector3 startPosition;
    private Slot startSlot;
    private float dragStartTime =-1f; // 用于区分点击和拖拽
    private CanvasGroup canvasGroup;

    [SerializeField]
    public SaveableItemData saveableItemData;

    private void Awake()
    {
        // 添加或获取CanvasGroup组件
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void InitData(SaveableItemData saveableItemData)
    {
        if(saveableItemData == null)
        {
            Debug.LogWarning("当前物品数据为null");
            return;
        }
        this.saveableItemData = saveableItemData;
        Sprite sprite = Resources.Load<Sprite>($"UI/Items/{saveableItemData.itemName}");
        if (sprite == null)
        {
            transform.parent.GetComponent<Slot>().ClearItem();
            return;
        }
        GetComponent<Image>().sprite = sprite;
    }

    // 点击选择逻辑
    public void OnPointerClick(PointerEventData eventData)
    {
         Debug.Log("点击事件触发"); // 测试用
        // 如果拖拽时间超过0.15秒，则不认为是点击
         if (dragStartTime < 0 || Time.time - dragStartTime > 0.15f)
        {
            if (saveableItemData != null && saveableItemData.isSeed())
            {
                PlayerFarming playerFarming = FindObjectOfType<PlayerFarming>();
                if (playerFarming != null)
                {
                    Debug.Log($"选中种子: {saveableItemData.itemName}");
                    playerFarming.SelectSeedFromInventory(saveableItemData);
                }
            }
        }
        else
        {
            Debug.Log("操作被识别为拖拽，忽略点击");
        }
        
        dragStartTime = -1f; // 重置状态
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartTime = Time.time; // 记录拖拽开始时间
        
        startPosition = transform.position;
        originalParent = transform.parent;
        startSlot = originalParent.GetComponent<Slot>();
        transform.SetParent(originalParent.root);
        
        // 临时禁用射线阻挡，允许检测下层对象
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 使用eventData.position更准确
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 恢复射线阻挡
        canvasGroup.blocksRaycasts = true;
        
        Slot targetSlot = GetSlotUnderMouse(eventData);

        if (targetSlot == null) // 没拖到有效格子
        {
            ResetPosition();
            return;
        }

        if (targetSlot.IsEmpty) // 空格子
        {
            MoveToSlot(targetSlot);
        }
        else if (targetSlot != startSlot) // 交换位置
        {
            SwapItems(targetSlot);
        }
        else // 没有移动
        {
            ResetPosition();
        }
        
        if (ItemSaveController.Instance != null)
        {
            ItemSaveController.Instance.onMoveItemUI();
        }
    }

    private Slot GetSlotUnderMouse(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            Slot slot = result.gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }

    private void MoveToSlot(Slot newSlot)
    {
        newSlot.SetItem(this);
        startSlot.ClearItemReference();
    }

    private void SwapItems(Slot targetSlot)
    {
        SaveItemUI targetItem = targetSlot.item;
        startSlot.SetItem(targetItem);
        targetSlot.SetItem(this);
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
        transform.SetParent(originalParent);
    }
}