using UnityEngine;
using UnityEngine.SceneManagement;

public class WinAreaScript : MonoBehaviour
{

    public float timeToWin = 10f; // Time the player needs to stay in the area
    private float timer = 0f;
    private bool playerInArea = false;


    // Update is called once per frame
    void Update()
    {
        if (playerInArea)
        {
            timer += Time.deltaTime;

            if (timer >= timeToWin)
            {
                LoadVictoryScene();
            }
        }
        else
        {
            timer = 0f; // Reset the timer if the player leaves the area
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    private void LoadVictoryScene()
    {
        Debug.Log("Victory! Loading victory scene...");
        SceneManager.LoadScene("VictoryScene"); // Replace "VictoryScene" with your actual scene name or index
    }
}
