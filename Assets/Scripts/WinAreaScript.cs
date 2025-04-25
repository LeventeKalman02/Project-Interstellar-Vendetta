using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class WinAreaScript : MonoBehaviour
{

    public float timeToWin = 10f; // Time the player needs to stay in the area
    private float timer = 0f;
    private bool playerInArea = false;

    public GameObject victoryMessage;
    public GameObject killAllEnemies;

    public GameObject timerMessage;

    public List<GameObject> enemies = new List<GameObject>();


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
            
            timerMessage.GetComponent<TextMeshProUGUI>().text = "Stay In Circle!: " + (timeToWin - timer).ToString("F2") + " seconds";
            timerMessage.SetActive(true); // Show the timer message
            victoryMessage.SetActive(false); // Hide the victory message
            killAllEnemies.SetActive(false); // Hide the survived message
        }
        else
        {
            timer = 0f; // Reset the timer if the player leaves the area
            timerMessage.GetComponent<TextMeshProUGUI>().text = "Time left: " + (timeToWin).ToString("F2") + " seconds";
            //set back to the original message
            timerMessage.SetActive(false); // Hide the timer message
            victoryMessage.SetActive(true); // Hide the survived message
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
            foreach (GameObject enemy in enemies){
                enemy.SetActive(true);
            }
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
        SceneManager.LoadScene(3);
    }
}
