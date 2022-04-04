using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyJingle : MonoBehaviour
{
    public float minCooldown = 10f;
    public float maxCooldown = 120f;

    AudioSource audioSrc;

    float cooldown;
    float timer = 0;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        cooldown = GetNewCooldown();
    }


    float GetNewCooldown()
    {
        return Random.Range(minCooldown, maxCooldown);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= cooldown)
        {
            Jingle();
        }
    }

    private void Jingle()
    {
        Debug.Log("play");
        timer = 0;
        cooldown = GetNewCooldown() + audioSrc.clip.length;
        audioSrc.Play();
    }
}
