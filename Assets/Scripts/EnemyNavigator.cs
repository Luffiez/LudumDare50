using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyNavigator : MonoBehaviour
{
    [SerializeField] float wanderRadius; // how far the enemy can wander
    [SerializeField] float playerDetectionRadius = 5f;
    [SerializeField] float stoppingDistance = 1f;
    [SerializeField] LayerMask obstacleMask;

    [Header("Idle Settings")]
    [Tooltip("How long the idle lasts")]
    [SerializeField] float minIdleDuration = 1;
    [Tooltip("How long the idle lasts")]
    [SerializeField] float maxIdleDuration = 3;

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 wanderTarget;

    float idleTimer = 0;
    bool reachedPlayer = false;
    bool wandering = true;
    public bool PlayerInReach =>
        DistanceToPlayer() <= playerDetectionRadius;
    bool PlayerInView =>
        !Physics.Linecast(transform.position, player.position, obstacleMask);

    [HideInInspector]
    public UnityEvent OnReachedPlayer;
    [HideInInspector]
    public UnityEvent PlayerOutOfRange;

    private void Awake()
    {
        wanderTarget = GetRandomPosition(wanderRadius);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
    }

    public bool IsMoving => agent.velocity != Vector3.zero;

    private void Update()
    {
        if (PlayerInReach) // && PlayerInView)
        {
            MoveTowardsPlayer();
            return;
        }

        if (wandering)
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
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void Wander()
    {
        if(agent.destination != wanderTarget)
            agent.destination = wanderTarget;
        
        if (ReachedDestination())
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
        if (agent.destination != player.position)
        {
            agent.destination = player.position;
            if (reachedPlayer)
            {
                reachedPlayer = false;
                PlayerOutOfRange?.Invoke();
            }
        }

        if (ReachedDestination())
        {
            OnReachedPlayer?.Invoke();
            reachedPlayer = true;
        }
    }

    bool ReachedDestination()
    {
        if (agent.pathPending)
            return false;

        if (agent.remainingDistance <= agent.stoppingDistance &&
            !agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            return true;
        
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Wander Radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);

        // Player Detection Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
