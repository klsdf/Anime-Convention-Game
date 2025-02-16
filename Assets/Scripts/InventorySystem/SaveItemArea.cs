//作者：闫辰祥
//2025.2.7
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveItemArea : MonoBehaviour
{
    /// <summary>
    /// 初始化面板数据
    /// </summary>
    /// <param name="saveableItemDatas">可保存物品数据列表</param>
    public void InitData(List<SaveableItemData> saveableItemDatas)
    {
        ClearAllSlots();
        for (int i = 0; i < saveableItemDatas.Count; i++)
        {
            if (i < transform.childCount) // 确保有足够的Slot
            {
                Slot slot = transform.GetChild(i).GetComponent<Slot>();
                if (slot != null)
                {
                    slot.CreateItem(saveableItemDatas[i]);
                }else{
                    Debug.LogError($"SaveItemArea:UpdateData:槽位{i}为null");
                }
            }
            else
            {
                Debug.LogError($"SaveItemArea:UpdateData:槽位数量不足，槽位数量：{transform.childCount}，物品数量：{saveableItemDatas.Count}");
            }
        }
    }

    private void ClearAllSlots()
    {
        foreach (Transform child in transform)
        {
            // print($"销毁{child.name}");
            child.GetComponent<Slot>().ClearItem();
        }
    }


    public List<SaveableItemData> GetData()
    {
        List<SaveableItemData> saveableItemDatas = new List<SaveableItemData>();
        foreach (Transform child in transform)
        {
            SaveItemUI saveItemUI = child.GetComponent<Slot>().item;
            if (saveItemUI != null)
            {
                saveableItemDatas.Add(saveItemUI.SaveableItemData);
            }
        }
        return saveableItemDatas;
    }
}