using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresManager : MonoBehaviour
{
    public Text firstScore, secondScore, thirdScore;

    // Start is called before the first frame update
    void Start()
    {
        firstScore.text = $"1. {MainManager.Instance.HighScoresNames[0]} - {MainManager.Instance.HighScores[0]}";
        secondScore.text = $"2. {MainManager.Instance.HighScoresNames[1]} - {MainManager.Instance.HighScores[1]}";
        thirdScore.text = $"3. {MainManager.Instance.HighScoresNames[2]} - {MainManager.Instance.HighScores[2]}";
    }

}
