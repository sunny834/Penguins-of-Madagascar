using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;
    private BinaryFormatter formatter;

    //Feilds
    private const string saveFileName = "data.peng";
    public Save SaveState;

    public Action<Save> OnLoad;
    public Action<Save> OnSave;

    private void Awake()
    {
        formatter = new BinaryFormatter();
       // instance = this;
        //try and load ,saved state
        Load();
    }

    public void Load()
    {
        try
        {
            FileStream file = new FileStream( Application.persistentDataPath+saveFileName, FileMode.Open, FileAccess.Read);
            SaveState = formatter.Deserialize(file) as Save;//deserialized
            file.Close();
            OnLoad?.Invoke(SaveState);
        }
        catch 
        {
            Debug.Log("No File Found");
         //   Savefile();
        }
     
    }
    public void saveGame()
    {
        // If there is no previous save,then create a new one!
        if (SaveState = null)
        {
            SaveState = new Save();

            SaveState.LastSaveTime = DateTime.Now;
            FileStream file = new FileStream(Application.persistentDataPath+ saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(file, SaveState);
            file.Close();
            OnSave?.Invoke(SaveState);
        }
    }

}
