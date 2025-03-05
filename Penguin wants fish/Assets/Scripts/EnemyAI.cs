using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 15f;
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
      //  animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseDistance)
        {
            agent.SetDestination(player.position);
          //  animator.SetBool("isChasing", true);
        }
        else
        {
            Debug.Log("  hi");
            //animator.SetBool("isChasing", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("  hi");
            // animator.SetTrigger("Attack");
        }
    }
}
