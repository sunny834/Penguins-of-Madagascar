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

    //shop item
    public GameObject HatPrefabs;
    public Transform hatContainer;
    private Hat[] hats;

    protected override void Awake()
    {
        base.Awake();
       // brain = GetComponent<GameManager>();
        hats = Resources.LoadAll<Hat>("Hat");
        PopularShop();
    }
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Shop);
       
        totalFish.text =  SaveManager.Instance.SaveState.HighestFish.ToString("000");
        shopUI.SetActive(true);
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
            //price
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            //item name
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
        }

    }
    private void OnHatClick(int i)
    {
        Debug.Log("Hat was clicked!" +  i);
        currentHatName.text = hats[i].name;
        hatLogic.SelectHat(i);
    }
}
