using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterBuilder : MonoBehaviour
{
    public Character character = new Character();

    public CharacterBuilder StartNewCharacter()
    {
        character = new Character();
        return this;
    }

    public CharacterBuilder ApplyConfig(CharacterConfig config)
    {
        character.className = config.className;
        character.maxHP = config.baseHP;
        character.currentHP = config.baseHP;
        character.maxActionPoint = config.baseAP;
        character.currentActionPoint = config.baseAP;
        character.baseDamage = config.baseDamage;
        character.shield = 0;

        
        if (config.weaponConfig.weaponName == "Sword")
        {
           
            character.weapon = new PhysicalWeapon(config.weaponConfig);
        }
        else if (config.weaponConfig.weaponName == "FireStaff")
        {
           
            character.weapon = new ElementalWeapon(config.weaponConfig);
        }

        return this;
    }

    
    public Character Build()
    {
        if (character == null)
        {
            return null;
        }
        return character;
    }
}
