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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out RaycastHit hit))
        {
           IHurt damage= hit.collider.gameObject.GetComponent<IHurt>();
            if (damage != null)
            {
                damage.NormalDamage(normaldamage);
            }
        }
    }
}
