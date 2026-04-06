using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

public class Controllers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject basketPrefab;

    private MenuManager menuManager;

    private List<PlayerInput> players = new List<PlayerInput>();
    public List<Color> Colors = new List<Color>();
    public List<BasketScript> baskets = new List<BasketScript>();
    public List<GameObject> basketSpawners = new List<GameObject>();
    private const int MAX_PLAYERS = 4;
    private bool keyboardTaken = false;


    private void Awake ()
    {
        menuManager = GetComponent<MenuManager>();
    }

    private void Update()
    {
        if (menuManager.inMainMenu)
        {
            if (players.Count >= MAX_PLAYERS) { return; }

            if (!keyboardTaken && Keyboard.current.spaceKey.wasPressedThisFrame ) {
            CreatePlayer("KeyboardMouse", new InputDevice[] { Keyboard.current, Mouse.current });
            keyboardTaken = true;
            }

            foreach (Gamepad gamepad in Gamepad.all)
            {
            if (players.Count >= MAX_PLAYERS) break;

            bool alreadyAssigned = players.Any(p => p.devices.Contains(gamepad));
            if (alreadyAssigned) continue;

            if (gamepad.buttonSouth.wasPressedThisFrame)
                CreatePlayer("Gamepad", new InputDevice[] { gamepad });
            }
        }
        
    }

    void CreatePlayer(string controlScheme, InputDevice[] devices)
    {
        int index = players.Count;

        PlayerInput p = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: controlScheme,
            pairWithDevices: devices
        );

        if (index < Colors.Count)
        {
            p.GetComponent<Player>().playerColor = Colors[index];
        }
        players.Add(p.GetComponent<PlayerInput>());

        p.transform.position = menuManager.menuSpawnPoints[index].transform.position;
        menuManager.UpdatePlayerCount(players.Count);
    }


    public void SpawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerInput p = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: players[i].currentControlScheme,
            pairWithDevices: players[i].devices.ToArray()
            );

            if (i < Colors.Count)
            {
                p.GetComponent<Player>().playerColor = Colors[i];
            }

            p.GetComponent<PlayerMovement>().EnableMovement();
        }
    }

    public void SpawnBaskets()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject b = Instantiate(basketPrefab, basketSpawners[i].transform.position, Quaternion.identity);
            if (i < Colors.Count)
            {
                b.GetComponent<BasketScript>().basketColor = Colors[i];
            }
            baskets.Add(b.GetComponent<BasketScript>());
        }
    }
}
