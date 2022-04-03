using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IHurt
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField]
    VolumeProfile profile;
    public HealthChangedEvent OnHealthChanged;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    Vignette vignette;

    void Awake()
    {
        if (OnHealthChanged == null)
            OnHealthChanged = new HealthChangedEvent();
        currentHealth = maxHealth;

        if(profile.TryGet<Vignette>(out var v))
        {
            vignette = v;
        }

        vignette.intensity.value = 0;
    }

    public void NormalDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        float heatlhPerc = 1 - (float)currentHealth / maxHealth;
        vignette.intensity.value = 0.5f * heatlhPerc;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, currentHealth, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
