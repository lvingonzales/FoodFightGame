using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject InventoryPrefab;
    public int numOfPlayers = 1;

    [SerializeField]private List<Player> players = new List<Player>();
    [SerializeField]private List<GameObject> InventorySpawnPoints = new List<GameObject>();

    void Awake ()
    {
        players.Clear();
    }

    void Start()
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            GameObject playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            players.Add(playerInstance.GetComponent<Player>());

            GameObject playerInventory = Instantiate(InventoryPrefab, InventorySpawnPoints[i].transform.position, Quaternion.identity);
            playerInventory.GetComponent<InventoryScript>().attachedPlayer = playerInstance.GetComponent<Player>().playerId;
        }
    }
}
