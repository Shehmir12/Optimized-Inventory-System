using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    public InventoryManager inventory;
    public GameObject inventoryItemPrefab;
    public Transform inventoryPanel;
    public Transform equippedInventoryPanel;
    public Transform questInventoryPanel;
    public Transform resourcesInventoryPanel;

    private List<InventoryItem> inventoryItemsUIs = new List<InventoryItem>();
    private List<InventoryGroups> EquippedinventoryItemsUI = new List<InventoryGroups>();

    private void Start()
    {
        CreateInventorySlots();
        UpdateInventoryUI();
    }

    private void CreateInventorySlots()
    {
        foreach (var slot in inventory.inventoryGroups)
        {
            GameObject itemUIObj = Instantiate(inventoryItemPrefab, inventoryPanel);
            InventoryItem itemUI = itemUIObj.GetComponent<InventoryItem>();
            itemUI.inventoryGroupItem = slot;
            inventoryItemsUIs.Add(itemUI);
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (var itemUI in inventoryItemsUIs)
        {
            itemUI.UpdateItemsUI();
        }
    }

    public void OnInventoryChanged()
    {
        Debug.Log($"{nameof(OnInventoryChanged)}");
        UpdateInventoryUI();
    }
}
