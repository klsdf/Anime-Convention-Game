//作者：闫辰祥
//创建时间: 2024年12月7日

using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
public enum AssertType
{
    飞船,
    传送门,
    书,
    星星,
    音乐,
    灯
}

[System.Serializable]
public class AssertItem
{
    public AssertType assertType;
    public bool isUnlocked;
    public Sprite icon;
    public GameObject prefab;
}

public class AssertController : Singleton<AssertController>
{
    // 存储所有道具的解锁状态
    public List<AssertItem> assertItems = new List<AssertItem>();
  
    private void Awake()
    {
        // 从存档加载已解锁道具
        LoadUnlockedItems();
    }

    // 解锁道具
    public void UnlockItem(AssertType itemId)
    {
        var item = assertItems.Find(x => x.assertType == itemId);
        if (item != null)
        {
            item.isUnlocked = true;
            SaveUnlockedItems();
        }
        else
        {
            var newItem = new AssertItem { assertType = itemId, isUnlocked = true };
            assertItems.Add(newItem);
            SaveUnlockedItems();
        }
    }

    // 检查道具是否解锁
    public bool IsItemUnlocked(string itemId)
    {
        if (System.Enum.TryParse(itemId, out AssertType type))
        {
            var item = assertItems.Find(x => x.assertType == type);
            return item != null && item.isUnlocked;
        }
        return false;
    }

    /// <summary>
    /// 获取所有已解锁道具
    /// </summary>
    /// <returns> 所有已解锁道具 </returns>
    public List<AssertItem> GetAllUnlockedItems()
    {
        return assertItems.FindAll(x => x.isUnlocked);
    }

    

      // 保存解锁状态到json
    [Sirenix.OdinInspector.Button("保存道具状态")]
    private void SaveUnlockedItems()
    {
        Debug.Log("保存成功");
        string json = JsonUtility.ToJson(new SerializableAssertItems { items = assertItems });
        PlayerPrefs.SetString("UnlockedItems", json);
        PlayerPrefs.Save();
    }

    // 从json加载解锁状态
    [Sirenix.OdinInspector.Button("加载道具状态")]
    private void LoadUnlockedItems()
    {
        if (PlayerPrefs.HasKey("UnlockedItems"))
        {
            string json = PlayerPrefs.GetString("UnlockedItems");
            SerializableAssertItems data = JsonUtility.FromJson<SerializableAssertItems>(json);
            assertItems = data.items;
            Debug.Log("加载成功");
        }else
        {
            SaveUnlockedItems();
            LoadUnlockedItems();
        }
    }

    [System.Serializable]
    private class SerializableAssertItems
    {
        public List<AssertItem> items;
    }
}

