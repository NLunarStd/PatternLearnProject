using UnityEngine;

public class ElementalWeapon : IWeapon
{
    WeaponConfig weaponConfig;

    public ElementalWeapon(WeaponConfig wConfig)
    {
        weaponConfig = wConfig;
    }

    public string GetName()
    {
        return weaponConfig.name;
    }

    public float CalculateDamage()
    {
        return weaponConfig.baseElementalDamage;
    }
}
