using UnityEngine;
using UnityEngine.InputSystem;
using System;

public partial class Player : MonoBehaviour 
{
    // public PlayerInteraction playerData;
    public int playerId {get; private set;}
    public Color playerColor;
    public int playerScore;

    public GameObject aimArrow;


    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerId = UnityEngine.Random.Range(0, 10000);
    }

    void Start()
    {
        aimArrow.GetComponent<ArrowColor>().SetArrowColor(playerColor);
        InitMovement();
        playerScore = 0;
    }   

    void Update()
    {
        UpdateParticles();
        UpdateMovement();
    }
}
