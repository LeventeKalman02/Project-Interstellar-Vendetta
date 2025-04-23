using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public bool pauseMenu = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayUI();
        }
    }

    public void DisplayUI()
    {
        if (pauseMenu)
        {
            pauseMenuUI.SetActive(false);
            pauseMenu = true;
            Time.timeScale = 1f; // Resume the game
        }
        else if(!pauseMenu)
        {
            pauseMenuUI.SetActive(true);
            pauseMenu = false;
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void BackToMenu() {
        if (SceneTransitionManager.singleton != null)
        {
            SceneTransitionManager.singleton.GoToScene(0); // Use SceneTransitionManager for fade-out
        }
        else
        {
            Debug.LogError("SceneTransitionManager is not set up in the scene.");
        }
    }

    public void Exit() {
        Application.Quit();
        Debug.Log("Exit Game");
    }

}
