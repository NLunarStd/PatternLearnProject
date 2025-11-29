using Mono.Cecil;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class AttackCommand : ICommand
{

    Character source;
    Character target;
    float damageDealt;

    public AttackCommand(Character _source, Character _target)
    {
        source = _source;
        target = _target;
    }
    public void Execute()
    {
        float damage = source.weapon.CalculateDamage();
        Debug.Log($"Raw Damage from {source.className}: {damage}");

        if (damage <= 0) 
        {
            damage = 0;
        }
        float finalDamage = target.TakeDamage(damage);

        if (source == GameManager.instance.turnBaseSystem.playerCharacter)
        {
            source.currentActionPoint -= 1;
        }

        damageDealt = finalDamage;

        EventManager.Publish(new CharacterActionTakenEvent
        {
            Source = source,
            Target = target,
            ActionName = "Attack",
            Value = damageDealt
        });

        Debug.Log("Execute Attack Command");
    }

    public void Undo()
    {
        target.Heal(damageDealt);
        source.currentActionPoint += 1;
    }

    public void Redo()
    {

    }
}
