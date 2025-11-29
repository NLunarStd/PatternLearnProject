using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Start Canvas")]
    public Transform startCanvas;
    public Button startBtn;
    public Button exitBtn;

    [Header("UI Charcter Selection Canvas")]
    public Transform characterSelectCanvas;
    public TextMeshProUGUI selectiongClassName;
    public Image selectiongClassImage;
    public TextMeshProUGUI selectingClassDescription;
    public Button nextBtn;
    public Button previousBtn;
    public Button backBtn;
    public Button selectBtn;

    [Header("UI Gameplay Canvas")]
    public Transform gameplayCanvas;
    public Button attackBtn;
    public Button defenseBtn;
    public Button skillBtn;
    public Button endTurnBtn;
    public Button undoBtn;

    public Image characterPortrait;
    public TextMeshProUGUI characterClassName;
    public Slider playerHPSlider;
    public Slider playerAPSlider;
    public Transform shields;

    public TextMeshProUGUI enemyName;
    public Slider enemyHPSlider;
    public Transform enemyShield;

    public TextMeshProUGUI playerDamageTakenDisplay;
    public TextMeshProUGUI enemyDamageTakenDisplay;


    public RectTransform noticePanel;
    public TextMeshProUGUI noticeText;

    [Header("Result UI")]
    public RectTransform resultPanel;
    public TextMeshProUGUI battleResult;
    public TextMeshProUGUI rewardText;

    [Header("System")]
    public CharacterBuilder characterBuilder;
    public TurnBaseSystem turnBaseSystem;

    [Header("Characters Config")]
    public List<CharacterConfig> avaliableCharacterConfigs = new List<CharacterConfig>();
    public CharacterConfig currentUsingCharacterConfig;


    [Header("In Game")]
    public SpriteRenderer characterRenderer;
    public SpriteRenderer enemyRenderer;

    public Transform background;
    public TextMeshProUGUI roomCount;
    public TextMeshProUGUI turnOwner;


    [Header("GameState")]
    [SerializeField]
    GameState currentState;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            startCanvas.gameObject.SetActive(false);
            characterSelectCanvas.gameObject.SetActive(false);
            gameplayCanvas.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ChangeState(new StartState(this));
        
    }
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
        //Debug.Log($"Changed state to {newState.GetType().Name}");
    }

    public List<CharacterConfig> GetAvaliableCharacter()
    {
        return avaliableCharacterConfigs;
    }
}
