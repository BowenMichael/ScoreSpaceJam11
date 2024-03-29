using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject store;
    public StoreManager sm;
    public Text text;
    public string storeScene;
    public string endScene;
    public float stageScaling;
    private int score;
    private int roomsCleared;
    private int stage=1;


    // Start is called before the first frame update
    void Start()
    {
        sm = store.gameObject.GetComponentInChildren<StoreManager>();
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
        stage++;
        PlayerPrefs.SetInt("Score", score);
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
        Time.timeScale = 1;
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SceneManager.LoadScene(endScene, LoadSceneMode.Single);
    }

    public void openStore()
    {
        store.SetActive(true);
        sm.onOpenStore();
        Time.timeScale = 0;
    }

    public void closeStore()
    {
        store.SetActive(false);
        score = sm.updateScoreOnClose();
        Time.timeScale = 1;
        GetComponent<RoomManager>().resetRooms();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public float getScaling()
    {
        return stageScaling*stage;
    }

    public int getScore()
    {

        return score;
    }
    public int removeScore(int cost)
    {
        score -= cost;
        text.text = "Score: " + this.score;
        return score;
    }



}
