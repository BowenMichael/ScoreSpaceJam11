using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject store;
    public Text text;
    public string storeScene;
    public string endScene;
    private int score;
    private int roomsCleared;


    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
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

    public void stageComplete()
    {
        PlayerPrefs.SetInt("Score", score);
        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("RoomsCleared", PlayerPrefs.GetInt("RoomsCleared") + roomsCleared);
        //SceneManager.LoadScene(storeScene);
        openStore();
    }

    public void roomComplete()
    {
        roomsCleared++;
        
    }

    public void endGame()
    {
        SceneManager.LoadScene(endScene, LoadSceneMode.Single);
    }

    public void openStore()
    {
        store.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeStore()
    {
        store.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
