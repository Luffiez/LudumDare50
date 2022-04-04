using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    private int kills=0;
    private int ammoCollected=0;
    private int damageTaken=0;
    private int damageGiven=0;
    private int damageHealed =0;
    private int wave = 0;
    private float timeSurvived=0;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    public int Kills { get { return kills; } }
    public int AmmoCollected { get{ return ammoCollected; } }
    public int DamageTaken { get { return damageTaken; } }

    public int DamageGiven { get { return damageGiven; } }

    public int DamageHealed { get { return damageHealed; } }

    public int Wave { get { return wave; } }

    public float TimeSurvived { get { return timeSurvived; } }

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void UpdateDamageTaken(int damage)
    {
        damageTaken += damage;
    }
    public void UpdateDamageGiven(int damage)
    {
        damageGiven += damage;
    }
    public void UpdateDamageHealed(int health)
    {
        damageHealed += health;
    }

    public void UpdateWave()
    {
        wave++;
    }

    public void UpdateKills()
    {
        kills ++;
    }


    public void UpdateAmmoCollected(int ammo)
    {
        ammoCollected += ammo;
    }
    // Update is called once per frame
    void Update()
    {
        if(!playerHealth.Dead)
        timeSurvived += Time.deltaTime;
    }
}
