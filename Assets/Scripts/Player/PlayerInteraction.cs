using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using Unity.VisualScripting;

public partial class Player : MonoBehaviour
{
    [SerializeField]public string currentFruit = null;
    [SerializeField]private ProjectileScriptableObject primaryFruitData, secondaryFruitData;

    InputAction fireAction, interactAction;
    private ShelfLogic currentShelf = null;
    [SerializeField] private int ammoCap = 6;
    [SerializeField] public int currentAmmo = 0;
    private int maxAmmo = 0;


    float throwModifier = 1.0f;
    float holdStart;

    public GameObject currentFruitPrefab;

    public static event Action<ProjectileScriptableObject, ProjectileScriptableObject, int> UpdateInventory;

    void InitInteraction()
    {
        interactAction = playerInput.actions.FindAction("Interact");
        fireAction = playerInput.actions.FindAction("Fire");
    }

    void InteractionCheck()
    {
        fireAction.started += ctx =>
        {
            holdStart = Time.time;
        };
        fireAction.canceled += _ctx =>
        {
            float heldFor = Time.time - holdStart;
            ThrowFruit(heldFor, GetMouseDirection());
        };

        if (interactAction.triggered)
        {
            AttemptReload();
        }
    }

    void ReleaseFire ()
    {
        Debug.Log("Threw" + throwModifier);
        // Throw using modifier
        ThrowFruit(throwModifier, GetMouseDirection());

        // Reset Modifier
        throwModifier = 1.0f;
    }

    private void ThrowFruit(float heldFor, Vector3 mouseDirection)
    {

        //Calculate throwModifier
        throwModifier = throwModifier + Mathf.Clamp((Mathf.Round(heldFor * 100f) / 100f), 0f, 1f);

        //Check Ammo
        if (currentAmmo > 0)
        {
            Fire(GetMouseDirection());
            currentAmmo = currentAmmo - 1;
        }
        if (currentAmmo == 0)
        {
            HandleEmptyAmmo();
        }

        throwModifier = 1f;
    }

    void HandleEmptyAmmo ()
    {
        if(secondaryFruitData == null)
            {
                ResetFruitData();
                UpdateInventory?.Invoke(primaryFruitData, secondaryFruitData, playerId);
                return;
            }
            primaryFruitData = secondaryFruitData;
            secondaryFruitData = null;
            UpdateInventory?.Invoke(primaryFruitData, secondaryFruitData, playerId);

            currentAmmo = ammoCap / primaryFruitData.ammoWeight;
            maxAmmo = currentAmmo;
            currentFruitPrefab = primaryFruitData.prefab;
    }

    private void AttemptReload()
    {
        if(currentShelf == null || currentShelf.isLoaded == false)
        {
            return;
        }

        if(primaryFruitData == null)
        {
            primaryFruitData = currentShelf.GetAmmo();
        } else if(secondaryFruitData == null)
        {
            secondaryFruitData = currentShelf.GetAmmo();
        } else
        {
            return;
        }

            UpdateInventory?.Invoke(primaryFruitData, secondaryFruitData, playerId);

            currentAmmo = ammoCap / primaryFruitData.ammoWeight;
            maxAmmo = currentAmmo;
            currentFruitPrefab = primaryFruitData.prefab;
    }

    // Triggers and Collisions
    private void OnTriggerEnter2D(Collider2D other) {
        ShelfLogic shelf = other.GetComponent<ShelfLogic>();

        if (shelf != null)
        {
            currentShelf = shelf;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        ShelfLogic shelf = other.GetComponent<ShelfLogic>();

        if (shelf != null && shelf == currentShelf)
        {
            currentShelf = null;
        }
    }

    private void ResetFruitData ()
    {
        primaryFruitData = null;
        secondaryFruitData = null;
        currentFruit = null;
        currentFruitPrefab = null;
        maxAmmo = 0;
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        return (mouseWorld - transform.position).normalized;
    }

    private void Fire(Vector2 direction)
    {
        GameObject instance = Instantiate(currentFruitPrefab, transform.position, Quaternion.identity);
        ProjectileBaseClass fruit =  instance.GetComponent<ProjectileBaseClass>();
        fruit.ownerId = playerId;
        Debug.Log(playerId);
        fruit.Launch(direction, throwModifier);
    }

    public string getAmmoType()
    {
        return currentFruit;
    }

    public int getAmmoCount()
    {
        return currentAmmo;
    }
}
