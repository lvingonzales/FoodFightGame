using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    [SerializeField]private ProjectileScriptableObject projectileType;

    protected Rigidbody2D rb;
    protected Vector2 endDestination;

    protected virtual void Awake () 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
    }

    public virtual void Fire(Vector2 direction, Vector2 destination)
    {
        endDestination = destination;
        rb.linearVelocity = direction * projectileType.speed;
    }

    public virtual void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * projectileType.speed;
    }

    protected void FixedUpdate()
    {
        
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
