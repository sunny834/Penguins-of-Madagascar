using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] private Transform hatContainer;
    private Hat[] hats;
    private List<GameObject> hatModel = new List<GameObject>();
    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        SpawnHats();
        SelectHat( SaveManager.Instance.SaveState.CurrentHatIndex);

    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {

            hatModel.Add(Instantiate(hats[i].Model, hatContainer) as GameObject);
        }
       
    }
    public void DisableAllHats()
    {
        for(int i = 0;i< hatModel.Count;i++)
        {
            hatModel[i].SetActive(false);
        }
    }
    public void SelectHat(int index)
    {
        DisableAllHats();
        hatModel[index].SetActive(true);
    }
}
