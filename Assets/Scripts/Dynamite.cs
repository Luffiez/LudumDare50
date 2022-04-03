using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour,IWeapon
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
    public void HoldShoot()
    {
        if (ShootTimer < Time.time)
        {
            animator.Play("Shoot");
            ShootTimer = Time.time + shootTime;
            GameObject dynamteObject=Instantiate(dynamitePrefab, Camera.main.transform.position, Quaternion.identity);
            Rigidbody rigidbody = dynamteObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * forwardForce + Vector3.up * upwardForce,ForceMode.Impulse);
        }
    }

    public void Shoot()
    {

    }
}
