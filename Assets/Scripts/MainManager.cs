using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {

        // Set up the Best Score text at the start using the singleton
        if (!string.IsNullOrEmpty(GameDataManager.Instance.BestScoredPlayerName))
        {
            BestScoreText.text = $"Best Score: {GameDataManager.Instance.BestScoredPlayerName} : {GameDataManager.Instance.BestScore}";
        }
        else
        {
            BestScoreText.text = "Best Score: Name : 0"; // Default text if no player data exists
        }


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Exit the game when ESC is pressed
#if UNITY_EDITOR
                // Exit play mode in the editor
                EditorApplication.isPlaying = false;
#else
        // Quit the application in a standalone build
        Application.Quit();
#endif
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        // Check if the current player has beaten the best score during gameplay
        if (m_Points > GameDataManager.Instance.BestScore)
        {
            // Update the singleton and UI during the game if there's a new best score
            GameDataManager.Instance.BestScore = m_Points;
            GameDataManager.Instance.BestScoredPlayerName = GameDataManager.Instance.CurrentPlayerName;
            // Save the new best score to JSON
            GameDataManager.Instance.SaveBestScore();

            // Update the Best Score text immediately
            BestScoreText.text = $"Best Score: {GameDataManager.Instance.BestScoredPlayerName} : {GameDataManager.Instance.BestScore}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

     /*   if (m_Points > GameDataManager.Instance.BestScore)
        {
            GameDataManager.Instance.BestScore = m_Points;
            GameDataManager.Instance.BestScoredPlayerName = GameDataManager.Instance.CurrentPlayerName;

            // Save the new best score to JSON
            GameDataManager.Instance.SaveBestScore();
        }

        // Update the best score text in the UI
        BestScoreText.text = $"Best Score: {GameDataManager.Instance.BestScoredPlayerName} : {GameDataManager.Instance.BestScore}";*/
    }
}
