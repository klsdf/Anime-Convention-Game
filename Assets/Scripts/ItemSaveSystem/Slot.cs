//作者：闫辰祥
//时间：2025.2.7

using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public SaveItemUI item; // 物品

    public bool IsEmpty => item == null;

    public void SetItem(SaveItemUI newItem)
    {
        item = newItem;
        if (item != null)
        {
            print($"将{item.name}设置到了{name}");
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("哈人喵！给Slot设置的数据为空！");
        }
    }

    public void CreateItem(SaveableItemData saveableItemData)
    {
        GameObject temp = Resources.Load<GameObject>("UI/Items/ItemPrefab");
        temp = Instantiate(temp, transform.position, Quaternion.identity);
        temp.transform.SetParent(transform);
        item = temp.GetComponent<SaveItemUI>();

        item.UpdateItem(saveableItemData);
    }



    public void ClearItem()
    {
        if (item != null)
        {
            Destroy(item.gameObject);
        }

        item = null;
    }
}
