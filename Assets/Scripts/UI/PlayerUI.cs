using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public abstract class PlayerUI : MonoBehaviour
{

    protected TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Player.OnPlayerInteraction += refreshUi;
    }

    private void OnDisable()
    {
        Player.OnPlayerInteraction -= refreshUi;

    }

    public abstract void refreshUi (string fruitName, int currentAmmo, int maxAmmo);

    
}
