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

        return new PlayerData { PlayerName = "Player", BestScore = 0 };
    }
}

[System.Serializable]
public class PlayerData
{
    public string PlayerName { get; set; }
    public int BestScore { get; set; }
}

