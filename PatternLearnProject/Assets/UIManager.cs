using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillConfig;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{

    GameManager gameManager;
    int currentRoom = 1;
    private void OnEnable()
    {
        if (GameManager.instance != null && EventManager.instance != null)
        {
            //Debug.Log("UI Manager check gameManager and EventManager");
            gameManager = GameManager.instance;

            EventManager.Subscribe<EnemyCreatedEvent>(OnCreateNewEnemy);
            EventManager.Subscribe<SelectedCharacterEvent>(OnSelectCharacter);
            EventManager.Subscribe<CharacterActionTakenEvent>(OnActionTaken);
            EventManager.Subscribe<TurnPhaseChangeEvent>(OnPhaseChanged);
            EventManager.Subscribe<OnVictoryEvent>(OnVictory);
            EventManager.Subscribe<OnDelayEnd>(OnDelayEnd);

        }
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe<EnemyCreatedEvent>(OnCreateNewEnemy);
        EventManager.UnSubscribe<SelectedCharacterEvent>(OnSelectCharacter);
        EventManager.UnSubscribe<CharacterActionTakenEvent>(OnActionTaken);
        EventManager.UnSubscribe<TurnPhaseChangeEvent>(OnPhaseChanged);
        EventManager.UnSubscribe<OnVictoryEvent>(OnVictory);
        EventManager.UnSubscribe<OnDelayEnd>(OnDelayEnd);
    }
    void OnCreateNewEnemy(EnemyCreatedEvent e)
    {
        gameManager.enemyName.text = e.EnemyName;
        gameManager.enemyHPSlider.maxValue = e.EnemyHP;
        gameManager.enemyHPSlider.value = e.EnemyHP;
        gameManager.enemyRenderer.sprite = e.EnemySprite;
        gameManager.enemyRenderer.gameObject.SetActive(true);


    }

    void OnSelectCharacter(SelectedCharacterEvent e)
    {
        //Debug.Log($"PlayerMaxHP {e.ClassBaseHP} PlayerMaxAP {e.ClassAP} ");
        gameManager.characterClassName.text = e.ClassName;
        gameManager.characterPortrait.sprite = e.ClassSprite;
        gameManager.playerHPSlider.maxValue = e.ClassBaseHP;
        gameManager.playerHPSlider.value = e.ClassBaseHP;
        gameManager.playerAPSlider.maxValue = e.ClassAP;
        gameManager.playerAPSlider.value = e.ClassAP;

    }

    void OnActionTaken(CharacterActionTakenEvent e)
    {
        if (e.Source == gameManager.turnBaseSystem.playerCharacter)
        {
            if (e.ActionName == "Attack")
            {
                gameManager.enemyDamageTakenDisplay.text = e.Value.ToString();
                // animate DamageTaken
                StartCoroutine(AnimateDamageTaken(gameManager.enemyDamageTakenDisplay.rectTransform));
                // animate Slider decrease
                StartCoroutine(AnimateSliderBar(gameManager.enemyHPSlider, e.Value, true));
            }
            else if (e.ActionName == "Defense")
            {
                float currentShield = gameManager.turnBaseSystem.playerCharacter.shield;

                gameManager.shields.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentShield.ToString();
                gameManager.shields.gameObject.SetActive(true);
            }
            else if (e.ActionName == "Skill")
            {
                if (e.SkillEffectType == SkillEffectType.Damage)
                {
                    // animate DamageTaken
                    StartCoroutine(AnimateDamageTaken(gameManager.enemyDamageTakenDisplay.rectTransform));
                    // animate Slider decrease
                    StartCoroutine(AnimateSliderBar(gameManager.enemyHPSlider, e.Value, true));
                }
                else if (e.SkillEffectType == SkillEffectType.Heal)
                {
                    // animate DamageTaken
                    StartCoroutine(AnimateDamageTaken(gameManager.playerDamageTakenDisplay.rectTransform));
                    // animate Slider decrease
                    StartCoroutine(AnimateSliderBar(gameManager.playerHPSlider, e.Value, false));
                }
            }
            StartCoroutine(AnimateSliderBar(gameManager.playerAPSlider, gameManager.turnBaseSystem.playerCharacter.maxActionPoint - gameManager.turnBaseSystem.playerCharacter.currentActionPoint, true));
        }
        else if (e.Source == gameManager.turnBaseSystem.currentEnemy)
        {
            if (e.ActionName == "Attack")
            {
                gameManager.playerDamageTakenDisplay.text = e.Value.ToString();
                // animate DamageTaken
                StartCoroutine(AnimateDamageTaken(gameManager.playerDamageTakenDisplay.rectTransform));
                // animate Slider decrease
                StartCoroutine(AnimateSliderBar(gameManager.playerHPSlider, e.Value, true));
            }
            else if (e.ActionName == "Defense")
            {
                float currentShield = gameManager.turnBaseSystem.currentEnemy.shield;

                gameManager.enemyShield.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentShield.ToString();
                gameManager.enemyShield.gameObject.SetActive(true);
            }
            else if (e.ActionName == "Skill")
            {
                if (e.SkillEffectType == SkillEffectType.Damage)
                {
                    // animate DamageTaken
                    StartCoroutine(AnimateDamageTaken(gameManager.playerDamageTakenDisplay.rectTransform));
                    // animate Slider decrease
                    StartCoroutine(AnimateSliderBar(gameManager.playerHPSlider, e.Value, true));
                }
                else if (e.SkillEffectType == SkillEffectType.Heal)
                {
                    // animate DamageTaken
                    StartCoroutine(AnimateDamageTaken(gameManager.enemyDamageTakenDisplay.rectTransform));
                    // animate Slider decrease
                    StartCoroutine(AnimateSliderBar(gameManager.enemyHPSlider, e.Value, false));
                }
            }
        }
    }


    void OnPhaseChanged(TurnPhaseChangeEvent e)
    {
        if (e.newPhase == TurnBaseSystem.BattlePhase.PlayerTurn)// change phase to PlayerTurn
        {
            gameManager.shields.gameObject.SetActive(false);


            gameManager.attackBtn.enabled = true;
            gameManager.skillBtn.enabled = true;
            gameManager.defenseBtn.enabled = true;
            gameManager.undoBtn.enabled = false;
            gameManager.endTurnBtn.enabled = true;

            gameManager.turnOwner.text = "TURN : PLAYER";


            StartCoroutine(AnimateSliderBar(gameManager.playerAPSlider, gameManager.turnBaseSystem.playerCharacter.maxActionPoint - gameManager.turnBaseSystem.playerCharacter.currentActionPoint, false));
        }
        if (e.newPhase == TurnBaseSystem.BattlePhase.EnemyTurn)
        {
            gameManager.enemyShield.gameObject.SetActive(false);

            gameManager.turnOwner.text = "TURN : ENEMY";
        }
    }

    void OnDelayEnd(OnDelayEnd e)
    {
        gameManager.attackBtn.enabled = true;
        gameManager.skillBtn.enabled = true;
        gameManager.defenseBtn.enabled = true;
        gameManager.undoBtn.enabled = false;
        gameManager.endTurnBtn.enabled = true;

        StartCoroutine(TransitionRectTransform(gameManager.noticePanel));
    }
    IEnumerator AnimateDamageTaken(RectTransform target)
    {
        //Fade in and Out
        // slow up right
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = target.anchoredPosition;
        TextMeshProUGUI tmpText = target.GetComponent<TextMeshProUGUI>();
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // เคลื่อนที่ขึ้น
            target.anchoredPosition = startPos + Vector3.up * 50f * t;

            // Fade Out
            tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, 1f - t);
            yield return null;
        }
        tmpText.text = "";
    }

    IEnumerator AnimateSliderBar(Slider target, float valueChange, bool decrease)
    {
        //slide to newValue
        float absValueChange = Mathf.Abs(valueChange);

        float targetValue = decrease
            ? target.value - absValueChange // ลบออกเมื่อเป็น Damage
            : target.value + absValueChange; // บวกเพิ่มเมื่อเป็น Heal

        targetValue = Mathf.Clamp(targetValue, target.minValue, target.maxValue);

        float duration = 0.3f;
        float elapsed = 0f;
        float startValue = target.value;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            target.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        target.value = targetValue;
    }

    void OnVictory(OnVictoryEvent e)
    {
        gameManager.battleResult.text = "victory";
        gameManager.rewardText.text = $"you get: {e.DefeatedEnemy.dropExp} exp";

        gameManager.attackBtn.enabled = false;
        gameManager.skillBtn.enabled = false;
        gameManager.defenseBtn.enabled = false;
        gameManager.undoBtn.enabled = false;
        gameManager.endTurnBtn.enabled = false;

        StartCoroutine(AnimateSliderBar(gameManager.playerAPSlider, gameManager.turnBaseSystem.playerCharacter.maxActionPoint - gameManager.turnBaseSystem.playerCharacter.currentActionPoint, false));

        gameManager.resultPanel.gameObject.SetActive(true);
        StartCoroutine(TransitionRectTransform(gameManager.resultPanel));
    }

    IEnumerator TransitionRectTransform(RectTransform target)
    {
        CanvasGroup cg = target.GetComponent<CanvasGroup>();
        if (cg == null) cg = target.gameObject.AddComponent<CanvasGroup>();

        float fadeInTime = 0.5f;  
        float stayTime = 1.5f;    
        float fadeOutTime = 0.5f; 

        float elapsed = 0f;
        target.anchoredPosition = new Vector2(0, -100); 
        cg.alpha = 0;
        target.gameObject.SetActive(true);

        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeInTime;

            cg.alpha = Mathf.Lerp(0, 1, t);
            target.anchoredPosition = Vector2.Lerp(new Vector2(0, -100), Vector2.zero, t);

            yield return null;
        }
        cg.alpha = 1;
        target.anchoredPosition = Vector2.zero;

        yield return new WaitForSeconds(stayTime);

        elapsed = 0f;
        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeOutTime;

            cg.alpha = Mathf.Lerp(1, 0, t);
            target.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(0, 50), t);

            yield return null;
        }

        cg.alpha = 0;
        target.gameObject.SetActive(false);

        currentRoom += 1;
        gameManager.roomCount.text = $"Room: {currentRoom}";
    }
}
