using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    public GameObject DeathUI;
    [SerializeField] private TextMeshProUGUI HiScore;
    [SerializeField] private TextMeshProUGUI FishCount;
    [SerializeField] private TextMeshProUGUI CurrentHiScore;
    [SerializeField] private TextMeshProUGUI CurrentFishCatched; 
    [SerializeField] private TextMeshProUGUI CurrentHeartCollected;
    [SerializeField] private AudioClip DeathSound;
    public EnemyAI Enemy;
    [SerializeField] private GameStateGame gamestate;

    [SerializeField] private MusicManager music;
   
    //circle field
    [SerializeField] private Image completionCircle;
    public float ReviveTime = 3.5f;
    private float DeathTime;

    public Action<int> OnUseHeart;

    public override void Construct()
    {
        AudioManager.Instance.PlaySFX(DeathSound,1f);
        GameManager.Instance.motor.PauseGame();
        DeathUI.SetActive(true);
     
        DeathTime = Time.time;

        if (SaveManager.Instance.SaveState.HighScore < (int)GameStats.instance.CurrentScore)
        {
            SaveManager.Instance.SaveState.HighScore = (int)GameStats.instance.CurrentScore;
            HiScore.color = Color.green;
            CurrentHiScore.color = Color.cyan;
        }
        else
            HiScore.color= Color.red;
          

        SaveManager.Instance.SaveState.HighestFish += GameStats.Instance.CurrentFish;
        if (GameStats.Instance.TotalHeart==0)
        {
            if (SaveManager.Instance.SaveState.TotalHearts == 0)
            {
                SaveManager.Instance.SaveState.TotalHearts = 0;
                //Debug.Log("sunny" +
                //SaveManager.Instance.SaveState.TotalHearts);
            }
            else
            {
                SaveManager.Instance.SaveState.TotalHearts = SaveManager.Instance.SaveState.TotalHearts;
            }
            
        }
        else
        {
            SaveManager.Instance.SaveState.TotalHearts = GameStats.Instance.TotalHeart;
        }
       


        SaveManager.Instance.saveGame();
        HiScore.text = "HighScore " + SaveManager.Instance.SaveState.HighScore;
        FishCount.text = "Fish " + SaveManager.Instance.SaveState.HighestFish;
       // CurrentHeartCollected.text =""+GameStats.Instance.TotalHeart;

        CurrentHiScore.text = GameStats.Instance.CurrentScore.ToString("00000");
        CurrentFishCatched.text =GameStats.instance.CurrentFish.ToString();
        CurrentHeartCollected.text= SaveManager.Instance.SaveState.TotalHearts.ToString();
        
    }
    public override void Destruct()
    {
        DeathUI.SetActive(false);
    }
    public override void UpdateState()
    {
        float ratio=(Time.time-DeathTime)/ReviveTime;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1-ratio;
        if (ratio > 1)
        {
            AudioManager.Instance.StopAudio();
            completionCircle.gameObject.SetActive(false);
        }

    }
    public void EnableRevive()
    {
        completionCircle.gameObject.SetActive(true);
        AudioManager.Instance.pause();
    }
    public void ToMenu()
    {
        brain.ChangeSate(GetComponent<GameStateInit>());
        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.sceneGeneration.ResetWorld();
        Enemy.ResetEnemy();
        gamestate.HeartCounts.gameObject.SetActive(true );
        

       


    }
    public void ResumeGame()
    {
        if (GameStats.Instance.TotalHeart>= 1|| SaveManager.Instance.SaveState.TotalHearts>=1)
        {
           
            if (SaveManager.Instance.SaveState.TotalHearts ==1)
            {
                
                //GameStats.Instance.TotalHeart--;
                OnUseHeart?.Invoke(GameStats.Instance.TotalHeart);
                if (music.song)
                {
                    AudioManager.Instance.ResumeAudio();
                }
                else
                {
                    AudioManager.Instance.StopAudio();
                }
                Enemy.Idle();

                // completionCircle.gameObject.SetActive(false);
                Debug.Log("heyyy");
                brain.ChangeSate(GetComponent<GameStateGame>());
                GameManager.Instance.motor.RespawnPlayer();
                GameStats.Instance.OnuseHeart();
                SaveManager.Instance.SaveState.TotalHearts = 0;
                

            }
            else
            {
                SaveManager.Instance.SaveState.TotalHearts -= 1;
                //GameStats.Instance.TotalHeart--;
                OnUseHeart?.Invoke(GameStats.Instance.TotalHeart);
                AudioManager.Instance.ResumeAudio();
                Enemy.Idle();

                // completionCircle.gameObject.SetActive(false);
                Debug.Log("heyyy");
                brain.ChangeSate(GetComponent<GameStateGame>());
                GameManager.Instance.motor.RespawnPlayer();
                GameStats.Instance.OnuseHeart();

            }
           
           
        }
        else
        {
            Debug.Log("heyyylooooo");
            completionCircle.gameObject.SetActive(false);
           // gamestate.HeartCounts.gameObject.SetActive(true);

        }
    
    }
}
