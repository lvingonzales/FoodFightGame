using UnityEngine;
using UnityEngine.InputSystem;
using System;

public partial class Player : MonoBehaviour 
{
    // public PlayerInteraction playerData;
    public int playerId {get; private set;}
    public int playerScore;

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
        playerId = UnityEngine.Random.Range(0, 10000);
    }

    void Start()
    {
        InitInteraction();
        InitMovement();
        playerScore = 0;
    }   

    void Update()
    {
        InteractionCheck();
        UpdateMovement();
    }
}
