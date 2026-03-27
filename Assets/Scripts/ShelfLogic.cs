using System.Collections;
using UnityEngine;

public class ShelfLogic : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 3;
    [SerializeField] private float refillTime = 2f;
    public ProjectileScriptableObject[] fruitList;
    private ProjectileScriptableObject currentAmmoType;
    public ProjectileScriptableObject fruitData { get; private set; } = null;
    [SerializeField] private SpriteRenderer shelfVisual;
    
    private int currentAmmo = 0;
    private bool isRefilling = false;
    public bool isLoaded = false;
    private void Start()
    {
        RefillShelf();
    }

    private void ChooseFruit()
    {
        if(fruitList.Length == 0)
        {
            Debug.LogError("No fruits assigned to shelf!");
            return;
        }
        fruitData = fruitList[Random.Range(0, fruitList.Length)];
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
        shelfVisual.sprite = fruitData.pileSprite;
        // currentAmmo = maxAmmo;
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
        shelfVisual.sprite = null;
        isLoaded = false;

        RefillShelf();

        return fruitData;
    }
}
