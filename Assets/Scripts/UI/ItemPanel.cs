//作者：闫辰祥
//创建时间: 2024年12月8日

using UnityEngine;
using System.Collections.Generic;


public class ItemPanel : MonoBehaviour {
    

    private void OnEnable() {
        List<AssertItem> items = AssertController.Instance.GetAllUnlockedItems();
        foreach (AssertItem item in items)
        {
            GameObject itemObj = Resources.Load<GameObject>("UI/Image");

            itemObj =  Instantiate(itemObj);
            itemObj.GetComponentInChildren<DragAndSpawn>().Init(item);
            itemObj.transform.SetParent(transform);
        }
    }
    private void OnDisable() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}