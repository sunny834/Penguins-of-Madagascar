
using UnityEngine;

public class GameStateDeath : GameState
{
    public override void Construct()
    {
        GameManager.Instance.motor.PauseGame();
    }
    public override void UpdateState()
    {
        if(InputManager.Instance.SwipeDown)
        {
            ToMenu();
        }
        if (InputManager.Instance.SwipeUp)
        {
            ResumeGame();
        }

    }
    public void ToMenu()
    {
        brain.ChangeSate(GetComponent<GameStateInit>());
    }
    public void ResumeGame()
    {
        Debug.Log("heyyy");
        GameManager.Instance.motor.RespawnPlayer();
        brain.ChangeSate(GetComponent<GameStateGame>());
    }
}
