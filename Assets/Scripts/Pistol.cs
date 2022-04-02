using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    int normaldamage;
    [SerializeField]
    GameObject HitPrefab;

    [SerializeField]
    Animator animator;
    public void HoldShoot()
    {
        
    }

    public void Shoot()
    {
        animator.Play("Shoot");
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit))
        {
            IHurt damage = hit.collider.gameObject.GetComponent<IHurt>();
          
            if (damage != null)
            {
                damage.NormalDamage(normaldamage);
            }
            GameObject hitParticles=Instantiate(HitPrefab, Vector3.zero, Quaternion.identity);
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
