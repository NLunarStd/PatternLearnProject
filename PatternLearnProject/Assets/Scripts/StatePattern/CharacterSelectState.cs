using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class CharacterSelectState : GameState
{
    public CharacterSelectState(GameManager gManager) : base(gManager) { }

    List<CharacterConfig> avaliableCharacterConfigs = new List<CharacterConfig>();
    int classIndex = 0;

    public TextMeshProUGUI className;
    public Image classImage;

    public override void Enter()
    {
        gameManager.characterSelectCanvas.gameObject.SetActive(true);

        classIndex = 0;
        avaliableCharacterConfigs = gameManager.GetAvaliableCharacter();


        gameManager.nextBtn.onClick.AddListener(OnNext);
        gameManager.previousBtn.onClick.AddListener(OnPrevious);
        gameManager.backBtn.onClick.AddListener(OnBackToStart);
        gameManager.selectBtn.onClick.AddListener(OnSelect);

        UpdateCharacterDisplay();
    }
    public override void Exit()
    {
        gameManager.characterSelectCanvas.gameObject.SetActive(false);

        gameManager.nextBtn.onClick.RemoveListener(OnNext);
        gameManager.previousBtn.onClick.RemoveListener(OnPrevious);
        gameManager.backBtn.onClick.RemoveListener(OnBackToStart);
        gameManager.selectBtn.onClick.RemoveListener(OnSelect);
    }

    void UpdateCharacterDisplay()
    {
        CharacterConfig currentCharacter = avaliableCharacterConfigs[classIndex];

        if (currentCharacter != null)
        {
            gameManager.selectiongClassName.text = currentCharacter.name;
            gameManager.selectiongClassImage.sprite = currentCharacter.sprite;
            gameManager.selectingClassDescription.text = currentCharacter.classDescription;
        }
    }

    void OnNext()
    {
        classIndex++;
        if( classIndex >= avaliableCharacterConfigs.Count ) 
        { 
            classIndex = 0; 
        }
        UpdateCharacterDisplay();
    }
    void OnPrevious()
    {
        classIndex--;
        if( classIndex < 0 )
        {
            classIndex = (avaliableCharacterConfigs.Count - 1);
        }
        UpdateCharacterDisplay();
    }

    void OnSelect()
    {
        CharacterConfig selectConfig = avaliableCharacterConfigs[classIndex];

        Character selectCharacter = gameManager.characterBuilder.StartNewCharacter()
            .ApplyConfig(selectConfig).Build();

        gameManager.turnBaseSystem.SetPlayerCharacter(selectCharacter);
        gameManager.currentUsingCharacterConfig = selectConfig;

        //EventManager.Publish(new SelectedCharacterEvent {
        //    ClassName = selectConfig.className,
        //    ClassSprite = selectConfig.sprite,
        //    ClassBaseHP = selectConfig.baseHP,
        //    ClassAP = selectConfig.baseAP
        //});


        gameManager.ChangeState(new GameplayState(gameManager));
    }
    void OnBackToStart()
    {
        gameManager.ChangeState(new StartState(gameManager));
    }
}
