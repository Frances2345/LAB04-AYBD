using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public float speed = 3.5f;
    public bool canMove = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }
    }

    public void Update()
    {
        if (target != null && agent != null)
        {
            if (canMove && !agent.isStopped)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            else
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
    }

    public void MoveTowardsPlayer()
    {

    }

    public void SetStopped(bool stop)
    {
        if (agent != null)
        {
            agent.isStopped = stop;
        }
    }
}
