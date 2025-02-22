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
        // Ensure SaveState is loaded and not null
        var saveState = SaveManager.Instance?.SaveState;
        if (saveState != null)
        {
            HiScore.text = "High Score: " + saveState.HighScore.ToString();
            FishCounts.text = "Fish: " + saveState.HighestFish.ToString();
        }
        else
        {
            Debug.LogWarning("Save data is null. Initializing defaults.");
            HiScore.text = "High Score: 0";
            FishCounts.text = "Fish: 0";
        }
        //  HiScore.text = "HighScore:" + SaveManager.Instance.SaveState.HighScore.ToString();
        //   FishCounts.text = "Fish:" + SaveManager.Instance.SaveState.HighestFish.ToString();
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
