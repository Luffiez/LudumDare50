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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (weaponType)
        {
            case WeaponType.dynamite:
                weapon =   player.GetComponentInChildren<Dynamite>() as IWeapon;
                break;

            case WeaponType.pistol:
                weapon = player.GetComponentInChildren<Pistol>() as IWeapon;
                break;

            case WeaponType.shotgun:
                weapon = player.GetComponentInChildren<Shotgun>() as IWeapon;
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
