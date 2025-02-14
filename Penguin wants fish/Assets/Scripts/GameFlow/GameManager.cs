using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    public static GameManager instance;

    public PlayerMotor motor;

    private GameState state;
    private void Awake()
    {
        instance = this;
        state = gameObject.GetComponent<GameStateInit>();
        state.Construct();
    }
    private void Update()
    {
        state.UpdateState();
    }
    public void ChangeSate(GameState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }
}
