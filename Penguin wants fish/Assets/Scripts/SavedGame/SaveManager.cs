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

    private const string saveFileName = "/data.peng"; // Ensure there's a slash before the filename
    private BinaryFormatter formatter;
    public Save SaveState;

    public Action<Save> OnLoad;
    public Action<Save> OnSave;

    private string SaveFilePath => Application.persistentDataPath + saveFileName;

    private void Awake()
    {

        instance = this;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        formatter = new BinaryFormatter();
        Load();
    }

    public void Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.Log("No File Found, creating new save.");
            saveGame();
            return;
        }

        try
        {
            using (FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                SaveState = formatter.Deserialize(file) as Save;
            }
            OnLoad?.Invoke(SaveState);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load save file: {e.Message}");
        }
    }

    public void saveGame()
    {
        try
        {
            SaveState ??= new Save();
            SaveState.LastSaveTime = DateTime.Now;

            using (FileStream file = new FileStream(SaveFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(file, SaveState);
            }
            OnSave?.Invoke(SaveState);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }
}
