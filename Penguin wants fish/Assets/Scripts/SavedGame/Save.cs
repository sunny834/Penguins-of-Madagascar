using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save : MonoBehaviour
{
    public int HighScore {  get;  set; }
    public int HighestFish {  get; set; }
    public DateTime LastSaveTime { get; set; }

    public Save()
    {
        HighestFish = 0;
        HighScore = 0;
        LastSaveTime = DateTime.Now;
    }
}
