using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// @file InventoryManager.cs
/// @brief 物品管理器类，负责管理游戏中的所有物品。
/// @author [你的名字]
/// @date [日期]

public class InventoryManager : MonoBehaviour
{
    /// @brief 存储所有物品的列表。
    public Dictionary<ItemType, ItemData> itemDataDic = new Dictionary<ItemType, ItemData>();

    /// @brief 初始化物品管理器。
    /// @details 在游戏开始时调用，初始化物品列表。
    public InventoryData backpack;
    public InventoryData toolbarData;

    /// @brief 单例实例。
    public static InventoryManager Instance { get; private set; }

    /// @brief 初始化物品管理器。
    /// @details 在游戏开始时调用，初始化物品列表。
    public void Awake()
    {
        Instance = this;
        Init();
    }

    /// @brief 初始化物品。
    /// @details 初始化物品数据和库存。
    public void Init()
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Data");
        foreach (ItemData data in itemDataArray)
        {
            itemDataDic.Add(data.type, data);
        }

        backpack = Resources.Load<InventoryData>("Data/backpack");
        toolbarData = Resources.Load<InventoryData>("Data/Toolbar");
    }

    /// @brief 根据物品类型获取物品数据。
    /// @param type 物品类型。
    /// @return 返回对应的物品数据，如果未找到则返回null。
    public ItemData GetDataValue(ItemType type)
    {
        ItemData data;
        bool isSuccess = itemDataDic.TryGetValue(type, out data);
        if (isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("你传递的type" + type + "不存在");
            return null;
        }
    }

    /// @brief 将物品添加到背包。
    /// @param type 物品类型。
    /// @details 根据物品类型将物品添加到背包中。
    public void AddToBag(ItemType type)
    {
        ItemData item = GetDataValue(type);
        if (item == null)
        {
            return;
        }

        foreach (SlotData slotdata in backpack.slotList)
        {
            if (slotdata.item == item && slotdata.CanAddItem())
            {
                slotdata.AddOne();
                return;
            }
        }
        foreach (SlotData slotdata in backpack.slotList)
        {
            if (slotdata.count == 0)
            {
                slotdata.AddItem(item);
                return;
            }
        }
        Debug.LogWarning("你的背包已满");
    }
}
