using System.Collections.Generic;
using UnityEngine;
using static InventoryScriptableManager;


public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int health = 100;
    public int strength = 10;
    public GameObject[] PlayerObjects;
    public Transform leftHandPivot;
    public Transform rightHandPivot;
    public Transform ArmorsHolderPivot; 

    public Item equippedWeapon;
    public Item equippedArmor;
    public Item equippedQuest;

    public delegate void onUpdateStats();
    public static event onUpdateStats OnUpdateStats;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void EquipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Equipment:
                equippedWeapon = item; 
                break;
            case ItemType.Resources:
                equippedArmor = item; 
                break;
            case ItemType.Quest:
                equippedQuest = item;
                break;
        }
        health += item.healthBonus;
        strength += item.strengthBonus;
        UpdatePlayerAppearance(item);
        OnUpdateStats?.Invoke();
    }
    public void UseItem(Item item)
    {
        if (item.itemType == ItemType.Resources || item.itemType == ItemType.Quest || item.itemType == ItemType.Equipment)
        {
            health += item.healthBonus;
            strength += item.strengthBonus;

            Debug.Log($"Used {item.itemName}. Health is now {health}");
        }
        OnUpdateStats?.Invoke();
    }

    private void UpdatePlayerAppearance(Item item)
    {
       
            foreach (GameObject obj in PlayerObjects)
            {
                if (obj != null)
                {
                    if (obj.name == item.itemName)
                    {
                        //Debug.Log("equippedArmor.itemName" + equippedArmor.itemName);
                        obj.SetActive(true);
                    }
                }
            }
           /* if (ArmorsHolderPivot != null && item.prefab != null)
            {
                if (ArmorsHolderPivot.childCount > 0) { Destroy(ArmorsHolderPivot.GetChild(0).gameObject); }
                Debug.Log("equippedWeapon.itemName: " + equippedArmor.itemName);
                GameObject armorObj = Instantiate(item.prefab, ArmorsHolderPivot);
                armorObj.transform.localPosition = Vector3.zero;
                armorObj.transform.localEulerAngles = Vector3.zero;
            }*/
  


        if(item != null && item.prefab != null)
        {
            if (rightHandPivot.childCount > 0) { Destroy(rightHandPivot.GetChild(0).gameObject); }
            Debug.Log("equippedWeapon.itemName: " + equippedWeapon.itemName);
            GameObject weaponObj = Instantiate(item.prefab, rightHandPivot);
            weaponObj.transform.localPosition = Vector3.zero;
            weaponObj.transform.localEulerAngles = Vector3.zero;
        }
          
    }
}
