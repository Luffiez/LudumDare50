using UnityEngine;

public interface IWeapon 
{
    public int AmmoCount { get; }
    public int AmmoCap { get; }

    public void AddAmmo(int amount);
  
    public void HoldShoot();

    public OnAmmoChangedEvent OnAmmoChanged { get; }
}

