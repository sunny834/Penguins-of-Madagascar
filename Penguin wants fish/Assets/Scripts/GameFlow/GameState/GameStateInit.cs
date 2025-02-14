using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateInit :GameState
{
    public override void UpdateState()
    {
        if (InputManager.Instance.Tap)
        {
            brain.ChangeSate(GetComponent<GameStateGame>());
        }
        
    }
}
