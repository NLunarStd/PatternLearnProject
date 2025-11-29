using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig",menuName = "Skill Config")]
public class SkillConfig : ScriptableObject
{
    public string skillName;
    public int apCost; 
    public TargetType targetType;
    public SkillEffectType effectType; 
    public float baseValue;
    public int duration;

    public enum TargetType { Single, AOE, Self }
    public enum SkillEffectType { Damage, Heal, Buff, Debuff }
}
