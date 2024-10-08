using System.IO;
using UnityEngine;

public static class JSONReadWriteUtility
{
    private static string _filePath = Application.persistentDataPath + "/playerData.json";

    public static void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(_filePath, json);
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }


        // Return a default player data instance if no file exists
        return new PlayerData { BestScoredPlayerName = "Player", BestScore = 0 };
    }
}


[System.Serializable]
public class PlayerData
{
    public string BestScoredPlayerName = "Player";  // The name of the best-scoring player
    public int BestScore = 0;                       // The best score
    public string CurrentPlayerName = "";           // The current player's name
}

