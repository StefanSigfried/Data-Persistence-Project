using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class StartSceneManager : MonoBehaviour
{
    public Text WarningText;
    public Text BestScoreText;
    public InputField PlayerNameInput;
    public Button StartButton;
    public Button QuitButton;

    private PlayerData playerData;

    private void Start()
    {
        // Load player data from JSON at the start
        LoadPlayerData_JSON();

        // Display the current best score and name
        if (playerData != null)
        {
            BestScoreText.text = "Best Score: " + playerData.BestScoredPlayerName + " : " + playerData.BestScore;
        }

        // Add listeners for buttons
        StartButton.onClick.AddListener(OnStartButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);

        WarningText.gameObject.SetActive(false);
    }

    void OnStartButtonClicked()
    {
        // Set the player's name and prepare the data for the game scene
        if (string.IsNullOrEmpty(PlayerNameInput.text) || PlayerNameInput.text.Length < 3)
        {
            // Display warning message (assuming you have a UI Text element for this)
            WarningText.text = "Please enter a name with at least 3 characters!";
            WarningText.gameObject.SetActive(true); // Make sure the warning text is visible
        }
        else
        {
            // If the name is valid, hide the warning and proceed
            WarningText.gameObject.SetActive(false);
            playerData.CurrentPlayerName = PlayerNameInput.text;

            // Save the player data to JSON before switching scenes
            SavePlayerData_JSON();

            // Set the player data in GameDataManager before loading the main scene
            GameDataManager.Instance.SetPlayerData(playerData);

            // Load the game scene
            SceneManager.LoadScene("main");
        }
    }

    void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        // Exit play mode in the editor
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a standalone build
        Application.Quit();
#endif
    }

    private void LoadPlayerData_JSON()
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

    private void SavePlayerData_JSON()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

       /* GameDataManager.Instance.SetPlayerData(
            playerData.CurrentPlayerName,
            playerData.BestScoredPlayerName,
            playerData.BestScore
        );*/
    }
}
