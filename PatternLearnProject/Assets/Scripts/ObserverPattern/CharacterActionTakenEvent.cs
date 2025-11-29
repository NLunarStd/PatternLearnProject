using UnityEngine;
using static SkillConfig;

public class CharacterActionTakenEvent : IGameEvent
{
    public Character Source;
    public Character Target;
    public string ActionName;
    public float Value;
    public SkillEffectType SkillEffectType;
}
