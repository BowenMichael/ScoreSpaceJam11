using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject itemHolder;
    public int score;
    public Text scoreText;
    private int highScore;
    public Text highScoreText;
    

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        highScore = PlayerPrefs.GetInt("HighScore");
        foreach(GameObject item in items)
        {
            GameObject tmp;
            ItemController tmpControler;
            tmp = Instantiate(item, itemHolder.transform);
            if(!tmp.TryGetComponent<ItemController>(out tmpControler))
            {
                tmpControler = tmp.AddComponent<ItemController>();
            }

            tmpControler.sm = this;
        }
    }

    public void buyItem(GameObject item, int cost )
    {

    }  
}
