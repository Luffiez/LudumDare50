using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField]
    int healthRecovery;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.RestoreHealth(healthRecovery);
        }
    }
}
