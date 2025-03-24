using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
/// <summary>
/// 保存的物品的数据，用于保存物品的名称和数量
/// </summary>
public class SaveableItemData
{
    /// <summary>
    /// 物品名称
    /// </summary>
    public string itemName;
    /// <summary>
    /// 物品数量
    /// </summary>
    public int itemCount;

    public SaveableItemType itemType;

    // 私有字段，隐藏 CropsData
    public CropsData _cropData;

    /// <summary>
    /// 是否是种子
    /// </summary>
    /// <returns></returns>
    public bool isSeed(){
        return itemType == SaveableItemType.Seed;
    }
    
    

}

public enum SaveableItemType
{
    Seed,
    Crop,
    Tool,
    Furniture,
}




[Serializable]
public class SaveableItemDataList
{
    [SerializeField]
    private List<SaveableItemData> saveableItemDataList = new List<SaveableItemData>();

    public void Add(SaveableItemData saveableItemData)
    {
        saveableItemDataList.Add(saveableItemData);
    }

    public SaveableItemData this[int index]
    {
        get => saveableItemDataList[index];
        set => saveableItemDataList[index] = value;
    }

    public int Count => saveableItemDataList.Count;
}
