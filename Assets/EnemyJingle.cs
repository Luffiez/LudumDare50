using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyJingle : MonoBehaviour
{
    public float minCooldown = 10f;
    public float maxCooldown = 120f;
    public AudioClip[] clips;

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
        if (clips.Length == 0)
            return;

        timer = 0;
        int id = Random.Range(0, clips.Length);
        AudioClip clip = clips[id];
        audioSrc.PlayOneShot(clip);

        cooldown = GetNewCooldown() + clip.length;
    }
}
