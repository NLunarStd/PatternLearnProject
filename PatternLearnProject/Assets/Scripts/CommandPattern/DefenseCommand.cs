using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenseCommand :ICommand
{
    Character source;
    float shieldGain;
    int actionPointUsed;

    public DefenseCommand(Character _source)
    {
        source = _source;
    }
    public void Execute()
    {
        actionPointUsed = source.currentActionPoint;
        if (source == GameManager.instance.turnBaseSystem.playerCharacter)
        { 
            shieldGain = (source.currentActionPoint * 3);
        }
        source.shield += shieldGain;
        source.currentActionPoint = 0;

        EventManager.Publish(new CharacterActionTakenEvent
        {
            Source = source,
            ActionName = "Defense",
            Value = shieldGain
        });
        Debug.Log("Execute Defense Command");
    }

    public void Undo()
    {
        source.shield -= shieldGain;
        source.shield = Mathf.Max(0, source.shield);
        if (source == GameManager.instance.turnBaseSystem.playerCharacter)
        {
            source.currentActionPoint = actionPointUsed;
        }
    }

    public void Redo()
    {

    }
}
