using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public MenuManager menuManager;

    private void Awake()
    {
        menuManager = GetComponent<MenuManager>();
    }

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void LoadMainScene ()
    {
        menuManager.inMainMenu = false;
        SceneManager.LoadScene(sceneName: "Main");
        // GetComponent<Controllers>().SpawnBaskets();
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponent<Controllers>().SpawnPlayers();
    }

    void OnDisable()
    {
        // Unsubscribe to clean up
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
