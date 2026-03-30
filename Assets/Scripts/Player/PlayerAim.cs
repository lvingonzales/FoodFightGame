using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    PlayerInput playerInput;
    public GameObject aimArrow;
    Animator animator;
    private Vector2 stickDirection = Vector2.right;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void UpdateSprite()
    {
        Vector2 direction = GetAimDirection();

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        float arrowAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimArrow.transform.position = transform.position + (Vector3)(direction * 1f);
        aimArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90f);
    }

    Vector2 GetMouseDirection()
    {
        var mouse = playerInput.GetDevice<Mouse>();
        if (mouse == null) return Vector2.right;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        mouseWorld.z = 0f;
        return (mouseWorld - transform.position).normalized;
    }

    public Vector2 GetAimDirection()
    {
        if (playerInput.currentControlScheme == "KeyboardMouse")
            return GetMouseDirection();
        else
            return stickDirection;
    }

    public void OnLook(InputValue value)
    {
        if (playerInput.currentControlScheme != "Gamepad") return;

        Vector2 input = value.Get<Vector2>();
        if (input.magnitude > 0.15f)
            stickDirection = input.normalized;
    }

    private void Update()
    {
        UpdateSprite();
    }
}
