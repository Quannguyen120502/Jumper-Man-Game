using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text texthighScore;


    public void PlayBtn()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void resetButton()
    {
        PlayerPrefs.SetInt("highScore", 0);
        PlayerPrefs.SetInt("questionCounter", 0);
        texthighScore.text = "High Score = " + PlayerPrefs.GetInt("highScore").ToString();
    }

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        texthighScore.text = "High Score = " + highScore;
    }
}
