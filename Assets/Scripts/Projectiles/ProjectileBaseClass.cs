using UnityEngine;
using System;

public class ProjectileBaseClass : MonoBehaviour
{
    [SerializeField]protected ProjectileScriptableObject projectileType;
    public static event Action<int> OnPlayerHit;
    private int parentPlayerId;

    protected Rigidbody2D rb;
    protected Vector2 endDestination;

    protected virtual void Awake () 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // public virtual void Fire(Vector2 direction, int playerId)
    // {
    //     rb.linearVelocity = direction * projectileType.speed;
        
    // }
    public virtual void Launch(Vector2 direction, int playerId)
    {
        rb.linearVelocity = direction * projectileType.speed;
        rb.linearDamping = projectileType.speed * .01f;
        parentPlayerId = playerId;
    }

    protected void FixedUpdate()
    {
        
    }

     protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Enemy hitEnemy = other.GetComponent<Enemy>();

        if (hitEnemy == null) return;
        if (hitEnemy.enemyId == parentPlayerId) return;

        hitEnemy.ApplyEffect(projectileType.effectType, projectileType.effectDuration);
        hitEnemy.setScore(projectileType.hitValue);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }

}
