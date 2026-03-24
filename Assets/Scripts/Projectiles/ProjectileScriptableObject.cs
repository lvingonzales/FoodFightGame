using UnityEngine;

[CreateAssetMenu(fileName= "New Projectile Type", menuName = "Projectiles/ProjectileType")]
public class ProjectileScriptableObject : ScriptableObject
{
    [Header("Base Settings")]
    public float baseForce = 10f;
    public float mass = 1f;
    public int hitValue = 10;
    public int ammoWeight = 1;


    public float effectDuration;
    public EffectTypes effectType;
        
    public string prefabName;
    public GameObject prefab;
    
    public Sprite spriteLarge;
    public Sprite spriteSmall;
    public Sprite pileSprite;
    
}
