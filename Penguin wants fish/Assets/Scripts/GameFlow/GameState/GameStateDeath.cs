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
        GameManager.Instance.motor.ResetPlayer();
      

    }
    public void ResumeGame()
    {
        Debug.Log("heyyy");
        brain.ChangeSate(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    
    }
}
