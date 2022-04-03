using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
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
    int ammoCap = 20;

    [SerializeField]
    Light lightSrc;

    int ammoCount;

    private void Start()
    {
        ammoCount = ammoCap;
    }

    public int AmmoCount { get => ammoCount; }
    public int AmmoCap { get => ammoCap; }

    public void AddAmmo(int amount)
    {
        ammoCount = Mathf.Clamp(ammoCount + amount, ammoCount, ammoCap);
    }

    public void HoldShoot()
    {
        
    }

    public void Shoot()
    {
        if (ShootTimer < Time.time)
        {
            ShootTimer = Time.time + shootTime;

            if (AmmoCount <= 0)
                return;

            ammoCount--;
            StartCoroutine(ToggleLight());
            animator.Play("Shoot");
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit))
            {
                IHurt damage = hit.collider.gameObject.GetComponent<IHurt>();

                if (damage != null)
                {
                    damage.NormalDamage(normaldamage);
                }
                GameObject hitParticles = Instantiate(HitPrefab, Vector3.zero, Quaternion.identity);
                hitParticles.transform.position = hit.point;
                //https://forum.unity.com/threads/setting-rotation-from-hit-normal.166587/
                // get the cross from the user's left, this returns the up/down direction.
                Vector3 lookAt = Vector3.Cross(-hit.normal, transform.right);
                // reverse it if it is down.
                lookAt = lookAt.y < 0 ? -lookAt : lookAt;
                // look at the hit's relative up, using the normal as the up vector
                hitParticles.transform.rotation = Quaternion.LookRotation(hit.point + lookAt, hit.normal);
            }
        }
    }


    IEnumerator ToggleLight()
    {
        lightSrc.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lightSrc.enabled = false;
    }
}
