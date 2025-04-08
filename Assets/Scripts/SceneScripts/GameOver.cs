using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject Menu;

    [Header("Main Menu Buttons")]
    public Button restartButton;
    public Button QuitToMainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnableMenu();
        restartButton.onClick.AddListener(StartGame);
        QuitToMainMenuButton.onClick.AddListener(QuitToMainMenu);
    }

    public void StartGame () {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void QuitToMainMenu()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        Menu.SetActive(false);
    }

    public void EnableMenu()
    {
        Menu.SetActive(true);
    }

}
