using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> menuSpawnPoints = new List<GameObject>();

    public TextMeshProUGUI playerCountText;

    private int playerCount = 0;

    public static MenuManager instance;

    public bool inMainMenu = true;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        playerCountText.text = playerCount + " / 4";
    }

    public void UpdatePlayerCount(int count)
    {
        playerCount = count;
        playerCountText.text = playerCount + " / 4";
    }
}
