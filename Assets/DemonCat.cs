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
        navigator.OnReachedPlayer.AddListener(OnReachedPlayer);
        navigator.PlayerOutOfRange.AddListener(OnPlayerOutOfRange);
    }

    private void OnPlayerOutOfRange()
    {
        withinRange = false;
    }

    private void OnReachedPlayer()
    {
        withinRange = true;
    }

    private void Update()
    {
        if (withinRange && timer <= 0)
            Attack();

        if (timer > 0)
            timer -= Time.deltaTime;

        animator.SetBool("moving", navigator.IsMoving);
    }

    void Attack()
    {
        navigator.StartCoroutine(navigator.PauseMovement(movementPauseOnShoot));
        timer = attackRate;
        animator.Play("Attack");
        playerHealth.NormalDamage(damage);
        Debug.Log("Kill player??");
    }
}
