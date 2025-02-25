using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameCamera
    {
        Init=0,
        Game=1,
        Shop=2,
        Respawn=3
    }
    public static GameManager Instance { get { return instance; } }
    public static GameManager instance;

    public PlayerMotor motor;
    public WorldGeneration worldGeneration;
    public SceneGeneration sceneGeneration;
    public GameObject[] Cameras;

    private GameState state;
    private void Start()
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
    public void ChangeCamera(GameCamera cam)
    {
        foreach (GameObject go in Cameras)
        {
            go.SetActive(false);    
        }
        Cameras[(int)cam].SetActive(true);
    }
}
