using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnBaseSystem : MonoBehaviour
{
    [Header("Combat Data")]
    public Character playerCharacter;
    public Enemy currentEnemy;
    public int currentRoom = 1;

    public EnemyFactory enemyFactory;

    GameManager gameManager;

    public Stack<ICommand> commandHistory = new Stack<ICommand> ();

    public enum BattlePhase { SetUp, PlayerTurn, EnemyTurn, Victory, Defeat}
    public BattlePhase currentPhase;


    private void OnEnable()
    {
        gameManager = GameManager.instance;

        gameManager.attackBtn.interactable = false;
        gameManager.skillBtn.interactable = false;
        gameManager.defenseBtn.interactable = false;
        gameManager.undoBtn.interactable = false;
        gameManager.endTurnBtn.interactable = false;
    }

    public void SetPlayerCharacter(Character character)
    {
        playerCharacter = character;
        //Debug.Log($"load {character.className}");
        playerCharacter.currentActionPoint = playerCharacter.maxActionPoint;
    }

    public void StartCombat(bool newGame = true)
    {
        playerCharacter.currentActionPoint = playerCharacter.maxActionPoint;

        Debug.Log($"Set AP to Max: {playerCharacter.currentActionPoint}");

        StartCoroutine(BattleSequence(newGame));

    }
    private IEnumerator BattleSequence(bool newGame)
    {
        ChangeBattlePhase(BattlePhase.SetUp);

        if (newGame)
        {
            currentRoom = 1;
        }

        SpawnEnemy(currentRoom);

        //Debug.Log("Battle Setup Complete. Delaying for 3 seconds...");

        yield return new WaitForSeconds(3f);

        EventManager.Publish(new OnDelayEnd
        {

        });
        //Debug.Log("Delaying Complete");
        ChangeBattlePhase(BattlePhase.PlayerTurn);


    }
    void SpawnEnemy(int currentRoom)
    {
        currentEnemy = enemyFactory.CreateEnemy(currentRoom);

        //currentEnemy.CalculateNextMove();
    }

    void ChangeBattlePhase(BattlePhase newPhase)
    {
        Debug.Log($"Change Battle phase from {currentPhase} to {newPhase}");
        currentPhase = newPhase;

        if (newPhase == BattlePhase.EnemyTurn)
        {
            StartEnemyTurn();

            playerCharacter.statusEffects.DecrementDurations();
            currentEnemy.statusEffects.DecrementDurations();

        }
        else if (newPhase == BattlePhase.PlayerTurn)
        {
            playerCharacter.currentActionPoint = playerCharacter.maxActionPoint;
            Debug.Log($"Recharge AP to Max: {playerCharacter.currentActionPoint}");
            playerCharacter.shield = 0;

            gameManager.attackBtn.interactable = true;
            gameManager.skillBtn.interactable = true;
            gameManager.defenseBtn.interactable = true;
            gameManager.undoBtn.interactable = false;
            gameManager.endTurnBtn.interactable = true;
        }
        else if (currentPhase == BattlePhase.Victory)
        {

            playerCharacter.currentEXP += currentEnemy.dropExp;
            //transition room
        }
        else if (currentPhase == BattlePhase.Defeat)
        {
            gameManager.ChangeState(new StartState(gameManager));
            // player lose
        }
    }

    public void ExecuteCommand(ICommand command)
    {
        if (currentPhase != BattlePhase.PlayerTurn)
        {
            return;
        }
        command.Execute();
        commandHistory.Push(command);
        gameManager.undoBtn.interactable = true;
        if (playerCharacter.shield <= 0)
        {
            gameManager.shields.gameObject.SetActive(false);
        }
        if(currentEnemy.shield <= 0)
        {
            gameManager.enemyShield.gameObject.SetActive(false);
        }
        CheckBattleEndCondition();
    }
    
    void CheckBattleEndCondition()
    {
        if (playerCharacter.currentHP <= 0)
        {
            ChangeBattlePhase(BattlePhase.Defeat);
        }
        else if (currentEnemy != null && currentEnemy.currentHP <= 0)
        {

            playerCharacter.currentActionPoint = playerCharacter.maxActionPoint;
            EventManager.Publish(new OnVictoryEvent
            {
                Winner = playerCharacter,
                DefeatedEnemy = currentEnemy
            });

            StartCoroutine(VictorySequence());
        }
    }
    private IEnumerator VictorySequence()
    {
        ChangeBattlePhase(BattlePhase.Victory);


        gameManager.enemyRenderer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(RoomTransitionSequence());
    }

    private IEnumerator RoomTransitionSequence()
    {

        Vector3 currentPos = gameManager.background.position;
        gameManager.background.position = currentPos + Vector3.down * 16f;

        
        yield return new WaitForSeconds(0.5f); 

        currentRoom++; 


        StartCombat(false);
    }
    public void UndoLastAction()
    {
        if (currentPhase != BattlePhase.PlayerTurn || commandHistory.Count ==0)
        {
            gameManager.undoBtn.interactable = false;
            return;
        }
        ICommand lastCommand = commandHistory.Pop();
        lastCommand.Undo();

        gameManager.playerAPSlider.value = playerCharacter.currentActionPoint;

        gameManager.playerHPSlider.value = playerCharacter.currentHP;

        if (currentEnemy.currentHP > 0)
        {
            gameManager.enemyHPSlider.value = currentEnemy.currentHP;
        }

        if (playerCharacter.shield > 0)
        {
            gameManager.shields.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerCharacter.shield.ToString();
            gameManager.shields.gameObject.SetActive(true);
        }
        else
        {
            gameManager.shields.gameObject.SetActive(false);
        }

        if (currentEnemy != null && currentEnemy.shield > 0)
        {
            gameManager.enemyShield.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentEnemy.shield.ToString();
            gameManager.enemyShield.gameObject.SetActive(true);
        }
        else if (currentEnemy != null)
        {
            gameManager.enemyShield.gameObject.SetActive(false);
        }

        if (commandHistory.Count <= 0)
        {
            gameManager.undoBtn.interactable = false;
        }
    }

    public void OnEndTurn()
    {
        if (currentPhase != BattlePhase.PlayerTurn)
        {
            return;
        }
        commandHistory.Clear();

        gameManager.attackBtn.interactable = false;
        gameManager.skillBtn.interactable = false;
        gameManager.defenseBtn.interactable = false;
        gameManager.undoBtn.interactable = false;
        gameManager.endTurnBtn.interactable = false;

        EventManager.Publish(new TurnPhaseChangeEvent { newPhase = BattlePhase.EnemyTurn });
        ChangeBattlePhase(BattlePhase.EnemyTurn);
    }
    public void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurnSequence());

    }
    IEnumerator EnemyTurnSequence()
    {
        Debug.Log("--- Starting Enemy Turn ---");

        ICommand enemyCommand = currentEnemy.ExecuteIntent(playerCharacter);

        enemyCommand.Execute();

        yield return new WaitForSeconds(2.0f);

        CheckBattleEndCondition();

        if (currentPhase != BattlePhase.Victory && currentPhase != BattlePhase.Defeat)
        {

            EventManager.Publish(new TurnPhaseChangeEvent { newPhase = BattlePhase.PlayerTurn });

            Debug.Log($"Player current HP : {playerCharacter.currentHP}");
            ChangeBattlePhase(BattlePhase.PlayerTurn);

        }
        else
        {
            Debug.Log("Battle Ended.");
        }
    }
}
