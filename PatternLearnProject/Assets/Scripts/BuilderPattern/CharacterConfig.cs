using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character config")]
public class CharacterConfig : ScriptableObject
{
    public string className = "Warrior";
    public Sprite sprite;
    public float baseHP = 15;
    public int baseAP = 3;
    public float baseDamage = 3;

    public WeaponConfig weaponConfig;
    //public IAbility ability = AttackBuff();
    public string classDescription = "";
}
