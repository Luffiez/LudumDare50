using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    int normaldamage;
    public void HoldShoot()
    {
        
    }

    public void Shoot()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit))
        {
            IHurt damage = hit.collider.gameObject.GetComponent<IHurt>();
            Debug.Log("hit: " + hit.collider.gameObject.name);
            if (damage != null)
            {
                damage.NormalDamage(normaldamage);
            }
        }
    }
}
