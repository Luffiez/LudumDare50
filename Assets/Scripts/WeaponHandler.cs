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
    [SerializeField]
    float changeTime = 0.3f;
    float changeTimer =0f;
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
            newWeaponIndex++;
            if(newWeaponIndex >= WeaponObjects.Count)
            {
                newWeaponIndex = 0;
            }
        }
        else
        {
            newWeaponIndex--;
            if (newWeaponIndex < 0)
            {
                newWeaponIndex = WeaponObjects.Count - 1;
            }
        }
        activeWeapon = WeaponObjects[newWeaponIndex].GetComponent<IWeapon>();
        WeaponAnimators[weaponIndex].gameObject.SetActive(false);
        WeaponAnimators[newWeaponIndex].gameObject.SetActive(true);
        weaponIndex = newWeaponIndex;
       
        SetWeaponMovement(false);
    }

    void Start()
    {
        activeWeapon = WeaponObjects[weaponIndex].GetComponent<IWeapon>();
        for (int i = 0; i < WeaponObjects.Count; i++)
        {
            if (i == weaponIndex)
            {
                continue;
            }
            WeaponAnimators[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (input.mouseWheel != 0 && changeTimer < Time.time)
        {
            SwapWeapon(input.newWeaponDirection >0);
            changeTimer = Time.time + changeTime;
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
        input.mouseWheel = 0;
    }

    public void SetWeaponMovement(bool isMoving)
    {
        WeaponAnimators[weaponIndex].SetBool("isMoving", isMoving);
    }
}
