using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class HUDScript : MonoBehaviour
{
    [SerializeField]private PlayerInteraction player;
    public TextMeshProUGUI ammoTypeText;
    public TextMeshProUGUI ammoCountText;

    private void OnEnable()
    {
        PlayerInteraction.OnPlayerInteraction += refreshUi;

    }

    private void OnDisable()
    {
        PlayerInteraction.OnPlayerInteraction -= refreshUi;

    }

    public void refreshUi(string fruitName, int currentAmmo, int maxAmmo)
    {
        Debug.Log("Refreshed Ui");
        setAmmoCountDisplay(currentAmmo, maxAmmo);
        setAmmoTypeDisplay(fruitName);
    }

    public void setAmmoTypeDisplay (string ammoType)
    {
        if (ammoType == null)        
        {
            ammoTypeText.text = "No Fruit";
        } else
        {
            ammoTypeText.text = ammoType;
        }
    }

    public void setAmmoCountDisplay (int currentAmmo, int maxAmmo)
    {
        if(currentAmmo == 0)
        {
            ammoCountText.text = "0/0";
        } else
        {
            ammoCountText.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
