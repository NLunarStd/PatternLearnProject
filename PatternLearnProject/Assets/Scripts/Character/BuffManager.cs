using System.Collections.Generic;
using UnityEngine;

public class BuffManager 
{
    private Character owner;
    private List<StatusEffect> _activeEffects = new List<StatusEffect>();

    public BuffManager(Character _owner)
    {
        owner = _owner;
    }

    public void AddEffect(StatusEffect newEffect)
    {
        // Logic: ถ้ามีบัฟชนิดเดียวกันอยู่แล้ว อาจจะต่ออายุ หรือซ้อนทับกัน
        _activeEffects.Add(newEffect);
    }

    public float GetBonusAttack()
    {
        float bonus = 0;
        foreach (var effect in _activeEffects)
        {
            if (effect.Type == EffectType.Attack_Boost)
            {
                bonus += effect.Value;
            }
        }
        return bonus;
    }

    public void DecrementDurations()
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            _activeEffects[i].Duration--;
            if (_activeEffects[i].Duration <= 0)
            {
                _activeEffects.RemoveAt(i);
            }
        }
    }
}
