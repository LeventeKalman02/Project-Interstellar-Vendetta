using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public float health = 100f;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    Animator animator;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("Attack"); //trigger the attack animation
        agent.SetDestination(transform.position); //stop moving
        transform.LookAt(player); //look at the player
        if (!alreadyAttacked) {

            //attack code here
            

            Debug.Log("Attacking player!");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //reset the attack after a certain time
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false; //reset the attack
    }

    private void TakeDamage(int damage) {
        health -= damage; //reduce health by damage amount
        if (health <= 0) {
            DestroyEnemy(); //call DestroyEnemy function if health is less than or equal to 0
        }
    }

    private void DestroyEnemy() {
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
