using UnityEditor;
using UnityEngine;

public class StartState : GameState
{
    public StartState(GameManager gManager) : base(gManager) { }

    public override void Enter()
    {
        gameManager.startCanvas.gameObject.SetActive(true);

        gameManager.startBtn.onClick.AddListener(OnStartGame);
        gameManager.exitBtn.onClick.AddListener(OnExitGame);
    }
    public override void Exit()
    {
        gameManager.startCanvas.gameObject.SetActive(false);

        gameManager.startBtn.onClick.RemoveAllListeners();
        gameManager.exitBtn.onClick.RemoveAllListeners();
    }

    void OnStartGame()
    {
        gameManager.ChangeState(new CharacterSelectState(gameManager));
    }

    void OnExitGame()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }
}
