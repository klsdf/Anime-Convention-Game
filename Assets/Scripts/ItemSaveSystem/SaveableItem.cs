//作者：闫辰祥
//2025.2.7
using System.Collections.Generic;
using UnityEngine;

public class SaveableItem : InteractObjBase
{

   public List<SaveableItemData> saveableItemData;
 
    public override void Interact()
    {
        base.Interact();
        Debug.Log($"Interact {this.gameObject.name}");
        ItemSaveController.Instance.OpenUI(saveableItemData);
    }
}
