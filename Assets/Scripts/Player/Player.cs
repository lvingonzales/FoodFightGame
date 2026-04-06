using UnityEngine;
using UnityEngine.InputSystem;
using System;

public partial class Player : MonoBehaviour 
{
    // public PlayerInteraction playerData;
    public int playerId {get; private set;}
    public Color playerColor;
    public int playerHitPoints;

    public GameObject aimArrow;


    Rigidbody2D rb;
    PlayerInput playerInput;

    private int GetScore()
    {
        return playerHitPoints;
    }

    public void setScore(int addedScore)
    {
        playerHitPoints = playerHitPoints + addedScore;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerId = UnityEngine.Random.Range(0, 10000);
    }

    void Start()
    {
        aimArrow.GetComponent<ArrowColor>().SetArrowColor(playerColor);
        InitMovement();
        playerHitPoints = 0;
    }   

    void Update()
    {
        UpdateParticles();
        UpdateMovement();
    }

    public void AddHitPoints (int points)
    {
        playerHitPoints = playerHitPoints + points;
    }
}
