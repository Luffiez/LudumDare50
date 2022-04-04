using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthPowerUp : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField]
    int healthRecovery;
    StatTracker statTracker;
    [SerializeField]
    AudioClip audioClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        statTracker = FindObjectOfType<StatTracker>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);
            statTracker.UpdateDamageHealed(healthRecovery);
            playerHealth.RestoreHealth(healthRecovery);
        }
    }
}
