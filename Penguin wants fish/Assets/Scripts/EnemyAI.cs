using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Assign your player in the Inspector
    public float chaseRange = 10f;

    private NavMeshAgent agent;
    public Animator animator;
    public bool isChasing = false;
    public bool isAttacking = false;
    public bool isActivated = false; // To control when the enemy starts
    public Transform enemySpawnPosition;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Idle(); // Start in Idle
    }

    void Update()
    {
        if (!isActivated || isAttacking) return; // Do nothing until Play() is called

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    public void Play() // Call this method from another script
    {
        isActivated = true;
    }

    void ChasePlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
        agent.SetDestination(player.position);
       
    }

    public void Idle()
    {
        isChasing = false;
        agent.ResetPath();
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        transform.position = enemySpawnPosition.position;
        transform.rotation = enemySpawnPosition.rotation;
    }
    public void ResetEnemy()
    {

        isChasing = false;
        agent.ResetPath();
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        transform.position = enemySpawnPosition.position;
        transform.rotation = enemySpawnPosition.rotation;
        animator.SetTrigger("Restart");
        isActivated = false;
    }

    public void Respawn()
    {
        animator.SetBool("IsRespwaned",true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated && other.tag == "Player")
        {
            Debug.Log("attacked");
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        agent.ResetPath();
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        Invoke(nameof(ResetAttack), 1.5f); // Adjust based on your attack animation
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
}
