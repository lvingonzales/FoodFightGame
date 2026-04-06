using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public partial class Player : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    public float speedLimit = 10f;
    public float friction;
    [SerializeField] private Camera mainCamera;
    
    InputAction moveAction;
    Vector2 moveValue;
    void InitMovement()
    {
        // change speed and friction to add slippery
        mSpeed = 50f;
        friction = 1.25f;
        moveAction = playerInput.actions.FindAction("Movement");
        moveAction.Disable();
        rb.linearDamping = speedLimit * friction;
    }

    public void EnableMovement()
    {
        moveAction.Enable();
    }

    public void DisableMovement()
    {
        moveAction.Disable();
        rb.linearVelocity = Vector2.zero;
    }
 
    void UpdateMovement()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveValue * mSpeed);
        rb.linearDamping = speedLimit * friction;

        if(rb.linearVelocity.magnitude > speedLimit)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
        }
    }

    public void ApplyEffect(EffectTypes effect, float duration)
    {
        switch (effect)
    {
        case EffectTypes.Stun:
            StartCoroutine(Stun(duration));
            break;
        case EffectTypes.Slippery:
            StartCoroutine(Slippery(duration));
            break;
    }
    }

    private bool isStunned = false;
    IEnumerator Stun(float duration)
    {
        if (isStunned) yield break;
        Debug.Log("Stunned!");
        isStunned = true;
        float originalSpeed = mSpeed;
        mSpeed = 0f;

        yield return new WaitForSeconds(duration);

        mSpeed = originalSpeed;
        isStunned = false;
    } 

    private bool isSlippery = false;
    IEnumerator Slippery(float duration)
    {
        if (isSlippery) yield break;
        Debug.Log("Slippery!");
        isSlippery = true;
        float originalFriction = friction;
        float originalSpeed = mSpeed;
        friction = 0.25f;
        mSpeed = 20f;

        yield return new WaitForSeconds(duration);

        friction = originalFriction;
        mSpeed = originalSpeed;
        isSlippery = false;
    } 
}
