using UnityEngine;

public class DemonCat : MonoBehaviour
{
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] int damage = 10;
    [SerializeField] float movementPauseOnShoot = 5f;
    EnemyNavigator navigator;

    [SerializeField]
    bool withinRange = false;
    Animator animator;

    PlayerHealth playerHealth;

    float timer = 0;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
        timer = attackRate;
        navigator = GetComponent<EnemyNavigator>();
    }


    private void Update()
    {
        if(DistanceToPlayer() <= navigator.StoppingDistance && timer <= 0)
            Attack();

        if (timer > 0)
            timer -= Time.deltaTime;

        animator.SetBool("moving", navigator.IsMoving);
    }


    float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, playerHealth.transform.position);
    }

    void Attack()
    {
        navigator.StartCoroutine(navigator.PauseMovement(movementPauseOnShoot));
        timer = attackRate;
        animator.Play("Attack");
        playerHealth.NormalDamage(damage);
    }
}
