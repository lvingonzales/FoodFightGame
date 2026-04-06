using UnityEngine;
using System;

public class ProjectileBaseClass : MonoBehaviour
{
    [SerializeField]protected ProjectileScriptableObject data;
    public GameObject particlesPrefab;
    public static event Action<int> OnPlayerHit;
    public int ownerId;

    protected Rigidbody2D rb;
    protected Vector2 endDestination;

    protected virtual void Awake () 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = data.mass;
    }

    public virtual void Launch(Vector2 direction, float throwModifier)
    {
        //rb.linearVelocity = direction * projectileType.speed;
        //rb.linearDamping = projectileType.speed * .01f;

        float force = data.baseForce * throwModifier;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        rb.AddTorque(force * 2.0f, ForceMode2D.Impulse);
    }

    protected void FixedUpdate()
    {
        
    }

     protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Register Player Hit
        //hitEnemy.ApplyEffect(projectileType.effectType, projectileType.effectDuration);
        //hitEnemy.setScore(projectileType.hitValue);

        if(other.gameObject.TryGetComponent(out Player player))
        {
            if(player.playerId == ownerId)
            {
                return;
            }

            player.AddHitPoints(data.hitValue);
        }

        Debug.Log(other.gameObject);
        SpawnParticles();
        Destroy(gameObject);
    }

    protected virtual void SpawnParticles()
    {
        GameObject particles = Instantiate(
            particlesPrefab,
            transform.position,
            Quaternion.identity
            );
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        ps.Emit(30);

        if (ps != null )
        {
            float duration = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy( particles, duration );
        }
    }
}
