using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Button stunButton;
    public Button slipButton;


    [SerializeField]private List<Player> players = new List<Player>();

    void Awake ()
    {
        players.Clear();
    }

    void Start()
    {
        GameObject playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players.Add(playerInstance.GetComponent<Player>());

        stunButton.onClick.AddListener(StunPlayer);

        slipButton.onClick.AddListener(SlipPlayer);
    }

    private void SlipPlayer ()
    {
        players[0].ApplyEffect(EffectTypes.Slippery, 5f);
    }

    private void StunPlayer ()
    {
        players[0].ApplyEffect(EffectTypes.Stun, 2f);
    }
}
