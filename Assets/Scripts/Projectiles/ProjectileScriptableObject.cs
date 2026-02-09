using UnityEngine;

[CreateAssetMenu(fileName= "New Projectile Type", menuName = "Projectiles/ProjectileType")]
public class ProjectileScriptableObject : ScriptableObject
{
    [Header("Base Settings")]
    public float speed = 10f;
    public int hitValue = 10;
    public int ammoWeight = 1;
        
    public string prefabName;
    public GameObject prefab;
    
}
