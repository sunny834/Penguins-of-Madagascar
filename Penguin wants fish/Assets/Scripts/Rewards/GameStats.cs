using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return instance; } }
    public static GameStats instance;

    //score
    public float CurrentScore;
    public float HighScore;
    public float DistanceModifier=1.5f;

    //Fish
    public int TotalFish;
    public int CurrentFish;
    public int PointsPerFish=4;

    //Action
    public Action<int> OnCollectionFish;
    public Action<float> OnScoreChange;

    //Internal cooldown
    private float LastScoreUpdate;
    private float UpdateScoreDelta = 0.2f;


    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        float s=GameManager.Instance.motor.transform.position.z * DistanceModifier;
        s+=CurrentFish*PointsPerFish+60;
        //Debug.Log(s);
        if(s>0)
        {
            Debug.Log(s +"Inside");
            CurrentScore=s;
            if(Time.time-LastScoreUpdate>UpdateScoreDelta)
            {
                OnScoreChange?.Invoke(CurrentScore);
                LastScoreUpdate = Time.time;
            }
          
        }
        
    }

    public void OnCollectFish()
    {
        CurrentFish++;
        OnCollectionFish?.Invoke(CurrentFish);
    }

    public void ResetSeason()
    {
        CurrentFish = 0;
        CurrentScore=0;
        OnCollectionFish?.Invoke(CurrentFish);
        OnScoreChange?.Invoke(CurrentScore);
        
    }

}
