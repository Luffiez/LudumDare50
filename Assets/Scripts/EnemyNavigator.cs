using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyNavigator : MonoBehaviour
{
    [SerializeField] float wanderRadius; // how far the enemy can wander
    [SerializeField] float playerDetectionRange = 5f;
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
    bool PlayerInReach =>
        DistanceToPlayer() <= playerDetectionRange;
    bool PlayerInView =>
        !Physics.Linecast(transform.position, player.position, obstacleMask);

    public UnityEvent OnReachedPlayer;
    public UnityEvent PlayerOutOfRange;

    private void Awake()
    {
        wanderTarget = GetRandomPosition(wanderRadius);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
    }

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
        if (agent.destination != player.position)
        {
            agent.destination = player.position;
            if (reachedPlayer)
            {
                reachedPlayer = false;
                PlayerOutOfRange?.Invoke();
            }
        }

        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if ((!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
                {
                    OnReachedPlayer?.Invoke();
                    reachedPlayer = true;
                }
            }
        }
    }
}
