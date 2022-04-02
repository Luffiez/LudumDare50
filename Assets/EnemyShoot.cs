using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPosition;

    float timer = 0;
    Transform player;
    EnemyNavigator navigator;
    bool isShooting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navigator = GetComponent<EnemyNavigator>();
        navigator.OnReachedPlayer.AddListener(StartShooting);
        navigator.PlayerOutOfRange.AddListener(StopShooting);
    }

    private void StopShooting()
    {
        isShooting = false;
    }

    private void StartShooting()
    {
        isShooting = true;
    }

    private void Update()
    {
        if (!isShooting)
            return;

        timer += Time.deltaTime;
        if (timer >= fireRate)
            Shoot();
    }

    void Shoot()
    {
        timer = 0;

        Vector3 dir = (player.position + Vector3.up ) - bulletSpawnPosition.position;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
    }
}
