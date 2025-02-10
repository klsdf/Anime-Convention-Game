//作者：闫辰祥
//创建时间：2025/1/15
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 物品保存控制器,所有的柜子，箱子都是由它控制，玩家的背包也是靠这个控制。
/// </summary>
public class ItemSaveController : Singleton<ItemSaveController>
{
    public GameObject UIPanel;
    public Button closeBtn;
    public SaveItemArea saveItemArea;

    private void Awake() {
        closeBtn.onClick.AddListener(CloseUI);
    }

    public void OpenUI(List<SaveableItemData> saveableItemData)
    {
        saveItemArea.UpdateData(saveableItemData);
        UIPanel.SetActive(true);
    }
    public void CloseUI()
    {
        UIPanel.SetActive(false);
    }
}
