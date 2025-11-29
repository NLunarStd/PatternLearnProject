using UnityEngine;

public interface IAbility 
{
    SkillConfig GetConfig();

    void Execute(Character source, Character target);

    void Undo(Character source, Character target, int appliedValue);
}
