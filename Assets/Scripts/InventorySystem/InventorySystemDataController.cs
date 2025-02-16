using System.Collections.Generic;
using UnityEngine;


//作者：闫辰祥
//2025.2.16
/// <summary>
/// 存储系统保存的数据，其他的柜子之类的数据都存在这里
/// </summary>

[System.Serializable]
public class InventorySystemData
{
    /// <summary>
    /// 持有数据的容器的引用
    /// </summary>
    public InventoryContainer inventoryContainer;
    public List<SaveableItemData> saveableItemData;
}


/// <summary>
/// 存储系统保存的数据的控制器
/// </summary>
[System.Serializable]
public class InventorySystemDataController:Singleton<InventorySystemDataController>
{
    /// <summary>
    /// 存储系统保存的数据
    /// </summary>
    [SerializeField]
    public List<InventorySystemData> inventorySystemDataList = new List<InventorySystemData>();



    public void Init()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("InventorySystemData"))
        {
            string json = PlayerPrefs.GetString("InventorySystemData");
            inventorySystemDataList = JsonUtility.FromJson<List<InventorySystemData>>(json);
        }
        else
        {
            SaveData();
            LoadData();
        }
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(inventorySystemDataList);
        PlayerPrefs.SetString("InventorySystemData", json);
        PlayerPrefs.Save();
    }


    /// <summary>
    /// 尝试注册容器，如果容器不存在，则注册容器
    /// </summary>
    /// <param name="inventoryContainer">容器</param>
    /// <param name="initData">初始数据</param>
    /// <returns>返回是否注册成功</returns>
    public bool TryRegisterInventoryContainer(InventoryContainer inventoryContainer,List<SaveableItemData> initData)
    {
        if (GetInventorySystemData(inventoryContainer) == null)
        {
            StoreOrUpdateItemData(inventoryContainer, initData);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 存储物体的可保存物品数据
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <param name="saveableItemData">可保存物品数据列表</param>
    public void StoreOrUpdateItemData(InventoryContainer inventoryContainer, List<SaveableItemData> saveableItemData)
    {
        if (GetInventorySystemData(inventoryContainer) == null)
        {
            InventorySystemData inventorySystemData = new InventorySystemData { inventoryContainer = inventoryContainer, saveableItemData = saveableItemData }; // 存储或更新数据
            inventorySystemDataList.Add(inventorySystemData);
        }
        else
        {
            GetInventorySystemData(inventoryContainer).saveableItemData = saveableItemData;
        }
    }

    /// <summary>
    /// 查找物体的可保存物品数据
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <returns>返回对应的可保存物品数据列表</returns>
    public InventorySystemData GetInventorySystemData(InventoryContainer inventoryContainer)
    {
        foreach (var item in inventorySystemDataList)
        {
            if (item.inventoryContainer == inventoryContainer)
            {
                return item; // 返回找到的数据
            }
        }
        return null; // 未找到返回null
    }

}