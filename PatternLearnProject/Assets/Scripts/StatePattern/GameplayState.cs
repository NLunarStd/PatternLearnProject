using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameManager gManager) : base(gManager) { }

    public override void Enter()
    {
        gameManager.gameplayCanvas.gameObject.SetActive(true);

        if (gameManager.currentUsingCharacterConfig != null)
        {
            CharacterConfig config = gameManager.currentUsingCharacterConfig;
            EventManager.Publish(new SelectedCharacterEvent
            {
                ClassName = config.className,
                ClassSprite = config.sprite,
                ClassBaseHP = config.baseHP,
                ClassAP = config.baseAP
            });
            //Debug.Log("Publish SelectedCharacter Event");
        }

        gameManager.attackBtn.onClick.AddListener(OnAttack);
        gameManager.defenseBtn.onClick.AddListener(OnDefense);
        gameManager.skillBtn.onClick.AddListener(OnSkill);
        gameManager.endTurnBtn.onClick.AddListener(OnEndTurn);
        gameManager.undoBtn.onClick.AddListener(OnUndo);

        gameManager.turnBaseSystem.StartCombat(true);
    }
    public override void Exit()
    {
        gameManager.gameplayCanvas.gameObject.SetActive(false);

        gameManager.attackBtn.onClick.RemoveAllListeners();
        gameManager.defenseBtn.onClick.RemoveAllListeners();
        gameManager.skillBtn.onClick.RemoveAllListeners();
        gameManager.endTurnBtn.onClick.RemoveAllListeners();

    }

    void OnAttack()
    {
        Debug.Log($"AP before command check: {gameManager.turnBaseSystem.playerCharacter.currentActionPoint}");
        if (gameManager.turnBaseSystem.playerCharacter.currentActionPoint <= 0)
        {
            Debug.LogWarning("Cannot attack, insufficient Action Points.");
            return;
        }

        Character player = gameManager.turnBaseSystem.playerCharacter;
        Character enemy = gameManager.turnBaseSystem.currentEnemy;
        if (player == null || enemy == null)
        {
            Debug.LogError("Player or Enemy is NULL!");
            return;
        }
        ICommand attackCommand = new AttackCommand(player,enemy);

        gameManager.turnBaseSystem.ExecuteCommand(attackCommand);
    }

    void OnDefense()
    {
        Character player = gameManager.turnBaseSystem.playerCharacter;
        ICommand defenseCommand = new DefenseCommand(player);
        gameManager.turnBaseSystem.ExecuteCommand(defenseCommand);
    }

    void OnSkill()
    {
        Character player = gameManager.turnBaseSystem.playerCharacter;
        Character enemy = gameManager.turnBaseSystem.currentEnemy;

        ICommand skillCommand = new SkillCommand(player, enemy);
        gameManager.turnBaseSystem.ExecuteCommand(skillCommand);
    }

    void OnEndTurn()
    {
        gameManager.turnBaseSystem.OnEndTurn();
    }

    void OnUndo()
    {
        gameManager.turnBaseSystem.UndoLastAction();
    }
}
