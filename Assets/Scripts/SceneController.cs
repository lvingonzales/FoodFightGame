using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public MenuManager menuManager;

    private void Awake()
    {
        menuManager = GetComponent<MenuManager>();
    }

    public void LoadMainScene ()
    {
        menuManager.inMainMenu = false;
        SceneManager.LoadScene(sceneName: "Main");
    }
}
