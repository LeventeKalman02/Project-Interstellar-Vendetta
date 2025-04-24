using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [SerializeField] private EnemyAI enemyAI;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.name);
        enemyAI.OnTriggerEnter(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter: " + collision.gameObject.name);
        enemyAI.OnCollisionEnter(collision);
    }
}
