using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public int attachedPlayer;

    // private ProjectileScriptableObject primaryData;
    // private ProjectileScriptableObject secondaryData;

    public SpriteRenderer primarySprite = null;
    public SpriteRenderer secondarySprite = null;


    private void OnEnable()
    {
        Player.UpdateInventory += LoadSprite;
    }

    private void OnDisable()
    {
        Player.UpdateInventory -= LoadSprite;
    }

    void Start()
    {
        primarySprite.sprite = null;
        secondarySprite.sprite = null;
    }

    private void LoadSprite (ProjectileScriptableObject primaryData, ProjectileScriptableObject secondaryData, int playerId)
    {
        if (playerId != attachedPlayer)
        {
            return;
        }
        primarySprite.sprite = primaryData != null ? primaryData.spriteLarge : null;
        secondarySprite.sprite = secondaryData != null ? secondaryData.spriteSmall : null;
    }
}
