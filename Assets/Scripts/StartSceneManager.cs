using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class StartSceneManager : MonoBehaviour
{
    public Text BestScoreText;
    public InputField PlayerNameInput;
    public Button StartButton;
    public Button QuitButton;

    private PlayerData playerData;

    private void Start()
    {
        // Load player data from JSON at the start
        LoadPlayerData();

        // Display the current best score and name
        if (playerData != null)
        {
            BestScoreText.text = "Best Score: " + playerData.playerName + " : " + playerData.bestScore;
        }

        // Add listeners for buttons
        StartButton.onClick.AddListener(OnStartButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnStartButtonClicked()
    {
        // Set the player's name and prepare the data for the game scene
        if (!string.IsNullOrEmpty(PlayerNameInput.text))
        {
            playerData.playerName = PlayerNameInput.text;
        }

        // Save the player data to JSON before switching scenes
        SavePlayerData();

        // Load the game scene
        SceneManager.LoadScene("main");
    }

    void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            playerData = new PlayerData(); // Create a new instance if no data exists
        }
    }

    private void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }
}
