using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    public GameObject obstacle;
    private GameObject[] obstacleS;

    Rigidbody2D myBody1, myBody2, physicObstacle;

    public float Speed = -3f;
    float bgLength = 0;
    float obstacleChangeTime = 0;
    public int numberOfObstacles = 5;
    int counter = 0;
    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        myBody1 = bg1.GetComponent<Rigidbody2D>();
        myBody2 = bg2.GetComponent<Rigidbody2D>();
        obstacleS = new GameObject[numberOfObstacles];

        bgLength = bg1.GetComponent<BoxCollider2D>().size.x;
        BgMotion(Speed);

        for (int i = 0; i < obstacleS.Length; i++)
        {
            obstacleS[i] = Instantiate(obstacle, new Vector3(-20, -20), Quaternion.identity);
            physicObstacle = obstacleS[i].AddComponent<Rigidbody2D>();
            physicObstacle.gravityScale = 0;
            physicObstacle.velocity = new Vector2(Speed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bg1.transform.position.x <= -bgLength)
            bg1.transform.position += new Vector3(bgLength * 2, 0);

        if (bg2.transform.position.x <= -bgLength)
            bg2.transform.position += new Vector3(bgLength * 2, 0);

        obstacleChangeTime += Time.deltaTime;

        float obstacleEmergenceTime = 3 ;

        if(obstacleChangeTime > obstacleEmergenceTime)
        {
            obstacleChangeTime = 0;
            obstacleS[counter].transform.position = new Vector3(10, -3.4f);
            counter++;
            if(counter >= obstacleS.Length)
            {
                counter = 0;
            }
        }
    }

   public void GameOver()
    {
        for(int i = 0; i < obstacleS.Length; i++)
        {
            obstacleS[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            BgMotion(0);
        }
    }

    void BgMotion(float speed)
    {
        myBody1.velocity = new Vector2(speed, 0);
        myBody2.velocity = new Vector2(speed, 0);
    }

    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0f;
            pause = true;
        }
      else  if (pause)
        {
            Time.timeScale = 1f;
            pause = false;
        }
    }
}
