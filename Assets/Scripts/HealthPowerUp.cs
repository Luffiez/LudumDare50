using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField]
    int healthRecovery;
    StatTracker statTracker;
    // Start is called before the first frame update
    void Start()
    {
        statTracker = FindObjectOfType<StatTracker>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statTracker.UpdateDamageHealed(healthRecovery);
            playerHealth.RestoreHealth(healthRecovery);
        }
    }
}
