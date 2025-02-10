//作者：闫辰祥
//2025.2.7
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveItemArea : MonoBehaviour
{

    private void Start()
    {

    }
    public void UpdateData(List<SaveableItemData> saveableItemDatas)
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
                }
            }
        }
    }

    private void ClearAllSlots()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Slot>().ClearItem();
        }
    }


}