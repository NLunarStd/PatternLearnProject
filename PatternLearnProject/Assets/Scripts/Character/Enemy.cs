using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : Character
{
    public string enemyName = "";
    public float dropExp = 100;

    public string nextIntent { get; private set; }
    public ICommand ExecuteIntent(Character targetCharacter)
    {
        float chance = UnityEngine.Random.Range(0f,1f);

        if(chance < 0.7f)
        {
            nextIntent = "Attack";
        }
        else if(chance < 1f)
        {
            nextIntent = "Defense";
        }
        else
        {
            nextIntent = "Skill";
        }

        EventManager.Publish(new EnemyIntentCalculatedEvent
        {
            EnemyName = enemyName,
            Intent = nextIntent
        });

        switch (nextIntent)
        {
            case "Attack":
                return new AttackCommand(this, targetCharacter);
                
            case "Defense":
                return new DefenseCommand(this);
            case "Skill":
                return new SkillCommand(this, targetCharacter);
            default:
                return new NullCommand();

        }
    }

    public void ActionStrategy()
    {

    }
}
