using UnityEngine;
using static InventoryScriptableManager;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public InventoryGroups inventoryGroupItem;
    public Image icon;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI itemNameText;
    InventoryManager inventory;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    InventoryUIManager inventoryUI;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<InventoryManager>();
        inventoryUI = FindObjectOfType<InventoryUIManager>();
        UpdateItemsUI();
    }

    public void UpdateItemsUI()
    {
        if (inventoryGroupItem.item != null)
        {
            icon.sprite = inventoryGroupItem.item.icon;
            icon.enabled = true;
            itemNameText.text = inventoryGroupItem.item.name;
            if (inventoryGroupItem.item.Stackable)
            {
                amountText.text = inventoryGroupItem.amount > 1 ? inventoryGroupItem.amount.ToString() : "";
                //amountText.enabled = false; 
            }
            else
            {
                amountText.text = inventoryGroupItem.amount > 1 ? inventoryGroupItem.amount.ToString() : "";
            }

        }
        else
        {
            //transform.gameObject.name = string.Empty;
            //Debug.Log("Destroyed" + inventoryGroupItem);
            /* if (inventoryGroupItem.item.Stackable)
             {

             }*/
            inventory.CleanNullItems();
            inventory.inventoryGroups.Remove(inventoryGroupItem);
            Destroy(transform.gameObject);



            //icon.enabled = false;
            //amountText.enabled = false;


        }
    }
    bool equipped = false;
    public void OnItemClicked()
    {
        if (equipped) return;
        if (inventoryGroupItem.item != null)
        {
            if (inventoryGroupItem.item.itemType == ItemType.Equipment)
            {
                if (inventoryGroupItem.item.Stackable)
                {
                    Player.Instance.UseItem(inventoryGroupItem.item);
                    inventoryGroupItem.amount--;
                    if (inventoryGroupItem.item.Stackable) if (inventoryGroupItem.amount <= 0)
                        {
                            inventoryGroupItem.ClearGroup();
                        }
                    UpdateItemsUI();
                }
                else
                {
                    Player.Instance.EquipItem((EquipmentItems)inventoryGroupItem.item);
                    transform.SetParent(inventoryUI.equippedInventoryPanel, false);
                    equipped = true;
                    Debug.Log($"if inventoryGroupItem.item name is {inventoryGroupItem.item.itemName}");
                }
            }
            else if (inventoryGroupItem.item.itemType == ItemType.Resources)
            {
                if (inventoryGroupItem.item.Stackable)
                {
                    Debug.Log($"else inventoryGroupItem.item name is {inventoryGroupItem.item.itemName}");
                    Player.Instance.UseItem(inventoryGroupItem.item);
                    inventory.EquippedinventoryGroup.Add(inventoryGroupItem);
                    //transform.SetParent(inventoryUI.resourcesInventoryPanel, false);
                    if (inventoryGroupItem.item.Stackable) inventoryGroupItem.amount--;
                    if (inventoryGroupItem.item.Stackable) if (inventoryGroupItem.amount <= 0)
                        {
                            inventoryGroupItem.ClearGroup();
                        }
                    UpdateItemsUI();
                }
                else
                {
                    /* Debug.Log($"else inventoryGroupItem.item name is {inventoryGroupItem.item.itemName}");
                     Player.Instance.EquipItem((EquipmentItems)inventoryGroupItem.item);
                     inventory.EquippedinventoryGroup.Add(inventoryGroupItem);
                     transform.SetParent(inventoryUI.resourcesInventoryPanel, false);
                     if (inventoryGroupItem.item.Stackable) inventoryGroupItem.amount--;
                     if (inventoryGroupItem.item.Stackable) if (inventoryGroupItem.amount <= 0)
                         {
                             inventoryGroupItem.ClearGroup();
                         }
                     UpdateItemsUI();*/
                }
            }
            else if (inventoryGroupItem.item.itemType == ItemType.Quest)
            {
                if (inventoryGroupItem.item.Stackable)
                {
                    Debug.Log($" inventoryGroupItem.item name is {inventoryGroupItem.item.itemName}");
                    Player.Instance.UseItem(inventoryGroupItem.item);
                    inventory.EquippedinventoryGroup.Add(inventoryGroupItem);
                    //transform.SetParent(inventoryUI.questInventoryPanel, false);
                    inventoryGroupItem.amount--;
                    if (inventoryGroupItem.amount <= 0)
                    {
                        inventoryGroupItem.ClearGroup();
                    }
                    UpdateItemsUI();
                }
                else
                {
                    Debug.Log($"else inventoryGroupItem.item name is {inventoryGroupItem.item.itemName}");
                    Player.Instance.UseItem(inventoryGroupItem.item);
                    inventory.EquippedinventoryGroup.Add(inventoryGroupItem);
                    transform.SetParent(inventoryUI.questInventoryPanel, false);
                    equipped = true;
                    /*inventoryGroupItem.amount--;
                    if (inventoryGroupItem.amount <= 0)
                    {
                        inventoryGroupItem.ClearGroup();
                    }*/
                    UpdateItemsUI();
                }
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItemUI = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItemUI != null)
        {
            if (inventoryGroupItem.item != null && inventoryGroupItem.item.Stackable /*&& (inventoryGroupItem.item.itemType == ItemType.Resources || inventoryGroupItem.item.itemType == ItemType.Quest  ||
                inventoryGroupItem.item.itemType == ItemType.Equipment)*/ && droppedItemUI.inventoryGroupItem.item == inventoryGroupItem.item)
            { 
                inventoryGroupItem.amount += droppedItemUI.inventoryGroupItem.amount;
                droppedItemUI.inventoryGroupItem.ClearGroup();
                //playerUIManager.OnInventoryChanged();
            }
            else
            {
                InventoryGroups tempSlot = new InventoryGroups
                {
                    item = inventoryGroupItem.item,
                    amount = inventoryGroupItem.amount
                };
                inventoryGroupItem.item = droppedItemUI.inventoryGroupItem.item;
                inventoryGroupItem.amount = droppedItemUI.inventoryGroupItem.amount;
                droppedItemUI.inventoryGroupItem.item = tempSlot.item;
                droppedItemUI.inventoryGroupItem.amount = tempSlot.amount;
            }
             
            UpdateItemsUI();
            droppedItemUI.UpdateItemsUI();
        }
    }
}
