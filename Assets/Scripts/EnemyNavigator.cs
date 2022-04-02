using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigator : MonoBehaviour
{
    public float wanderRadius; // how far the enemy can wander
    public float playerDetectionRange = 5f;
    public float stoppingDistance = 1f;
    public LayerMask obstacleMask;

    [Header("Idle Settings")]
    [Tooltip("How long the idle lasts")]
    public float minIdleDuration = 1;
    [Tooltip("How long the idle lasts")]
    public float maxIdleDuration = 3;

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 wanderTarget;

    float idleTimer = 0;

    bool wandering = true;
    bool PlayerInReach =>
        DistanceToPlayer() <= playerDetectionRange;
    bool PlayerInView =>
        !Physics.Linecast(transform.position, player.position, obstacleMask);
    
    private void Awake()
    {
        wanderTarget = GetRandomPosition(wanderRadius);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        if (PlayerInReach && PlayerInView)
        {
            MoveTowardsPlayer();
            return;
        }

        if(wandering)
        {
            Wander();
            return;
        }

        idleTimer -= Time.deltaTime;
        if(idleTimer <= 0)
        {
            wandering = true;
        }
    }

   
    public Vector3 GetRandomPosition(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void Wander()
    {
        agent.destination = wanderTarget;

        if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            wandering = false;
            wanderTarget = GetRandomPosition(wanderRadius);
            idleTimer = Random.Range(minIdleDuration, maxIdleDuration);
        }
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    public void MoveTowardsPlayer()
    {
        agent.destination = player.position;
    }
}
