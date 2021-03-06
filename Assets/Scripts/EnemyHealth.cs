using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{
    [SerializeField] int maxHealth;
    [SerializeField] GameObject deathParticles;
    [SerializeField] bool destroyParent = false;
    bool died = false;
    int currentHealth = 0;
    StatTracker statTracker;
    private void Awake()
    {
        currentHealth = maxHealth;

        if (currentHealth != maxHealth)
            Debug.LogError("wtf?");
    }

    private void Start()
    {
        statTracker = FindObjectOfType<StatTracker>();
    }

    public void NormalDamage(int damage)
    {
        if (currentHealth > maxHealth)
            Debug.LogError("wtf?");

        currentHealth -= damage;
        statTracker.UpdateDamageGiven(damage);
        if (currentHealth <= 0 && !died)
        {
            statTracker.UpdateKills();
            died = true;
            Die();
        }
            
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
