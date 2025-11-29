using UnityEngine;

public abstract class GameState
{
    protected GameManager gameManager;
    public GameState(GameManager gm)
    {
        gameManager = gm;
    }

    public abstract void Enter();
    public abstract void Exit();
}
