using StarterAssets;
using System.Collections;
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
    StatTracker statTracker;
    bool dead = false;
    public bool Dead { get { return dead; } }
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    [SerializeField] Behaviour[] disableOnDeath;
  

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
        statTracker = FindObjectOfType<StatTracker>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void NormalDamage(int damage)
    {
        if (dead)
            return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        statTracker.UpdateDamageTaken(damage);
        float heatlhPerc = 1 - (float)currentHealth / maxHealth;
        vignette.intensity.value = 0.6f * heatlhPerc;
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, currentHealth, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    IEnumerator Die(float duration = 0.5f)
    {
        dead = true;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            if (disableOnDeath[i] is WeaponHandler handler)
            {
                handler.SetWeaponMovement(false);
                yield return new WaitForEndOfFrame();
            }
            disableOnDeath[i].enabled = false;
        };
        float startRotation = transform.eulerAngles.z;
        float endRotation = 70;
        float t = 0.0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 70f;
            Mathf.Clamp(zRotation, startRotation, endRotation);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, endRotation);

        yield return new WaitForSeconds(1f);

        // Show stat and restart screen

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
