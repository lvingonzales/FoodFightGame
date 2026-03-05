using UnityEngine;

public class MovingEnemy : Enemy
{
    public float distance = 2.5f;
    
    private float direction = 1f;

    private Vector3 startPosition;

    private float xTarget;

    private float currentTime;

    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = friction;
        retarget();
    }

    // void Update()
    // {
    //     currentTime += Time.deltaTime * speed * friction;
    //     float x = Mathf.Sin(currentTime) * distance;
    //     transform.position = new Vector3(startPosition.x + x, transform.position.y, transform.position.z);
    // }
    
    private void retarget ()
    {
        // xTarget = startPosition + (distance * direction);
    }

    void FixedUpdate()
    {
        
        if(transform.position.x != xTarget)
        {
            rb.AddForce(new Vector2(direction * speed, 0), ForceMode2D.Impulse);
        } else
        {
            direction *= -1f;
            retarget();
        }
    }
}
