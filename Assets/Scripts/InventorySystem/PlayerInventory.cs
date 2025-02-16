using UnityEngine;

/// <summary>
/// 玩家背包
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private SaveableItemDataList initData = new SaveableItemDataList();

    private void Awake() {
        InventorySystemDataController.Instance.TryRegisterInventoryContainer(InventorySystemDataController.PLAYER_INVENTORY_DATA_KEY,initData);
    }
    
}
