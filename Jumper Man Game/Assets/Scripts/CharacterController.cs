using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public Sprite[] runSprites;
    public Sprite[] jumpSprites;
    public Text textScore;
    public Text textHighScore;
    public AudioClip jumpSound, ImpactSound, scoreSound;

    SpriteRenderer renderer;
    Rigidbody2D myBody;
    GameplayController gameplayController;

    AudioSource sound;

    bool jump = true;
    static bool gameOver = false;
    float RunningTime = 0;

    int runAnim = 0;
    int score = 0;
    int highScore = 0;
    int questionCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        gameplayController = GameObject.FindGameObjectWithTag("gameplaycontroller").GetComponent<GameplayController>();
        PlayerPrefs.GetInt("highScore");
        PlayerPrefs.GetInt("questionCounter");
        textHighScore.text = "High Score = " + (PlayerPrefs.GetInt("highScore")).ToString();
        sound = GetComponent<AudioSource>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myBody.AddForce(new Vector2(0, 450));
                sound.clip = jumpSound;
                sound.Play();
                jump = false;
            }
        }

        if (!gameOver)
        {
            RunningAnim();
        }
    }

    void RunningAnim()
    {
        RunningTime += Time.deltaTime;
        if (jump)
        {
            renderer.sprite = runSprites[runAnim++];
            if(runAnim == 18)
            {
                runAnim = 0;
            }
        }
        else
        {
            if(myBody.velocity.y > 0)
            {
                renderer.sprite = jumpSprites[0];
            }
            else
            {
                renderer.sprite = jumpSprites[1];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "scorebar")
        {
            score++;
            textScore.text = "Score = " + score;
            sound.clip = scoreSound;
            sound.Play();
            Time.timeScale += 0.3f;
        }

        if(collision.gameObject.tag == "obstacle")
        {
            Time.timeScale = 1f;
            sound.clip = ImpactSound;
            sound.Play();
            gameOver = true;
            gameplayController.GameOver();
            highScore = PlayerPrefs.GetInt("highScore");

            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
            questionCounter++;
            questionCounter += PlayerPrefs.GetInt("questionCounter");
            PlayerPrefs.SetInt("questionCounter", questionCounter);

            if(questionCounter == 2)
            {
                Invoke("QuizScene", 2);
                PlayerPrefs.SetInt("questionCounter", 0);
            }
            else
            {
                Invoke("MainMenu", 2);
            }


        }
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void QuizScene()
    {
        SceneManager.LoadScene("QuizScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump = true;
    }
}
