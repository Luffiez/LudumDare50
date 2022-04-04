using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmmoPack : MonoBehaviour
{
    enum WeaponType {pistol, shotgun, dynamite };
    [SerializeField]
    WeaponType weaponType;

    IWeapon weapon;

    [SerializeField]
    Shotgun shotgun;
    [SerializeField]
    Pistol pistol;
    [SerializeField]
    Dynamite dynamite;


    [SerializeField]
    int ammoAmount;
    StatTracker statTracker;
    AudioSource audioSource;
    [SerializeField]
    AudioClip pickupSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        statTracker = GameObject.FindObjectOfType<StatTracker>();
        //return;
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (weaponType)
        {
            case WeaponType.dynamite:
                weapon = dynamite;
                break;

            case WeaponType.pistol:
                weapon = pistol;
                break;

            case WeaponType.shotgun:
                weapon = shotgun;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(pickupSound);
            statTracker.UpdateAmmoCollected(ammoAmount);
            weapon.AddAmmo(ammoAmount);
        }
    }
}
