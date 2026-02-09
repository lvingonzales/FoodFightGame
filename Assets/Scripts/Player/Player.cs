using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour 
{
    public PlayerInteraction playerData;

    private void Awake()
    {
        playerData = GetComponent<PlayerInteraction>();
    }    
}
