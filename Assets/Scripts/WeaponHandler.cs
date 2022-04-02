using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class WeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    IWeapon activeWeapon;
    int weaponIndex = 0;
    [SerializeField]
    List<GameObject> WeaponObjects;

    [SerializeField]
    List<Animator> WeaponAnimators;

    bool holdShoot;
    private StarterAssets.StarterAssetsInputs input;

    private void Awake()
    {
        input = GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    void SwapWeapon(bool nextWeapon)
    {
        int newWeaponIndex = weaponIndex;
        if (nextWeapon)
        {
            weaponIndex++;
            if(weaponIndex >= WeaponObjects.Count)
            {
                newWeaponIndex = 0;
            }
        }
        else
        {
            weaponIndex--;
            if (weaponIndex < 0)
            {
                newWeaponIndex = WeaponObjects.Count - 1;
            }
        }
        WeaponObjects[weaponIndex].SetActive(false);
        WeaponObjects[newWeaponIndex].SetActive(true);
        activeWeapon = WeaponObjects[newWeaponIndex].GetComponent<IWeapon>();
        weaponIndex = newWeaponIndex;
        SetWeaponMovement(false);
    }

    void Start()
    {
        activeWeapon = WeaponObjects[weaponIndex].GetComponent<IWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.newWeaponDirection != 0)
        {
            SwapWeapon(input.newWeaponDirection == 1);
            input.newWeaponDirection = 0;
        }
        else
        {
            //Debug.Log("shoot:" + input.shooting);
            //Debug.Log("holdShoot:" + holdShoot);
            if (input.shooting)
            {
                activeWeapon.HoldShoot();
                if (!holdShoot)
                {
                    activeWeapon.Shoot();
                    holdShoot = true;
                }

            }
            else
            {
                holdShoot = false;
            }
        }  
    }

    public void SetWeaponMovement(bool isMoving)
    {
        WeaponAnimators[weaponIndex].SetBool("isMoving", isMoving);
    }
}
