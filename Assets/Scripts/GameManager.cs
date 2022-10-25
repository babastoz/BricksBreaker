using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public Text PlayerNameInput;
    public GameObject GameOverText, NewHighScoreText, InputField, RestartText, BackMenuButton;
    //public int[] HighScores = new int[3] {0,0,0};
    public int highScore1, highScore2, highScore3;
    public string highScore1Name, highScore2Name, highScore3Name;

    private bool m_Started = false;
    private int m_Points;
    int m_HighScore = 0;
    
    private bool m_GameOver = false;
    bool m_NewHighScore = false;
    bool m_NameEntered = false;

    string PlayerName;

    
    // Start is called before the first frame update
    void Start()
    {
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
        DisplayBestScore();
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                m_NameEntered = false;
                highScore1 = MainManager.Instance.HighScores[0];
                highScore2 = MainManager.Instance.HighScores[1];
                highScore3 = MainManager.Instance.HighScores[2];
                highScore1Name = MainManager.Instance.HighScoresNames[0];
                highScore2Name = MainManager.Instance.HighScoresNames[1];
                highScore3Name = MainManager.Instance.HighScoresNames[2];
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver && !m_NewHighScore)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            //else if (m_NewHighScore)
            //{
            //    HighScore();
            //}
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    //void PrintPoint(int point)
    //{
    //    Debug.Log("Point value: " + point);
    //}

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_NewHighScore)
        {
            NewHighScoreText.SetActive(true);
            InputField.SetActive(true);
            //HighScore();
            //MainManager.Instance.SaveHighScores();
        }
        else
        {
            RestartText.SetActive(true);
        }
        
    }
    void DisplayBestScore()
    {
        BestScoreText.text = $"Best Score: {MainManager.Instance.HighScores[0]}";
    }
    public void CheckScore()
    {
        if (m_Points > highScore3)
        {
            m_NewHighScore = true;
        }
    }

    void HighScore()
    {
        
        Debug.Log("NEW HIGHSCORE!");
        if (m_Points > highScore1 && m_Points > highScore2 && m_Points > highScore3)
        {
            MainManager.Instance.HighScores[0] = m_Points;
            SetPlayerName(0);
            Debug.Log("1st place");
            //m_NewHighScore = false;
            if (highScore1 > highScore2)
            {
                Debug.Log($"Replaced 2nd place score: {highScore2} by old 1st place score: {highScore1}");
                MainManager.Instance.HighScores[1] = highScore1;
                Debug.Log($"Replaced 2nd place name: {highScore2Name} by old 1st place name: {highScore1Name}");
                MainManager.Instance.HighScoresNames[1] = highScore1Name;
                if (highScore2 > highScore3)
                {
                    Debug.Log($"Replaced 3rd place score: {highScore3} by old 2nd place score: {highScore2}");
                    MainManager.Instance.HighScores[2] = highScore2;
                    Debug.Log($"Replaced 3rd place name: {highScore3Name} by old 2nd place name: {highScore2Name}");
                    MainManager.Instance.HighScoresNames[2] = highScore2Name;
                }
            }
        }
        if (m_Points <= highScore1 && m_Points > highScore2 && m_Points > highScore3)
        {
            MainManager.Instance.HighScores[1] = m_Points;
            SetPlayerName(1);
            Debug.Log("2nd place");
            //m_NewHighScore = false;
            if (highScore2 > highScore3)
            {
                Debug.Log("Replace 3rd place");
                MainManager.Instance.HighScores[2] = highScore2;
                MainManager.Instance.HighScoresNames[2] = highScore2Name;
            }
        }
        if (m_Points <= highScore1 && m_Points <= highScore2 && m_Points > highScore3)
        {
            SetPlayerName(2);
            MainManager.Instance.HighScores[2] = m_Points;
            Debug.Log("3rd place");
            //m_NewHighScore = false;
        }
        MainManager.Instance.SaveHighScores();

    }

    public void GetPlayerName()
    {
        PlayerName = PlayerNameInput.text;
        NewHighScoreText.SetActive(false);
        InputField.SetActive(false);
        //m_NameEntered = true;
        HighScore();
        m_NewHighScore = false;
        RestartText.SetActive(true);
        Debug.Log("PlayerName: " + PlayerName);
    }
    public void SetPlayerName(int playerRank)
    {
        MainManager.Instance.HighScoresNames[playerRank] = PlayerName;
    }
}
