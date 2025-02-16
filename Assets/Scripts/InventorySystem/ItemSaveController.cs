//作者：闫辰祥
//创建时间：2025/1/15
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 物品保存控制器,所有的柜子，箱子都是由它控制，玩家的背包也是靠这个控制。
/// </summary>
public class ItemSaveController : Singleton<ItemSaveController>
{
    public GameObject UIPanel;
    public Button closeBtn;
    public SaveItemArea saveItemArea;
    InventoryContainer currentInventoryContainer;

    // public Action onMoveItemUI;
    public void onMoveItemUI()
    {
        // Debug.Log("onMoveItemUI");
        SaveableItemDataList saveableItemDatas = saveItemArea.GetData();
        InventorySystemDataController.Instance.StoreOrUpdateItemData(currentInventoryContainer, saveableItemDatas);

    }
    private void Awake()
    {
        closeBtn.onClick.AddListener(CloseUI);
    }



    /// <summary>
    /// 打开UI并更新数据
    /// </summary>
    /// <param name="name">物体名称</param>
    /// <param name="saveableItemData">可保存物品数据列表</param>
    public void OpenUI(InventoryContainer inventoryContainer)
    {
        currentInventoryContainer = inventoryContainer;
        InventorySystemData inventorySystemData = InventorySystemDataController.Instance.GetInventorySystemData(inventoryContainer);
        saveItemArea.InitData(inventorySystemData.saveableItemDataList);
        UIPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void CloseUI()
    {
        UIPanel.SetActive(false);
    }

}
