using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour, IWeapon
{
    [SerializeField]
    int normaldamage;
    [SerializeField]
    GameObject HitPrefab;

    [SerializeField]
    Animator animator;
    float ShootTimer = 0;
    [SerializeField]
    float shootTime;
    [SerializeField]
    GameObject dynamitePrefab;
    [SerializeField]
    float upwardForce;
    [SerializeField]
    float forwardForce;

    [SerializeField]
    int ammoCap = 8;

    int ammoCount;

    public int AmmoCount => ammoCount;

    public int AmmoCap => ammoCap;

    public OnAmmoChangedEvent OnAmmoChanged { get => _OnAmmoChanged; }
    OnAmmoChangedEvent _OnAmmoChanged = new OnAmmoChangedEvent();

    private void Awake()
    {
        ammoCount = ammoCap;
    }

    public void AddAmmo(int amount)
    {
        ammoCount = Mathf.Clamp(ammoCount + amount, ammoCount, ammoCap);
        _OnAmmoChanged?.Invoke(ammoCount);
    }

    public void HoldShoot()
    {
        if (ShootTimer < Time.time)
        {
            ShootTimer = Time.time + shootTime;

            if (AmmoCount <= 0)
                return;
            animator.Play("Shoot");

            _OnAmmoChanged?.Invoke(ammoCount);
            ammoCount--;

            GameObject dynamteObject =Instantiate(dynamitePrefab, Camera.main.transform.position, Quaternion.identity);
            Rigidbody rigidbody = dynamteObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * forwardForce + Vector3.up * upwardForce,ForceMode.Impulse);
        }
    }

    public void Shoot()
    {

    }
}
