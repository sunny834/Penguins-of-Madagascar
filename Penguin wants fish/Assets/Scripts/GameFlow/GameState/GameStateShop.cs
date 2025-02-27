using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameStateShop : GameState
{
    [Header("UI Elements")]
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public GameObject shopUI;

    [Header("Hat System")]
    public HatLogic hatLogic;
    public GameObject HatPrefab;
    public Transform hatContainer;

    private Hat[] hats;
    private bool isInit = false;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat");

        if (hats == null || hats.Length == 0)
        {
            Debug.LogWarning("No hats found in Resources.");
            return;
        }

        shopUI.SetActive(true);

        if (!isInit)
        {
            UpdateFishCount();
            UpdateCurrentHatName();
            PopulateShop();
            isInit = true;
        }
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnClickHome()
    {
        brain.ChangeSate(GetComponent<GameStateInit>());
        Debug.Log("Returning to Home.");
    }

    private void PopulateShop()
    {
        if (HatPrefab == null)
        {
            Debug.LogError("HatPrefab is not assigned in the inspector!");
            return;
        }

        if (hatContainer == null)
        {
            Debug.LogError("HatContainer is not assigned in the inspector!");
            return;
        }

        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject hatGO = Instantiate(HatPrefab, hatContainer);

            if (hatGO == null)
            {
                Debug.LogError("Failed to instantiate hat prefab!");
                continue;
            }

            // Validate child objects
            Transform thumbnail = hatGO.transform.GetChild(0);
            Transform hatName = hatGO.transform.GetChild(1);
            Transform hatPrice = hatGO.transform.GetChild(2);

            if (thumbnail == null || hatName == null || hatPrice == null)
            {
                Debug.LogError($"Hat prefab structure is incorrect! Check the prefab's child elements.");
                continue;
            }

            // Add click listener
            hatGO.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            // Set thumbnail
            Image hatImage = thumbnail.GetComponent<Image>();
            if (hatImage != null) hatImage.sprite = hats[index].Thumbnail;

            // Set hat name
            TextMeshProUGUI hatNameText = hatName.GetComponent<TextMeshProUGUI>();
            if (hatNameText != null) hatNameText.text = hats[index].ItemName;

            // Set price/owned status
            TextMeshProUGUI hatPriceText = hatPrice.GetComponent<TextMeshProUGUI>();
            if (hatPriceText != null)
            {
                Debug.Log("Save file path: " + Application.persistentDataPath);
                hatPriceText.text = SaveManager.Instance.SaveState.UnlockHatFlag[index] == 0
                    ? hats[index].ItemPrice.ToString()
                    : "Owned";
            }
        }
    }


    private void OnHatClick(int index)
    {
        var saveState = SaveManager.Instance.SaveState;
        if (saveState.UnlockHatFlag[index] == 1) // Already owned
        {
            saveState.CurrentHatIndex = index;
            UpdateCurrentHatName();
            hatLogic.SelectHat(index);
            SaveManager.Instance.saveGame();
        }
        else if (hats[index].ItemPrice <= saveState.HighestFish) // Can buy
        {
            saveState.HighestFish -= hats[index].ItemPrice;
            saveState.UnlockHatFlag[index] = 1;
            saveState.CurrentHatIndex = index;

            UpdateFishCount();
            UpdateCurrentHatName();
            hatLogic.SelectHat(index);
            SaveManager.Instance.saveGame();

            // Update price text to "Owned"
            hatContainer.GetChild(index).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Owned";
        }
        else
        {
            Debug.Log("Not enough fish!");
        }
    }

    private void UpdateFishCount()
    {
        totalFish.text = SaveManager.Instance.SaveState.HighestFish.ToString("000");
    }

    private void UpdateCurrentHatName()
    {
        currentHatName.text = hats[SaveManager.Instance.SaveState.CurrentHatIndex].ItemName;
    }
}
