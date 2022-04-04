using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{
    [SerializeField] int maxHealth;
    [SerializeField] GameObject deathParticles;
    [SerializeField] bool destroyParent = false;
    int currentHealth = 0;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (currentHealth != maxHealth)
            Debug.LogError("wtf?");
    }

    public void NormalDamage(int damage)
    {
        if (currentHealth > maxHealth)
            Debug.LogError("wtf?");

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2f);

        if (destroyParent)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }
}
