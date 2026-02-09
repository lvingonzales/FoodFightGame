using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class HUDScript : MonoBehaviour
{
    [SerializeField]private PlayerInteraction player;
    public TextMeshProUGUI ammoType;
    public TextMeshProUGUI ammoCount;

    private void onEnable()
    {
        PlayerInteraction.onPlayerInteraction += refreshUi;
    }

    private void onDisable()
    {
        PlayerInteraction.onPlayerInteraction -= refreshUi;
    }

    private void refreshUi(PlayerInteraction playerInteraction)
    {
        setAmmoCountDisplay();
        setAmmoTypeDisplay();
    }

    public void setAmmoTypeDisplay ()
    {
        string currentAmmoType = player.getAmmoType();

        if (currentAmmoType == null)        
        {
            ammoType.text = "No Fruit";
        } else
        {
            ammoType.text = currentAmmoType;
        }
    }

    public void setAmmoCountDisplay ()
    {
        int currentAmmoCount = player.getAmmoCount();

        if(currentAmmoCount == 0)
        {
            ammoCount.text = "0/0";
        } else
        {
            ammoCount.text = currentAmmoCount + "/" + currentAmmoCount;
        }
    }
}
