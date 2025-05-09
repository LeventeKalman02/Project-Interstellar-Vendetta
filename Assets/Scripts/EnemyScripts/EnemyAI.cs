using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public float health = 100f;

    public float attackDamage = 20f;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    Animator animator;

    BoxCollider boxCollider;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update() {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) Attack();
    }

    //enemy patrolling
    private void Patrolling() {
        //set walk point if null
        if (!walkPointSet) SearchWalkPoint();
        //walk to the set walk point
        if (walkPointSet) agent.SetDestination(walkPoint);
        //calculate distance to walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (!CanReach()) {
            walkPointSet = false; //if the enemy can't reach the walk point, set to false
        }

        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false; //walk point reached, set to false
        }
    }

    //check if the enemy can reach the walk point
    private bool CanReach()
    {
        if (agent.pathPending && agent.remainingDistance > agent.stoppingDistance && agent.velocity.sqrMagnitude == 0f) return false;
        return true;
    }

    private void SearchWalkPoint() {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange); //return random value depending on walk point range
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position); //set the destination to the player position
    }
    private void Attack() {
        if (!alreadyAttacked){
            animator.SetTrigger("Attack"); //trigger the attack animation
            agent.SetDestination(transform.position); //stop moving
            transform.LookAt(player); //look at the player
            Debug.Log("Attacking player!");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack after a delay
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //enable the attack collider
    private void EnableAttack() {
        boxCollider.enabled = true;
    }

    private void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player detected!" + other.gameObject.name); //log player detection
        if (other.CompareTag("Player"))
        {
            //call the TakeDamage function from the PlayerHealth script
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); //pass the damage amount to the TakeDamage function
                Debug.Log("Player took damage!");
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player detected2!" + collision.gameObject.name); //log player detection
        if (collision.gameObject.CompareTag("Player"))
        {
            //call the TakeDamage function from the PlayerHealth script
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); //pass the damage amount to the TakeDamage function
                Debug.Log("Player took damage!");
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage; //reduce health by damage amount
        if (health <= 0) {
            DestroyEnemy(); //call DestroyEnemy function if health is less than or equal to 0
        }
    }

    private void DestroyEnemy() {
        Check_enemies_alive.instance.KilledOpponent(gameObject); //call the KilledOpponent function from the Check_enemies_alive script
        Destroy(gameObject); //destroy the enemy game object
    }

    //use to visualize the sight and attack range in the editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red; //set the color of the gizmos to red
        Gizmos.DrawWireSphere(transform.position, sightRange); //draw a wire sphere around the enemy to show the sight range
        Gizmos.color = Color.blue; //set the color of the gizmos to blue
        Gizmos.DrawWireSphere(transform.position, attackRange); //draw a wire sphere around the enemy to show the attack range
    }
}
