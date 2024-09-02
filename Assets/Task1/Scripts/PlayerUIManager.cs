using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{  
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI strengthText;

    public Image weaponIcon;
    public Image armorIcon; 

    private void Start()
    {
        UpdatePlayerStatsUI();
    }

    private void OnEnable()
    {
       Player.OnUpdateStats += UpdatePlayerStatsUI;
    }
    private void OnDisable()
    {
        Player.OnUpdateStats -= UpdatePlayerStatsUI;
    }

    public void UpdatePlayerStatsUI()
    {
        healthText.text = $"Health: {Player.Instance.health}";
        strengthText.text = $"Strength: {Player.Instance.strength}";

       /* if (player.equippedWeapon != null)
        {
            weaponIcon.sprite = player.equippedWeapon.icon;
            weaponIcon.enabled = true;
        }
        else
        {
            weaponIcon.enabled = false;
        }

        if (player.equippedArmor != null)
        {
            armorIcon.sprite = player.equippedArmor.icon;
            armorIcon.enabled = true;
        }
        else
        {
            armorIcon.enabled = false;
        }*/
    }
}
