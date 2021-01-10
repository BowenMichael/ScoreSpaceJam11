using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTextHandler : MonoBehaviour
{
    public Text highScoreText;
    public Text yourScoreText;
    public Text roomsClearedText;
    public Text fastestStageText;
    public Text FastestStageTimeText;
    public bool testing;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text += PlayerPrefs.GetInt("HighScore");
        yourScoreText.text += PlayerPrefs.GetInt("Score");
        roomsClearedText.text += PlayerPrefs.GetInt("RoomsCleared");
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            highScoreText.text = "high score: " + PlayerPrefs.GetInt("HighScore");
            yourScoreText.text = "Score: " +PlayerPrefs.GetInt("Score");
            roomsClearedText.text = "Rooms Cleared" + PlayerPrefs.GetInt("RoomsCleared");
        }
    }
}
