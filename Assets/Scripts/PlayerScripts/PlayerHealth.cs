using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f; // Player's health
    public float maxHealth = 100f; // Maximum health value

    public void TakeDamage(float damage)
    {
        // Reduce health by damage amount
        health -= damage;

        // Check if health is less than or equal to zero
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., play animation, restart level, etc.)
        Debug.Log("Player has died!");
        // You can add more logic here, such as restarting the game or showing a game over screen.
    }
}
