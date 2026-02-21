using TMPro;
using UnityEngine;

public class AmmoType : PlayerUI
{
    public override void refreshUi(string fruitName, int currentAmmo, int maxAmmo)
    {
        textField.text = fruitName;
    }
}
