using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{

    public void SecondAnswer()
    {
        Invoke("GameplayScene", 1);
    }

    public void ThirdAnswer()
    {
        Invoke("MainMenuScene", 1);
    }

    void GameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
