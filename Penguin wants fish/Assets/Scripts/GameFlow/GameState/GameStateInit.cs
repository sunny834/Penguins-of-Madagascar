using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameStateInit :GameState
{
    public GameObject MainMenuUI;
    [SerializeField] public TextMeshProUGUI HiScore;
    [SerializeField] public TextMeshProUGUI FishCounts;
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Init);
        HiScore.text = "High Score:" + "TBD"; 
        FishCounts.text = "Fishes:" + "TBD";
        MainMenuUI.SetActive(true);
    }
    public override void Destruct()
    {
        MainMenuUI.SetActive(false);
    }

    public void OnClickPlay()
    {
        brain.ChangeSate(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSeason();
    }
    public void OnClickShop()
    {
        // brain.ChangeSate(GetComponent<GameStateShop>());
        Debug.Log("Shop");
    }
}
