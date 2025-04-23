using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f; // Player's health
    public float maxHealth = 100f; // Maximum health value
    public TextMeshProUGUI healthText;


    private void Start()
    {
        // Initialize the health text at the start
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        // Reduce health by damage amount
        health -= damage;

        // Clamp health to ensure it doesn't go below 0
        health = Mathf.Clamp(health, 0, maxHealth);

        // Update the health UI
        UpdateHealthUI();

        // Check if health is less than or equal to zero
        if (health <= 0f)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        // Update the health text to display the current health
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("F0"); // Display health as an integer
        }
    }

    private void Die()
    {
        // Handle player death (e.g., play animation, restart level, etc.)
        Debug.Log("Player has died!");
        
        // Load the GameOver scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        
    }
}
