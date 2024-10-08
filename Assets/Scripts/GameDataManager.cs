using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public string CurrentPlayerName;       // The current player's name (from the input field)
    public string BestScoredPlayerName;    // The player with the highest score
    public int BestScore;                  // The highest score

    private void Awake()
    {
        // Singleton pattern - ensure only one instance
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerData(PlayerData inData)
    {
        CurrentPlayerName = inData.CurrentPlayerName;
        BestScoredPlayerName = inData.BestScoredPlayerName;
        BestScore = inData.BestScore;
    }
                

    public void SaveBestScore()
    {
        PlayerData data = new PlayerData
        {
            BestScoredPlayerName = BestScoredPlayerName,  // Ensure PascalCase is used
            BestScore = BestScore,
            CurrentPlayerName = CurrentPlayerName
        };
        JSONReadWriteUtility.SaveData(data);
    }
}
