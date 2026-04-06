using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    PlayerInput playerInput;
    Player player;
    PlayerAim playerAim;
    float throwModifier = 1.0f;
    public ProjectileScriptableObject primaryFruitData = null, secondaryFruitData = null;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        playerAim = GetComponent<PlayerAim>();
        fireAction = playerInput.actions.FindAction("Fire");
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
            primaryFruitData = shelf.GetAmmo();
            UpdateAmmo();
        } else if (secondaryFruitData == null)
        {
            secondaryFruitData = shelf.GetAmmo();
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

    public void Attack()
    {
        if (primaryFruitData == null) return;

        if (currentAmmo > 0)
        {
            GameObject fruit = Instantiate(primaryFruitData.prefab, playerAim.aimArrow.transform.position, Quaternion.identity);
            fruit.GetComponent<ProjectileBaseClass>().ownerId = player.playerId;
            fruit.GetComponent<ProjectileBaseClass>().Launch(playerAim.GetAimDirection(), throwModifier);

            throwModifier = 1f;
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

    private void Update()
    {
        fireAction.started += ctx =>
        {
            holdStart = Time.time;
        };
        fireAction.canceled += ctx =>
        {
            float heldTime = Time.time - holdStart;
            throwModifier = throwModifier + Mathf.Clamp((Mathf.Round(heldTime * 100f) / 100f), 0f, 1f);
            Attack();
        };
    }

    InputAction fireAction;
    float holdStart;
}
