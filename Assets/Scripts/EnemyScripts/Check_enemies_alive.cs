using System.Collections.Generic;
using UnityEngine;

public class Check_enemies_alive : MonoBehaviour
{

    List<GameObject> listOfEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        print(listOfEnemies.Count);
    }

    public void KilledOpponent(GameObject opponent)
    {
        if (listOfEnemies.Contains(opponent))
        {
            listOfEnemies.Remove(opponent);
        }

        print(listOfEnemies.Count);
    }

    public bool AreOpponentsDead()
    {
        if (listOfEnemies.Count <= 0)
        {
            // They are dead!
            return true;

            //display message to player that all enemies are dead
            //move to the hangar to escape
            //load victory scene

        }
        else
        {
            // They're still alive
            return false;
        }
    }
}
