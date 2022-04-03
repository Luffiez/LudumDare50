using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles;
    EnemyNavigator navigator;
    Animator animator;

    void Start()
    {
        navigator = GetComponent<EnemyNavigator>();
        animator = GetComponent<Animator>();

        navigator.OnReachedPlayer.AddListener(OnExplode);
    }

    private void OnExplode()
    {
        GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        ManageRollAnimation();
    }

    void ManageRollAnimation()
    {
        if (navigator.PlayerInReach)
            animator.SetFloat("speed", 1);
        else if (animator.GetFloat("speed") != 0.5f && navigator.IsMoving)
            animator.SetFloat("speed", 0.5f);
        else if(animator.GetFloat("speed") != 0f)
            animator.SetFloat("speed", 0f);
    }
}
