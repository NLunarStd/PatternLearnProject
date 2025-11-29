using UnityEngine;

public class Character
{
    public string className;
    public IAbility skill;
    public BuffManager statusEffects { get; private set; }
    public float baseDamage { get; set; }
    public IWeapon weapon;


    public int maxActionPoint;
    float _maxHP;

    public float maxHP
    {
        get => _maxHP;
        set { _maxHP = value; }

    }
    float _currentHP;
    public float currentHP
    {
        get => _currentHP;
        set { _currentHP = value; }
    }
    int _currentActionPoint;
    public int currentActionPoint
    {
        get => _currentActionPoint;
        set { _currentActionPoint = value; }
    }
    float _currentEXP;
    public float currentEXP
    {
        get => _currentEXP;
        set { _currentEXP = value; }
    }
    float _shield;
    public float shield {
        get => _shield;
        set { _shield = value; } 
    }


   
    


    public Character()
    {
        _maxHP = 10;
        _currentHP = _maxHP;
        _currentActionPoint = 3;
        _shield = 0;
        _currentEXP = 0;

        this.statusEffects = new BuffManager(this);
    }

    public float TakeDamage(float damage)
    {
        Debug.Log($"[{this.className} - Taking Damage] Raw Damage: {damage}. Current Shield BEFORE: {shield}");
        float damageAbsorbedByShield = 0;

        if (shield > 0)
        {
            damageAbsorbedByShield = Mathf.Min(damage, shield);

            shield -= damageAbsorbedByShield;

            shield = Mathf.Max(0, shield);
        }

        float remainingDamage = damage - damageAbsorbedByShield;

        if (remainingDamage > 0)
        {
            currentHP -= remainingDamage;
        }

        currentHP = Mathf.Max(0, currentHP);
        Debug.Log($"[{this.className} - Taking Damage] Remaining Damage to HP: {remainingDamage}");
        return remainingDamage;
    }

    public void Heal(float amount)
    {
        _currentHP += amount;
    }

    public float GetFinalDamage()
    {
        return baseDamage + statusEffects.GetBonusAttack();
    }
}
