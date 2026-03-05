using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    public int enemyId { get; private set; }
    public int enemyScore = 0;

    public float speed = 1f;
    private float maxSpeed = 3f;
    public float friction = 3f;
    protected float acceleration = 500f;
    protected float deceleration = 0.001f;

    public TextMeshProUGUI textField;

    public static event Action<int> OnHit;

    public void setScore(int hitValue)
    {
        enemyScore = enemyScore + hitValue;
        textField.text = enemyScore.ToString();
        // OnHit?.Invoke(enemyScore);
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
        case EffectTypes.Burn:
            StartCoroutine(Burn(duration));
            break;
    }
    }

    private bool isStunned = false;
    
    IEnumerator Stun(float duration)
    {
        if (isStunned) yield break;
        Debug.Log("Stunned!");
        isStunned = true;
        float originalSpeed = speed;
        speed = 0f;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        isStunned = false;
    } 

    private bool isSlippery = false;
    IEnumerator Slippery(float duration)
    {
        if (isSlippery) yield break;
        Debug.Log("Slippery!");
        isSlippery = true;
        float originalFriction = friction;
        friction = 0.5f;

        yield return new WaitForSeconds(duration);

        friction = originalFriction;
        isSlippery = false;
    } 

    private bool isBurning = false;
    IEnumerator Burn(float duration)
    {
        if(isBurning) yield break;
        isBurning = true;
        Debug.Log(enemyId + "- isBurning: " + isBurning);

        for (int i = 0; i < duration; i++)
        {
            Debug.Log("Burn tick");
            setScore(10);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(duration);

        isBurning = false;
    } 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
