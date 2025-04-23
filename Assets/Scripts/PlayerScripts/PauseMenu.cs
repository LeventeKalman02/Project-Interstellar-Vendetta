using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public bool pauseMenu = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
