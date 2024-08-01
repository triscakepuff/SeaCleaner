using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int LevelProgress = 0;
    public List<int> LevelScore = new List<int>();
    public int Money = 0;


    //Shop items
    public int UpgradeSlot = 0; //max 3
    public int UpgradeSpeed = 0; //max 3
    public int UpgradeArrow = 0; //max 3
    public int UpgradeShield = 0; //max 3
    public int UpgradeOxygen = 0; //max 5
}

public class SaveManager : MonoBehaviour
{
    public GameData gameData;

    private string saveFilePath;

    string exeDirectory = Path.GetDirectoryName(Application.dataPath);
    private void Awake()
    {
        saveFilePath = Path.Combine(exeDirectory, "gamedata.json");

        gameData = LoadGameData();
    }

    void Start()
    {

    }

    public void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game data saved.");
    }

    public GameData LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game data loaded.");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found, initializing with default values.");
            return new GameData();
        }
    }

    public void NewGame()
    {
        SaveGameData(new GameData());
    }
}
