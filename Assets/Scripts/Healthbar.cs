using TMPro;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerhealth;

    [SerializeField]
    TMP_Text healthText;

    private void Start()
    {
        playerhealth.OnHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth.ToString();
    }
}
