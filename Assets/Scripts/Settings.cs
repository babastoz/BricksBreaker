using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField]
    Slider ballSpeed;
    [SerializeField]
    Slider ballSize;

    [SerializeField]
    ColorPicker ColorPicker;
    

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.Instance.BallColor = color;
    }

    void Start()
    {
        //ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        //ColorPicker.onColorChanged += NewColorSelected;
        //ColorPicker.SelectColor(MainManager.Instance.BallColor);
        //if (MainManager.Instance.BallSpeed != 0 && MainManager.Instance.BallSize != 0)
        //{
        //    Debug.Log("BallSpeed&BallSize != 0");
        //    ballSpeed.value = MainManager.Instance.BallSpeed;
        //    ballSize.value = MainManager.Instance.BallSize;
        //}
        //BallSettingsInit();
        LoadSettingsClicked();
        ballSpeed.onValueChanged.AddListener(delegate { NewBallSpeedValue(); });
        ballSize.onValueChanged.AddListener(delegate { NewBallSizeValue(); });
    }

    void BallSettingsInit()
    {
        MainManager.Instance.BallSpeed = ballSpeed.value;
        MainManager.Instance.BallSize = ballSize.value;
    }

    public void NewBallSpeedValue()
    {
        MainManager.Instance.BallSpeed = ballSpeed.value;
    }

    public void NewBallSizeValue()
    {
        MainManager.Instance.BallSize = ballSize.value;
    }
    public void SaveSettingsClicked()
    {
        MainManager.Instance.SaveSettings();
        MainManager.Instance.SaveHighScores();
    }

    public void LoadSettingsClicked()
    {
        MainManager.Instance.LoadSettings();
        ColorPicker.SelectColor(MainManager.Instance.BallColor);
        ballSpeed.value = MainManager.Instance.BallSpeed;
        ballSize.value = MainManager.Instance.BallSize;
    }

    public void ResetDataClicked()
    {
        MainManager.Instance.ResetData();
        ColorPicker.SelectColor(MainManager.Instance.BallColor);
        ballSpeed.value = MainManager.Instance.BallSpeed;
        ballSize.value = MainManager.Instance.BallSize;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
