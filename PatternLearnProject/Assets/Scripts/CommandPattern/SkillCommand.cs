using UnityEngine;
using static SkillConfig;
using static UnityEngine.GraphicsBuffer;

public class SkillCommand : ICommand
{
    Character source;
    Character target;
    SkillConfig skillConfig;
    IAbility ability;

    int apUsed;
    float valueApplied;

    public SkillCommand(Character _source, Character _target)
    {
        source = _source;
        target = _target;
        ability = source.skill;
    }
    public void Execute()
    {
        if (source.currentActionPoint < skillConfig.apCost)
        {
            return;
        }
        apUsed = skillConfig.apCost;
        source.currentActionPoint -= apUsed;
        switch (skillConfig.effectType)
        {
            case SkillEffectType.Damage:
                float damage = skillConfig.baseValue + source.GetFinalDamage() * Random.Range(0.8f,1.5f);
                target.TakeDamage(damage);
                valueApplied = damage;
                break;

            case SkillEffectType.Heal:
                float heal = skillConfig.baseValue * Random.Range(0.8f, 1.5f);
                target.Heal(heal);
                valueApplied = heal;
                break;
            
            case SkillEffectType.Buff:
                target.statusEffects.AddEffect(new StatusEffect
                {
                    Name = "Attack Boost",
                    Type = EffectType.Attack_Boost,
                    Value = skillConfig.baseValue, 
                    Duration = skillConfig.duration                });
                break;
        }

        EventManager.Publish(new CharacterActionTakenEvent
        {
            Source = source,
            Target = target,
            ActionName = skillConfig.skillName,
            Value = valueApplied,
            SkillEffectType = skillConfig.effectType
        });

        Debug.Log("Execute Skill Command");
    }

    public void Undo()
    {
        source.currentActionPoint += apUsed;

        if (skillConfig.effectType == SkillEffectType.Damage)
        {
            target.Heal(valueApplied); 
        }
        else if (skillConfig.effectType == SkillEffectType.Heal)
        {
            target.TakeDamage(valueApplied);
        }
    }

    public void Redo()
    {

    }
}
