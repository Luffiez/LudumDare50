using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField]
    WeaponHandler weaponHandler;

    [SerializeField]
    TMP_Text ammoText;

    [SerializeField]
    Image ammoIcon;

    [SerializeField]
    Sprite[] ammoIcons;

    IWeapon currentWeapon;

    private void Awake()
    {
        weaponHandler.OnWeaponChanged.AddListener(OnWeaponChanged);
    }

    private void OnWeaponChanged(IWeapon newWeapon)
    {
        // Remove old listener
        if(currentWeapon != null)
            currentWeapon.OnAmmoChanged.RemoveListener(OnAmmoChanged);

        // Add new and update icon/ammo
        currentWeapon = newWeapon;
        currentWeapon.OnAmmoChanged.AddListener(OnAmmoChanged);

        if (currentWeapon is Pistol)
            ammoIcon.sprite = ammoIcons[0];
        else if(currentWeapon is Shotgun)
            ammoIcon.sprite = ammoIcons[1];

        OnAmmoChanged(currentWeapon.AmmoCount);
    }

    private void OnAmmoChanged(int currentAmmo)
    {
        ammoText.text = currentWeapon.AmmoCount.ToString();
    }
}
