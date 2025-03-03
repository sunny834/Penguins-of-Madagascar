using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Animator ani;
    //[SerializeField] private AudioClip FishSound;

    private void Start()
    {
        ani = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupHeart();
            Debug.Log("Collided with heart +1");
        }
        else
            Debug.Log("no");
    }
    private void PickupHeart()
    {
        //+fish Count
        //+ score
        // play sfx
        // trigger an animation
        //ani.SetTrigger("Pickup");
        // AudioManager.Instance.PlayMusicWithXFade(FishSound, 0.7f);
        GameStats.Instance.OnCollectHeart();
        gameObject.SetActive(false);
    }
    public void OnShowChunk()
    {
        ani?.SetTrigger("Idle");
    }
}
