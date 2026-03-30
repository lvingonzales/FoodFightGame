using System.Collections;
using UnityEngine;

public class ShelfLogic : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 3;
    [SerializeField] private float refillTime = 2f;
    public ProjectileScriptableObject[] fruitList;
    private ProjectileScriptableObject currentAmmoType;
    private ProjectileScriptableObject fruitData;
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
