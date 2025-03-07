using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Sprite playSprite;   // Assign your "Play" icon
    public Sprite pauseSprite;  // Assign your "Pause" icon
    public Button pauseButton;  // Assign the button in the Inspector

    private bool isPaused = false;
    private Image buttonImage;

    void Start()
    {
        if (pauseButton != null)
        {
            buttonImage = pauseButton.GetComponent<Image>();
            UpdateButtonSprite();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            AudioManager.Instance.pause();
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            AudioManager.Instance.ResumeAudio();
        }

        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = isPaused ? playSprite : pauseSprite;
        }
    }
}
