using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float movementPauseOnShoot = 5f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPositionL;
    [SerializeField] Transform bulletSpawnPositionR;

    float timer = 0;
    Transform player;
    EnemyNavigator navigator;
    bool canShoot = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navigator = GetComponent<EnemyNavigator>();
        navigator.OnReachedPlayer.AddListener(StartShooting);
        navigator.PlayerOutOfRange.AddListener(StopShooting);
    }

    private void StopShooting()
    {
        canShoot = false;
    }

    private void StartShooting()
    {
        canShoot = true;
    }

    private void Update()
    {
        if(animator.GetBool("walking"))
            animator.SetBool("walking", navigator.IsMoving);
        
        if (navigator.PlayerInReach)
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
                StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        navigator.StartCoroutine(navigator.PauseMovement(movementPauseOnShoot));
        timer = 0;

        animator.Play("Shoot");

        if(bulletSpawnPositionR)
        {
            // Right
            Vector3 dir = (player.position + Vector3.up) - bulletSpawnPositionR.position;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPositionR.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
       
        if(bulletSpawnPositionL)
        {
            yield return new WaitForSeconds(0.2f);

            // Left
            Vector3 dir = (player.position + Vector3.up) - bulletSpawnPositionL.position;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPositionL.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }
}
