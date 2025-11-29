using UnityEngine;

public class PhysicalWeapon : IWeapon
{
    WeaponConfig weaponConfig;

    public PhysicalWeapon(WeaponConfig wConfig)
    {
        weaponConfig = wConfig;
    }

    public string GetName()
    {
        return weaponConfig.name;
    }

    public float CalculateDamage()
    {
        float totalDamage = weaponConfig.basePhysicalDamage + weaponConfig.baseElementalDamage;
        return totalDamage;
    }
}
