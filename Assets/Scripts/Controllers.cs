using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]private List<Player> players = new List<Player>();

    void Awake ()
    {
        players.Clear();
    }

    void Start()
    {
        GameObject playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players.Add(playerInstance.GetComponent<Player>());
    }
}
