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
   
    //circle field
    [SerializeField] private Image completionCircle;
    public float ReviveTime = 3.5f;
    private float DeathTime;

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
       

        SaveManager.Instance.saveGame();
        HiScore.text = "HighScore " + SaveManager.Instance.SaveState.HighScore;
        FishCount.text = "Fish " + SaveManager.Instance.SaveState.HighestFish;
        CurrentHeartCollected.text =""+ SaveManager.Instance.SaveState.TotalHearts;
        CurrentHiScore.text = GameStats.Instance.CurrentScore.ToString("00000");
        CurrentFishCatched.text =GameStats.instance.CurrentFish.ToString();
      //  CurrentHeartCollected.text= GameStats.instance.TotalHeart.ToString();
        
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
       


    }
    public void ResumeGame()
    {
        if (SaveManager.Instance.SaveState.TotalHearts >= 1 || GameStats.Instance.CurrentHeart>=1)
        {
            GameStats.Instance.CurrentHeart--;
            AudioManager.Instance.ResumeAudio();
            Enemy.Idle();
            SaveManager.Instance.SaveState.TotalHearts -= 1;
           // completionCircle.gameObject.SetActive(false);
            Debug.Log("heyyy");
            brain.ChangeSate(GetComponent<GameStateGame>());
            GameManager.Instance.motor.RespawnPlayer();
        }
        else
        {
            Debug.Log("heyyylooooo");
            completionCircle.gameObject.SetActive(false);
            
        }
    
    }
}
