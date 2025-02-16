using System.Collections.Generic;
using Sirenix.OdinInspector;
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
    /// 容器名称
    /// </summary>
    [SerializeField]
    public string inventoryContainerName;
    /// <summary>
    /// 可保存物品数据列表
    /// </summary>
    [SerializeField]
    public SaveableItemDataList saveableItemDataList;   
}


[System.Serializable]
public class ListSaveableItemDataList 
{
    public List<InventorySystemData> list = new List<InventorySystemData>();
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
    public ListSaveableItemDataList inventorySystemDataList = new ListSaveableItemDataList();

    private const string INVENTORY_SYSTEM_DATA_KEY = "InventorySystemData";

    private void Start() {
        LoadData();
    }


    [Button("加载数据")]
    private void LoadData()
    {
        if (PlayerPrefs.HasKey(INVENTORY_SYSTEM_DATA_KEY))
        {
            string json = PlayerPrefs.GetString(INVENTORY_SYSTEM_DATA_KEY);
            inventorySystemDataList = JsonUtility.FromJson<ListSaveableItemDataList>(json);
            print("加载inventory数据成功");
        }
        else
        {
            print("没有数据，创建数据");
            SaveData();
            LoadData();
        }
    }

    [Button("保存数据")]
    private void SaveData()
    {

        string json = JsonUtility.ToJson(inventorySystemDataList);
        PlayerPrefs.SetString(INVENTORY_SYSTEM_DATA_KEY, json);
        PlayerPrefs.Save();
        print("保存数据成功！");
    }

    /// <summary>
    /// 尝试注册容器，如果容器不存在，则注册容器
    /// </summary>
    /// <param name="inventoryContainer">容器</param>
    /// <param name="initData">初始数据</param>
    /// <returns>返回是否注册成功</returns>
    public bool TryRegisterInventoryContainer(InventoryContainer inventoryContainer,SaveableItemDataList initData)
    {
        //先加载一下，更新一下数据
        LoadData();
        if (GetInventorySystemData(inventoryContainer) == null)
        {
            StoreOrUpdateItemData(inventoryContainer, initData);
            SaveData();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 存储物体的可保存物品数据
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <param name="saveableItemData">可保存物品数据列表</param>
    public void StoreOrUpdateItemData(InventoryContainer inventoryContainer, SaveableItemDataList saveableItemDataList)
    {
        if (GetInventorySystemData(inventoryContainer) == null)
        {
            InventorySystemData inventorySystemData = new InventorySystemData { inventoryContainerName = inventoryContainer.name, saveableItemDataList = saveableItemDataList }; // 存储或更新数据
            inventorySystemDataList.list.Add(inventorySystemData);
        }
        else
        {
            GetInventorySystemData(inventoryContainer).saveableItemDataList = saveableItemDataList;
        }
        SaveData();
    }

    /// <summary>
    /// 查找物体的可保存物品数据
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <returns>返回对应的可保存物品数据列表</returns>
    public InventorySystemData GetInventorySystemData(InventoryContainer inventoryContainer)
    {
        foreach (var item in inventorySystemDataList.list)
        {
            if (item.inventoryContainerName == inventoryContainer.name)
            {
                return item; // 返回找到的数据
            }
        }
        return null; // 未找到返回null
    }

}