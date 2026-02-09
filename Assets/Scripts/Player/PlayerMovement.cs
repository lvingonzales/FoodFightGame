using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float mSpeed = 10f;
    [SerializeField] private Camera mainCamera;
    
    InputAction moveAction;
    Vector2 moveValue;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Movement");
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Apply movement
        rb.linearVelocity = moveValue * mSpeed;
    }
}
