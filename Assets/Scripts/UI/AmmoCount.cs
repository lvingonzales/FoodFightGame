using TMPro;
using UnityEngine;

public class AmmoCount : PlayerUI
{
    public override void refreshUi(string fruitName, int currentAmmo, int maxAmmo)
    {
        textField.text = currentAmmo + " / " + maxAmmo;
    }
    
}
