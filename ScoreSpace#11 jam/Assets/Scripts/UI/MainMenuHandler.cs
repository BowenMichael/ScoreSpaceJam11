using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Button play;
    public string gameScene;

    private void Start()
    {
        play.onClick.AddListener(onClick);
    }

    void onClick()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }
}
