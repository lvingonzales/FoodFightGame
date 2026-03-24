using UnityEngine;
using UnityEngine.InputSystem;
using System;

public partial class Player : MonoBehaviour 
{
    // public PlayerInteraction playerData;
    public int playerId {get; private set;}
    public int playerScore;

    Animator animator;

    PlayerInput playerInput;

    private int GetScore()
    {
        return playerScore;
    }

    public void setScore(int addedScore)
    {
        playerScore = playerScore + addedScore;
    }

    void Awake()
    {
        MovementOnAwake();
        playerInput = GetComponent<PlayerInput>();
        playerId = UnityEngine.Random.Range(0, 10000);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        InitInteraction();
        InitMovement();
        playerScore = 0;
    }   

    void UpdateSprite ()
    {
        Vector2 direction = GetMouseDirection();

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    void Update()
    {
        InteractionCheck();
        UpdateMovement();
        UpdateSprite();
    }
}
