using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class GameStateShop : GameState
{
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;   
    public HatLogic hatLogic;
    public GameObject shopUI;
    private bool isInit=false;

    //shop item
    public GameObject HatPrefabs;
    public Transform hatContainer;
    private Hat[] hats;

   
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat");
        shopUI.SetActive(true);
        if (!isInit)
        {
            totalFish.text = SaveManager.Instance.SaveState.HighestFish.ToString("000");
            currentHatName.text = hats[SaveManager.Instance.SaveState.CurrentHatIndex].ItemName;
            PopularShop();
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
        Debug.Log("Shop");
    }
    public void PopularShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(HatPrefabs,hatContainer)as GameObject;
            //Button
            go.GetComponent<Button>().onClick.AddListener(()=>OnHatClick(index));
            //Thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite=hats[index].Thumbnail;
            if (SaveManager.Instance.SaveState.UnlockHatFlag[i] == 0)
            {
                //price
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            }
            else 
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }

          
            //item name
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
        }

    }
    private void OnHatClick(int i)
    {
        if(SaveManager.Instance.SaveState.UnlockHatFlag[i]==1)
        {
            SaveManager.Instance.SaveState.CurrentHatIndex = i;
            Debug.Log("Hat was clicked!" + i);
            currentHatName.text = hats[i].name;
            hatLogic.SelectHat(i);
            SaveManager.Instance.saveGame();
        }
        // if we don't have it, can we buy it?
        else if (hats[i].ItemPrice<=SaveManager.Instance.SaveState.HighestFish)
        {
           SaveManager.Instance.SaveState.HighestFish-= hats[i].ItemPrice;
           SaveManager.Instance.SaveState.UnlockHatFlag[i] = 1;
           currentHatName.text= hats[i].ItemName;
           hatLogic.SelectHat(i); 
           totalFish.text= SaveManager.Instance.SaveState.HighestFish.ToString("000");
           SaveManager.Instance.saveGame();

           hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";


        }
        //else , no
        else
        {
            Debug.Log("Not enough fish");
        }

       
    }
}
