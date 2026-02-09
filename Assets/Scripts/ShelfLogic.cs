using System.Collections;
using UnityEngine;

public class ShelfLogic : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 3;
    [SerializeField] private float refillTime = 2f;
    public ProjectileScriptableObject[] fruitList;
    private ProjectileScriptableObject currentAmmoType;
    
    private int currentAmmo = 0;
    private bool isRefilling = false;
    public bool isLoaded = false;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        RefillShelf();
    }

    private void ChooseFruit()
    {
        currentAmmoType = fruitList[0];
    }

    private void RefillShelf ()
    {
        if(!isRefilling)
        {
            isRefilling = true;
            StartCoroutine(RefillCoroutine());
        }
    }
    private IEnumerator RefillCoroutine()
    {
        ChooseFruit();
        yield return new WaitForSeconds(refillTime);
        // currentAmmo = maxAmmo;
        spriteRenderer.color = Color.green;
        isLoaded = true;
        isRefilling = false;
    }

    public ProjectileScriptableObject GetAmmo()
    {
        // int ammoToGive = currentAmmo;
        // if(currentAmmo == 0)
        // {
        //     Debug.Log("Nothing to give!");
        // }
        // currentAmmo = 0;
        if(isRefilling)
        {
            Debug.Log("Nothing to Give");
            return null;
        }
        spriteRenderer.color = Color.red;
        isLoaded = false;

        RefillShelf();

        return currentAmmoType;
    }
}
