using UnityEngine;
using UnityEngine.InputSystem;
public partial class Player : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    private float speedLimit = 6f;
    public float friction;
    [SerializeField] private Camera mainCamera;
    
    InputAction moveAction;
    Vector2 moveValue;
    Rigidbody2D rb;

    void MovementOnAwake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void InitMovement()
    {
        mSpeed = 1f;
        friction = 1f;
        moveAction = InputSystem.actions.FindAction("Movement");
        rb.linearDamping = speedLimit * friction;
    }

    void UpdateMovement()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveValue * mSpeed, ForceMode2D.Impulse);

        if(rb.linearVelocity.magnitude > speedLimit)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
        }
    }
}
