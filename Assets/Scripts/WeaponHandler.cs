using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    float changeTimer = 0f;

    private StarterAssets.StarterAssetsInputs input;

    public WeaponChangedEvent OnWeaponChanged;

    public IWeapon ActiveWeapon { get => activeWeapon; }

    private void Awake()
    {
        input = GetComponent<StarterAssets.StarterAssetsInputs>();
        OnWeaponChanged = new WeaponChangedEvent();
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
        OnWeaponChanged?.Invoke(activeWeapon);
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

        OnWeaponChanged?.Invoke(activeWeapon);
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
            if (input.shooting)
            {
                activeWeapon.HoldShoot();
            }
        }
        input.mouseWheel = 0;
    }

    public void SetWeaponMovement(bool isMoving)
    {
        WeaponAnimators[weaponIndex].SetBool("isMoving", isMoving);
    }
}
