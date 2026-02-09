using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]public string currentFruit = null;
    [SerializeField]private ProjectileScriptableObject currentFruitData = null;

    InputAction fireAction, interactAction;
    private ShelfLogic currentShelf = null;
    [SerializeField] private int ammoCap = 6;
    [SerializeField] public int currentAmmo = 0;
    public GameObject currentFruitPrefab;

    public static event Action<PlayerInteraction> onPlayerInteraction;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        fireAction = InputSystem.actions.FindAction("Fire");
        onPlayerInteraction?.Invoke(this);
    }

    void Update()
    {
        if (fireAction.triggered)
        {
            ThrowFruit();
        }

        if (interactAction.triggered)
        {
            AttemptReload();
        }
    }

    private void ThrowFruit()
    {
        //Check Ammo
        if (currentAmmo > 0)
        {
            Fire(GetMouseDirection());
            currentAmmo = currentAmmo - 1; 
        }

        //Get Mouse position

        //Instantiate Fruit
        
        //Reset Ammo
        if (currentAmmo == 0)
        {
            ResetFruitData();
        }

    }

    private void AttemptReload()
    {
        if(currentFruitData != null || currentShelf == null ||currentShelf.isLoaded == false)
        {
            return;
        } else
        {
            currentFruitData = currentShelf.GetAmmo();
            currentFruit = currentFruitData.prefabName;
            currentAmmo = ammoCap / currentFruitData.ammoWeight;
            currentFruitPrefab = currentFruitData.prefab;
        }
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
        currentFruitData = null;
        currentFruit = null;
        currentFruitPrefab = null;
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
        Debug.Log(fruit);
        fruit.Launch(direction);
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
