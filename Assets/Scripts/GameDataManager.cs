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

    public void SetPlayerData(string currentPlayerName, string bestScoredPlayerName, int bestScore)
    {
        CurrentPlayerName = currentPlayerName;
        BestScoredPlayerName = bestScoredPlayerName;
        BestScore = bestScore;
    }

    public void SaveBestScore()
    {
        PlayerData data = new PlayerData
        {
            bestScoredPlayerName = BestScoredPlayerName,
            bestScore = BestScore
        };
        JSONReadWriteUtility.SaveData(data);
    }
}
