using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text text;

    private int score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text.text = "Score: " + score;
    }

    public void addScore()
    {
        this.score++;
        text.text = "Score: " + score;
    }
    public void addScore(int score)
    {
        this.score += score;
        text.text = "Score: " + this.score;
    }


}