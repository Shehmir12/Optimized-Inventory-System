using UnityEngine;

public class InventoryScriptableManager
{ 
    public enum ItemType { Resources, Equipment, Quest }

    
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Accessory 
    } 
    public class Item : ScriptableObject
    {
        public string itemName;
        public bool Stackable;
        public GameObject prefab;
        public ItemType itemType;
        public Sprite icon;
        public int maxStackSize;
        public int strengthBonus;
        public int healthBonus;
    }
     
    [CreateAssetMenu(fileName = "Resources Item", menuName = "Inventory System/ResourcesItem")]
    public class StackableItems : Item
    {
        public StackableItems()
        {
            itemType = ItemType.Resources;
        }
    }
     
    [CreateAssetMenu(fileName = "Equipment Item", menuName = "Inventory System/EquipmentItem")]
    public class EquipmentItems : Item
    {
        public EquipmentType equipmentType;

        public EquipmentItems()
        {
            itemType = ItemType.Equipment; 
        }
    }

    [CreateAssetMenu(fileName = "Quest Item", menuName = "Inventory System/QuestItem")]
    public class QuestItems : Item
    {
        public QuestItems()
        {
            itemType = ItemType.Quest;
        }
    }
}
