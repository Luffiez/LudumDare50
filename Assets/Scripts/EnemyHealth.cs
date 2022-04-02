using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{
    [SerializeField] int maxHealth;
    [SerializeField] GameObject deathParticles;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void NormalDamage(int damage)
    {
        Debug.Log("Take damage");
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GameObject particles = Instantiate(deathParticles, transform.position + Vector3.up, Quaternion.identity);
        Destroy(particles, 2f);

        Destroy(gameObject);
    }
}
