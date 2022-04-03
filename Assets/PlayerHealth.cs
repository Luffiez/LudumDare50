using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IHurt
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public HealthChangedEvent OnTakeDamage;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    public void NormalDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeDamage.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, currentHealth, maxHealth);
        OnTakeDamage.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        currentHealth = maxHealth;    
    }
}
