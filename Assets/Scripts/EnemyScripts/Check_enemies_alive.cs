using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check_enemies_alive : MonoBehaviour
{

    List<GameObject> listOfEnemies = new List<GameObject>();
    public GameObject victoryMessage;
    public GameObject survivedMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        print(listOfEnemies.Count);

        // Ensure the victory message is hidden at the start
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(false);
        }
    }

    public void KilledOpponent(GameObject opponent)
    {
        if (listOfEnemies.Contains(opponent))
        {
            listOfEnemies.Remove(opponent);
        }

        print(listOfEnemies.Count);

        // Check if all enemies are dead
        if (AreOpponentsDead())
        {
            ShowVictoryMessage();
        }
    }

    public bool AreOpponentsDead()
    {
        return listOfEnemies.Count <= 0; // Check if the list is empty or null
    }

    private void ShowVictoryMessage()
    {
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(true); // Display the message
            survivedMessage.SetActive(false);
        }
    }
}
