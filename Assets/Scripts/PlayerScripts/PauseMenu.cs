using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public Button restartButton;
    public Button BackToMenuButton;
    public Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            restartButton.onClick.AddListener(Restart);
            BackToMenuButton.onClick.AddListener(BackToMenu);
            exitButton.onClick.AddListener(Exit);
            DisplayUI();
        }
    }

    public void DisplayUI()
    {
        bool isPaused = !pauseMenuUI.activeSelf; // Toggle the current state of the pause menu
        pauseMenuUI.SetActive(isPaused); // Show or hide the pause menu
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume the game
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    public void Restart() {
        Debug.Log("Restarting the current scene...");
        Time.timeScale = 1; // Ensure the game is unpaused before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void BackToMenu() {
        Debug.Log("Returning to the main menu...");
        Time.timeScale = 1; // Ensure the game is unpaused before transitioning
        SceneManager.LoadScene(0); // Load the main menu scene (index 0)
    }

    public void Exit() {
        Debug.Log("Exiting the game...");
        Application.Quit(); // Quit the application
    }

}
