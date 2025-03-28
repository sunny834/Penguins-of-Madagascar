using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public GameObject GameCanvas;
    [SerializeField] public TextMeshProUGUI HiScore;
    [SerializeField] public TextMeshProUGUI FishCounts;
    [SerializeField] public TextMeshProUGUI HeartCounts; 
    [SerializeField] public TextMeshProUGUI HeartCurrentCounts;
    [SerializeField] private AudioClip GamePlayMusic;
    [SerializeField] private GameStateDeath DeathScript;
    
    ////public int TotalheartCollect;
    //public bool Isheartcollected=false;
    public int Currentpick = 1;
    public void Start()
    {
        HeartCurrentCounts.gameObject.SetActive(false);
        HeartCounts.text = SaveManager.Instance.SaveState.TotalHearts.ToString("000");

    }
    public override void Construct()
    {
        GameManager.instance.motor.ResumeGame();
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Game);
        GameStats.Instance.OnCollectionFish += OnCollectFish;
        GameStats.Instance.OnScoreChange += OnScore;
        GameStats.Instance.OnCollectHeartss += OnCollectHeartss;
        DeathScript.OnUseHeart += Onuseheart;
        HiScore.text = "xTBD";
        FishCounts.text = "TBD";
        //HeartCounts.text = "0";
        GameCanvas.SetActive(true);
        AudioManager.Instance.PlayMusicWithXFade(GamePlayMusic,0.5f);
      


    }

    private void Onuseheart(int TotalUsedHeart)
    {
        
        HeartCounts.gameObject.SetActive(false);
        if (TotalUsedHeart >= 0)
        {
            SaveManager.Instance.SaveState.TotalHearts = -1;
        }
        else
        {
            Debug.Log(TotalUsedHeart);
        }
        Debug.Log(TotalUsedHeart);
        HeartCurrentCounts.text = TotalUsedHeart.ToString("000");
        
    }

    private void OnScore(float Score)
    {
        HiScore.text=Score.ToString("0000000");
    }

    

    private void OnCollectFish(int TotalCollected)
    {
        FishCounts.text = TotalCollected.ToString("000");
    }
    private void OnCollectHeartss(int TotalHeartCollected)
    {
          HeartCounts.gameObject.SetActive(false);
          HeartCurrentCounts.gameObject.SetActive(true);
          Debug.Log(TotalHeartCollected);
          HeartCurrentCounts.text = TotalHeartCollected.ToString("000");
         
        //SaveManager.Instance.SaveState.TotalHearts= + 1;



        //Currentpick = SaveManager.Instance.SaveState.TotalHearts;
        // Currentpick++;
        //HeartCounts.text = Currentpick.ToString();
        //SaveManager.Instance.SaveState.TotalHearts += 1;




    }
    public override void Destruct()
    {
        GameCanvas.SetActive(false);
        GameStats.Instance.OnCollectionFish -= OnCollectFish;
        GameStats.Instance.OnScoreChange -= OnScore;
        GameStats.Instance.OnCollectHeartss -= OnCollectHeartss;
        DeathScript.OnUseHeart -= Onuseheart;
    }
    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.sceneGeneration.ScanPosition();
       
    }
}
