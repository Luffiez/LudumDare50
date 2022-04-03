using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using System.Collections;

public class Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField]
    int normaldamage;
    [SerializeField]
    GameObject HitPrefab;
    [SerializeField]
    int nPellets;
    [SerializeField]
    float spread;
    [SerializeField]
    Animator animator;
    float ShootTimer = 0;
    [SerializeField]
    float shootTime;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Light lightSrc;

    [SerializeField]
    int ammoCap = 8;

    int ammoCount;

    public OnAmmoChangedEvent OnAmmoChanged { get => _OnAmmoChanged; }
    OnAmmoChangedEvent _OnAmmoChanged = new OnAmmoChangedEvent();

    private void Awake()
    {
        ammoCount = ammoCap;
    }

    public int AmmoCount => ammoCount;

    public int AmmoCap => ammoCap;

    public void AddAmmo(int amount)
    {
        ammoCount = Mathf.Clamp(ammoCount + amount, ammoCount, ammoCap);
        _OnAmmoChanged?.Invoke(ammoCount);
    }



    public void HoldShoot()
    {
        if (ShootTimer < Time.time)
        {
            ShootTimer = shootTime + Time.time;
            if (AmmoCount <= 0)
                return;
            ammoCount--;
            _OnAmmoChanged?.Invoke(ammoCount);

            StartCoroutine(ToggleLight());
            animator.Play("Shoot");
            //https://docs.unity3d.com/ScriptReference/RaycastCommand.html
            NativeArray<RaycastHit> results = new NativeArray<RaycastHit>(nPellets, Allocator.TempJob);
            NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(nPellets, Allocator.TempJob);
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;
            //https://stackoverflow.com/questions/47889977/unity-shotgun-making
            Vector3[] shoots = new Vector3[nPellets];
            for (int i = 0; i < shoots.Length; i++)
            {
                Vector3 vectorSpread = Vector3.zero;
                //direction in circle
                vectorSpread += Vector3.up * Random.Range(-1f, 1f);
                vectorSpread += Vector3.right * Random.Range(-1f, 1f);
                vectorSpread += Vector3.forward * Random.Range(-1f, 1f);
                //random again to change the spread, othervise its always at the edge of the circle
                Vector3 shootDirection = cameraForward + vectorSpread.normalized * Random.Range(-spread, spread);
                commands[i] = new RaycastCommand(cameraPosition, shootDirection, Mathf.Infinity, layerMask);
            }
            JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));
            handle.Complete();
            RaycastHit[] hits = results.ToArray();
            results.Dispose();
            commands.Dispose();
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].collider != null)
                {
                    GameObject hitParticles = Instantiate(HitPrefab, Vector3.zero, Quaternion.identity);
                    hitParticles.transform.position = hits[i].point;
                    //https://forum.unity.com/threads/setting-rotation-from-hit-normal.166587/
                    // get the cross from the user's left, this returns the up/down direction.
                    Vector3 lookAt = Vector3.Cross(-hits[i].normal, transform.right);
                    // reverse it if it is down.
                    lookAt = lookAt.y < 0 ? -lookAt : lookAt;
                    // look at the hit's relative up, using the normal as the up vector
                    hitParticles.transform.rotation = Quaternion.LookRotation(hits[i].point + lookAt, hits[i].normal);
                    IHurt hurt = hits[i].collider.gameObject.GetComponent<IHurt>();
                    if (hurt != null)
                    {
                        hurt.NormalDamage(normaldamage);
                    }
                }
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
