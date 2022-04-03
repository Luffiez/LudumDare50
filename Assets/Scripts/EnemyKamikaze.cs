using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles;
    [SerializeField] ParticleSystem rollParticles;
    EnemyNavigator navigator;
    Animator animator;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navigator = GetComponent<EnemyNavigator>();
        animator = GetComponent<Animator>();

    }

    private void Explode()
    {
        GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        ManageRollAnimation();

        if (DistanceToPlayer() <= navigator.StoppingDistance)
            Explode();
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    void ManageRollAnimation()
    {
        if (navigator.PlayerInReach)
        {
            animator.SetFloat("speed", 1);
            rollParticles.Play();
        }
        else if (animator.GetFloat("speed") != 0.5f && navigator.IsMoving)
        {
            animator.SetFloat("speed", 0.5f);
            rollParticles.Stop();
        }
        else if(animator.GetFloat("speed") != 0f)
        {
            animator.SetFloat("speed", 0f);
            rollParticles.Stop();
        }
    }
}
