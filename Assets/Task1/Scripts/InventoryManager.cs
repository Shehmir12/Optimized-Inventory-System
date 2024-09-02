using UnityEngine;
using System;
using static InventoryScriptableManager;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryGroups> inventoryGroups = new List<InventoryGroups>(); 
    public List<InventoryGroups> EquippedinventoryGroup = new List<InventoryGroups>();

    public bool AddItems(Item item, int amountToAdd)
    {
        if (item == null) { Debug.Log($"Item {item.itemName} is Null"); return false; }
        try
        {
           /* if (item.Stackable)
            {
                foreach (InventoryGroups group in inventoryGroups)
                {
                    if (group.item == item && group.amount < item.maxStackSize)
                    {
                        existingGroup = group;
                        //Debug.Log("existing Group Assigned"); 
                        break;
                    }
                }
            }*/
            if (item.itemType == ItemType.Resources || item.itemType == ItemType.Quest || item.itemType == ItemType.Equipment)
            { 
                InventoryGroups existingGroup = null;

                foreach (InventoryGroups group in inventoryGroups)
                {
                    if (group.item == item && group.amount < item.maxStackSize)
                    {
                        existingGroup = group;
                        //Debug.Log("existing Group Assigned"); 
                        break;
                    }
                }

                if (existingGroup != null)
                {
                    existingGroup.amount += amountToAdd;
                    if (existingGroup.amount > item.maxStackSize)
                    {
                        int overflow = existingGroup.amount - item.maxStackSize;
                        existingGroup.amount = item.maxStackSize;
                       // Debug.Log($"Overflow Amount {overflow} Deducted ");
                        return AddItems(item, overflow);
                    }
                    return true;
                }

                return false; 
            }
            InventoryGroups emptyGroup = inventoryGroups.Find(x => x.IsEmpty);
            
            if (emptyGroup != null)
            {
                emptyGroup.item = item;
                emptyGroup.amount = amountToAdd;
                return true;  
            }
            else
            {
                emptyGroup.item.name = string.Empty;
            }
        }
        catch (Exception e)
        { 
            Debug.LogException(e);
        } 
        return false;  
    }

    public void RemoveItems(Item item, int amountToRemove)
    {
        InventoryGroups removeableGroup = inventoryGroups.Find(x => x.item == item);

        if (removeableGroup != null)
        {
            removeableGroup.amount -= amountToRemove;
            if(removeableGroup.amount <= 0)
            {
                removeableGroup.ClearGroup();
            }
        }
    }
    public void AddEquippedItems(Item item, int amountToAdd)
    {
       // EquippedinventoryGroup.Insert(item, amountToAdd);
    }
    public void CleanNullItems()
    {
        inventoryGroups.RemoveAll(item => item.item == null);
    }
}
[Serializable]
public class InventoryGroups
{
    public Item item;
    public int amount;

    public bool IsEmpty => item == null;

    public void ClearGroup()
    {
        item = null;
        amount = 0;
    }
   
}
