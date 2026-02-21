using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public abstract class PlayerUI : MonoBehaviour
{

    protected TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerInteraction.OnPlayerInteraction += refreshUi;

    }

    private void OnDisable()
    {
        PlayerInteraction.OnPlayerInteraction -= refreshUi;

    }

    public abstract void refreshUi (string fruitName, int currentAmmo, int maxAmmo);
}
