using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{
    [SerializeField] int maxHealth;
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
        Destroy(gameObject);
    }
}
