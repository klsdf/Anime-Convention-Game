using UnityEngine;

public class SaveableItem : InteractObjBase
{
 
    public override void Interact()
    {
        base.Interact();
        Debug.Log($"Interact {this.gameObject.name}");
        ItemSaveController.Instance.OpenUI();
    }
}
