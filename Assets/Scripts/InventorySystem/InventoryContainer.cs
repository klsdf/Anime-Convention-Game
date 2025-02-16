//作者：闫辰祥
//2025.2.7
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 存储物体的容器，只要挂载这个脚本，就认为对象是可以存储物体的
/// </summary>
public class InventoryContainer : InteractObjBase
{
    /// <summary>
    /// 可保存物品数据列表
    /// </summary>
    [SerializeField]
    private SaveableItemDataList initData = new SaveableItemDataList(); 

    private void Awake() {
        InventorySystemDataController.Instance.TryRegisterInventoryContainer(this,initData);
    }

    
    public override void Interact()
    {
        base.Interact();
        ItemSaveController.Instance.OpenUI(this);

    }
}
