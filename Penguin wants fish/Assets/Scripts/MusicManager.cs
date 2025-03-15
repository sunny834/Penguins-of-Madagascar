using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    public Image Mute;
    //public Image Unmute;
    public Sprite muteIcon;
    public Sprite unmuteIcon;
    public bool song =true;
    
    public void mute()
    {
        if(song)
        {
            AudioManager.Instance.muteSong();

            Mute.sprite = muteIcon;
            song = false;
        }
        else
        {
           
            AudioManager.Instance.UnmuteAudio();
            Mute.sprite = unmuteIcon;   
            song = true;

        }
       
    }

}
