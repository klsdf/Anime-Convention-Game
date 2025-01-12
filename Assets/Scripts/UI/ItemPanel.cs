//作者：闫辰祥
//创建时间: 2024年12月8日

using UnityEngine;
using System.Collections.Generic;


public class ItemPanel : MonoBehaviour {
    private void OnEnable() {
        List<AssertItem> items = AssertController.Instance.GetAllUnlockedItems();
        foreach (AssertItem item in items)
        {
            if (item == null)
            {
                Debug.LogError("AssertItem is null");
                continue;
            }
            
            if (item.prefab == null)
            {
                Debug.LogError($"Prefab is null for item");
                continue;
            }

            GameObject uiPrefab = Resources.Load<GameObject>("UI/Image");
            if (uiPrefab == null)
            {
                Debug.LogError("Failed to load UI/Image prefab");
                continue;
            }

            GameObject itemObj = Instantiate(uiPrefab);
            if (itemObj == null)
            {
                Debug.LogError("Failed to instantiate UI prefab");
                continue;
            }

            var dragAndSpawn = itemObj.GetComponentInChildren<DragAndSpawn>();
            if (dragAndSpawn == null)
            {
                Debug.LogError("DragAndSpawn component not found on UI prefab");
                Destroy(itemObj);
                continue;
            }

            dragAndSpawn.Init(item);
            itemObj.transform.SetParent(transform);
        }
    }
    private void OnDisable() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}