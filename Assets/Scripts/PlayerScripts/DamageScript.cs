using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damage = 35f; // Damage value for the bullet

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Assuming the player has a PlayerHealth script attached to it
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                // Deal damage to the player
                enemy.TakeDamage(35); // Adjust damage value as needed
                GameObject.Destroy(gameObject); // Destroy the bullet after hitting the enemy
            }
        }
    }
}
