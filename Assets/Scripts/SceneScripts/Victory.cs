using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{

    [Header("UI Pages")]
    public GameObject Menu;

    [Header("Main Menu Buttons")]
    public Button BackToMainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnableMenu();
        BackToMainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    public void BackToMainMenu()
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


