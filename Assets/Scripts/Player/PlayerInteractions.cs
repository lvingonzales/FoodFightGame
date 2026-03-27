using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    PlayerInput playerInput;

    public ProjectileScriptableObject primaryFruitData = null, secondaryFruitData = null;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnInteract()
    {
        if (shelf == null) return;

        LoadFruit();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out shelf);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (shelf != null)
        {
            shelf = null;
        }
    }

    public ShelfLogic shelf = null;

    void LoadFruit()
    {
        if (primaryFruitData == null)
        {
            primaryFruitData = shelf.fruitData;
            UpdateAmmo();
        } else if (secondaryFruitData == null)
        {
            secondaryFruitData = shelf.fruitData;
        } else
        {
            return;
        }
    }

    void UpdateAmmo ()
    {
        currentAmmo = MAX_AMMO_WEIGHT / primaryFruitData.ammoWeight;
    }

    public int currentAmmo;
    private const int  MAX_AMMO_WEIGHT = 6;

    public void OnFire()
    {
        Debug.Log("triggered Fire Action");
        if (currentAmmo > 0)
        {
            currentAmmo = currentAmmo - 1;
        }

        if (currentAmmo == 0)
        {
            SwitchAmmo();
        }
    }

    void SwitchAmmo()
    {
        if (secondaryFruitData == null)
        {
            primaryFruitData = null;
            return;
        }

        primaryFruitData = secondaryFruitData;
        secondaryFruitData = null;

        UpdateAmmo ();
    }
}
