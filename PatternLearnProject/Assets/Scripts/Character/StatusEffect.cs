using UnityEngine;

public class StatusEffect
{
    public string Name { get; set; } // เช่น "AttackUp"
    public float Value { get; set; }   // ค่าโบนัสที่เพิ่ม (เช่น +10 Attack)
    public int Duration { get; set; } // ระยะเวลาคงเหลือ (เช่น 3 เทิร์น)
    public EffectType Type { get; set; } // ATTACK_BOOST, DEFENSE_BOOST, POISON 
}

public enum EffectType { Attack_Boost, Defense_Boost, Poison, Regeneration }
