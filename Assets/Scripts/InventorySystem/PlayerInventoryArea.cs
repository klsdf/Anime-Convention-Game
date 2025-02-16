//作者：闫辰祥
//2025.2.7
using System.Collections.Generic;
using UnityEngine;

// 玩家背包区域类
public class PlayerInventoryArea : MonoBehaviour
{

    /// <summary>
    /// 更新背包数据
    /// </summary>
    /// <param name="inventoryItems">背包物品数据列表</param>
    public void UpdateInventory(List<SaveableItemData> inventoryItems)
    {
        ClearAllSlots(); // 清空所有槽位
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (i < transform.childCount) // 确保有足够的槽位
            {
                Slot slot = transform.GetChild(i).GetComponent<Slot>();
                if (slot != null)
                {
                    slot.CreateItem(inventoryItems[i]); // 创建物品
                }
            }
        }
    }

    /// <summary>
    /// 清空所有槽位
    /// </summary>
    private void ClearAllSlots()
    {
        foreach (Transform child in transform)
        {
            print($"销毁{child.name}"); // 输出销毁信息
            child.GetComponent<Slot>().ClearItem(); // 清空物品
        }
    }
}
