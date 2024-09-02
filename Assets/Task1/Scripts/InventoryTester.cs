using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public InventoryManager playerInventory;
    public InventoryItem testItem;
    public int testAmount = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool itemAdded = playerInventory.AddItems(testItem.inventoryGroupItem.item, testAmount);
            if (itemAdded)
            {
                Debug.Log("Test item added to inventory.");
            }
            else
            {
                Debug.Log("Inventory is full or item couldn't be added.");
            }
        }
    }
}
