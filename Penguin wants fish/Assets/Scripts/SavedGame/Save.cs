using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save 
{
    [NonSerialized]private const int HAT_COUNT = 16;
    public int HighScore {  get;  set; }
    public int HighestFish {  get; set; }
    public DateTime LastSaveTime { get; set; }
    public int CurrentHatIndex { get; set; }
    public byte[] UnlockHatFlag { get; set; }

    public Save()
    {
        HighestFish = 0;
        HighScore = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockHatFlag= new byte[HAT_COUNT];
        UnlockHatFlag[0]=1;
    }
}
