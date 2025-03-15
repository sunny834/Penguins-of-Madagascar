using System.Collections;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Animator ani;

    private void Start()
    {
        ani = GetComponentInParent<Animator>();

        if (ani == null)
        {
            Debug.LogWarning("Animator component not found on parent of Heart.");
        }

        StartCoroutine(HeartSpawnRoutine()); // Start the enable-disable loop
    }

    private IEnumerator HeartSpawnRoutine()
    {
        while (true)  // Infinite loop to keep repeating the cycle
        {
            yield return new WaitForSeconds(60f); // Wait for 60 seconds
            gameObject.SetActive(true); // Enable heart
            Debug.Log("Heart Enabled");
            yield return new WaitForSeconds(15f); // Wait for 15 seconds
            gameObject.SetActive(false); // Disable heart
            Debug.Log("Heart Disabled");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupHeart();
            Debug.Log("Collided with heart +1");
        }
        else
        {
            Debug.Log("No collision with player.");
        }
    }

    private void PickupHeart()
    {
        if (GameStats.Instance != null)
        {
            GameStats.Instance.OnCollectHeart();
        }
        else
        {
            Debug.LogError("GameStats Instance is null. Ensure it's properly initialized.");
        }

        if (ani != null)
        {
            ani.SetTrigger("Pickup");
        }

        gameObject.SetActive(false);
    }

    public void OnShowChunk()
    {
        if (ani != null)
        {
            ani.SetTrigger("Idle");
        }
    }
}
