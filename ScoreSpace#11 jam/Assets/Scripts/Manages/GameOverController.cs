using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverController : MonoBehaviour
{
    public string playScene;
    public string menuScene;

    public void onReset()
    {
        clearTempPrefs();
        SceneManager.LoadScene(playScene, LoadSceneMode.Single);
        
    }

    public void onExit()
    {
        clearTempPrefs();
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
    }

    public void clearTempPrefs()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
}
