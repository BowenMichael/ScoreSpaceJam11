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
    private PlayerController plr;
    private int highScore;
    public Text highScoreText;
    public int healthIncrement = 5;
    public int energyRegenIncrement = 5;
    public int MaxEnergyIncrement = 5;
    public int increaseDist = 1;


    private void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
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

    public void buyItem(ItemController item, int cost )
    {
        Debug.Log(item.iType);
        if (cost <= score)
        {
            item.stock--;
            score -= cost;
        
            switch (item.iType)
            {
                case ItemController.itemType.UNKOWN:
                    return;
                case ItemController.itemType.INCREASE_DIST:
                    plr.increaseDist(increaseDist);
                    break;
                case ItemController.itemType.ENERGY_MAX:
                        plr.increaseMaxEnergy(MaxEnergyIncrement);
                    break;
                case ItemController.itemType.ENERGY_RATE:
                    plr.increaseEnergyRegen(energyRegenIncrement);
                    break;
                case ItemController.itemType.HEALTH:
                    if (!plr.increaseHealth(healthIncrement))
                    {
                        item.notPuchasable("FULL");
                        score += cost;
                        break;
                    }
                    break;
            }
        }
        else
        {
            item.notPuchasable("FUNDS");
        }
    }

    public void sellItem(ItemController item, int cost)
    {
        Debug.Log(item.iType);
        score -= -cost;

        switch (item.iType)
        {
            case ItemController.itemType.UNKOWN:
                return;
            case ItemController.itemType.ITEM:
                break;
            case ItemController.itemType.ENERGY_MAX:
                plr.increaseMaxEnergy(-MaxEnergyIncrement);
                break;
            case ItemController.itemType.ENERGY_RATE:
                plr.increaseEnergyRegen(-energyRegenIncrement);
                break;
            case ItemController.itemType.HEALTH:
                if (!plr.increaseHealth(-healthIncrement))
                {
                    item.notPuchasable("FULL");
                    score += -cost;
                    break;
                }
                break;
        }
      
    }


    public void onOpenStore()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    public int updateScoreOnClose()
    {
        PlayerPrefs.SetInt("Score", score);
        return score;
    }
}
