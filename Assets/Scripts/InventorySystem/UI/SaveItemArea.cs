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
    public void InitData(SaveableItemDataList saveableItemDatas)
    {
        ClearAllSlots();
        if(saveableItemDatas == null)
        {
            Debug.LogWarning("当前容器的数据为null");
            return;
        }
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

    /// <summary>
    /// 获取保存的物品数据
    /// </summary>
    /// <returns>保存的物品数据列表</returns>
    public SaveableItemDataList GetData()
    {
        SaveableItemDataList saveableItemDatas = new SaveableItemDataList();
        foreach (Transform child in transform)
        {
            Slot slot = child.GetComponent<Slot>();
            if (slot == null)
            {
                Debug.LogError($"SaveItemArea:GetData:槽位{child.name}没有Slot组件");
                continue; // 如果没有Slot组件，跳过这个子对象
            }

            SaveItemUI saveItemUI = slot.item; // 访问Slot的item属性
            if (saveItemUI == null)
            {
                continue; // 如果item为null，跳过这个子对象
            }

            if (saveItemUI.saveableItemData == null)
            {
                Debug.LogError($"SaveItemArea:GetData:槽位{child.name}的物品数据为null");
                continue; // 如果物品数据为null，跳过这个子对象
            }


                var item = saveItemUI.saveableItemData;
        
                saveableItemDatas.Add(item);    
                
  
        }
        return saveableItemDatas;
    }
}