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
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
        }
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
