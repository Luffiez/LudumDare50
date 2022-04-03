using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpRespawner : MonoBehaviour
{
    Collider collider;
    [SerializeField]
    SpriteRenderer renderer;
    bool respawned = true;
    [SerializeField]
    float respawnTime;
    float respawnTimer;
    [SerializeField]
    TextMeshPro timerText;
    private void Start()
    {
        collider = GetComponent<Collider>();
          timerText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (respawned)
        {
            collider.enabled = false;
            renderer.enabled = false;
            respawned = false;
            respawnTimer = respawnTime;
        }
   
    }
    // Update is called once per frame
    void Update()
    {
        if (!respawned)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                respawned = true;
                collider.enabled = true;
                renderer.enabled = true;
                timerText.text = "";
                return;
            }
            int minutes = (int)respawnTimer / 60;
            int seconds = (int)respawnTimer - minutes * 60;
            string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
            timerText.text = minutes + ":" + secondsString;
        }


    }
}
