using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Weapon config")]
public class WeaponConfig : ScriptableObject
{
    public string weaponName;
    public int basePhysicalDamage;
    public int baseElementalDamage;
    public string description;
}
