using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Animator ani;

    private void Start()
    {
       ani= GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupFish();
            Debug.Log("Collided with fish +1");
        }
        else
            Debug.Log("no");
    }
    private void PickupFish()
    {
        //+fish Count
        //+ score
        // play sfx
        // trigger an animation
        ani.SetTrigger("Pickup");
        GameStats.Instance.OnCollectFish();
    }
    public void OnShowChunk()
    {
        ani?.SetTrigger("Idle");
    }
}
