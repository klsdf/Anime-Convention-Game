//作者：闫辰祥
//2025.2.7


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 可以被拖拽的UI元素
/// </summary>
public class SaveItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector3 startPosition;
    private Slot startSlot;

    public void UpdateItem(SaveableItemData saveableItemData)
    {
        Sprite sprite = Resources.Load<Sprite>($"UI/Items/{saveableItemData.itemName}");
        GetComponent<Image>().sprite = sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        startSlot = originalParent.GetComponent<Slot>();
        transform.SetParent(originalParent.root); // 让物品在最高层级
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // 跟随鼠标
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
    }
    /// <summary>
    /// 获取鼠标下的 Slot
    /// </summary>
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
        // print("移动到目标格子");

        newSlot.SetItem(this);
        // startSlot.ClearItem();
    }
    private void OnDestroy() {
        // print("我g了");
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