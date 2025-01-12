using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public int maxStackSize;

    public enum ItemType { Consumable, Equipment, Material }
    public ItemType itemType;

    // Additional properties (e.g., for consumables)
    public int healAmount;  // For consumables (potions, etc.)
}