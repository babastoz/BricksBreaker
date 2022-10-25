using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color BallColor;
    public float BallSpeed;
    public float BallSize;
    public float PaddleSpeed;
    public int[] HighScores = new int[3] { 0, 0, 0 };
    public string[] HighScoresNames = new string[3] { "-", "-", "-" };


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        LoadSettings();
        LoadHighScores();
    }

    public void ResetData()
    {
        BallSpeed = default;
        BallSize = 0;
        BallColor = default;
        for (int i = 0; i < HighScores.Length; i++)
        {
            HighScores[i] = 0;
        }
        for (int i = 0; i < HighScoresNames.Length; i++)
        {
            HighScoresNames[i] = "";
        }
        SaveSettings();
        SaveHighScores();
    }

    [System.Serializable]
    class SaveData
    {
        public Color BallColor;
        public float BallSpeed;
        public float BallSize;
        public float PaddleSpeed;
        public int firstPlaceScore, secondPlaceScore, thirdPlaceScore;
        public string firstPlaceName, secondPlaceName, thirdPlaceName;
    }

    public void SaveSettings()
    {
        SaveData data = new SaveData();
        data.BallColor = BallColor;
        data.BallSpeed = BallSpeed;
        data.BallSize = BallSize;
        //data.firstPlaceScore = HighScores[0];
        //data.secondPlaceScore = HighScores[1];
        //data.thirdPlaceScore = HighScores[2];

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/settingsfile.json", json);
        Debug.Log("Settings file saved");
    }

    public void LoadSettings()
    {
        //ColorPicker.SelectColor(BallColor);
        string path = Application.persistentDataPath + "/settingsfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BallColor = data.BallColor;
            BallSpeed = data.BallSpeed;
            BallSize = data.BallSize;
            Debug.Log("Settings file loaded");
        }
    }

    public void SaveHighScores()
    {
        SaveData data = new SaveData();
        data.firstPlaceScore = HighScores[0];
        data.secondPlaceScore = HighScores[1];
        data.thirdPlaceScore = HighScores[2];
        data.firstPlaceName = HighScoresNames[0];
        data.secondPlaceName = HighScoresNames[1];
        data.thirdPlaceName = HighScoresNames[2];

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscoresfile.json", json);
        Debug.Log("HighScores file saved");
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscoresfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScores[0] = data.firstPlaceScore;
            HighScores[1] = data.secondPlaceScore;
            HighScores[2] = data.thirdPlaceScore;
            HighScoresNames[0] = data.firstPlaceName;
            HighScoresNames[1] = data.secondPlaceName;
            HighScoresNames[2] = data.thirdPlaceName;
            Debug.Log("HighScores file loaded");
        }
    }
}
