using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public StoreManager sm;
    public GameObject item;
    public string name;
    public string desc;
    public int sellPrice;
    public int buyPrice;
    public int stock;

    public Text nameText;
    public Text descText;
    public Text sellText;
    public Text buyText;
    public Button buyButton;
    public Button sellButton;
    public itemType iType;


    public enum itemType
    {
        UNKOWN = -1,
        ITEM,
        HEALTH,
        ENERGY_RATE,
        ENERGY_MAX,

    }

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = name;
        descText.text = desc;
        sellText.text = sellPrice.ToString();
        buyText.text = buyPrice.ToString();

        buyButton.onClick.AddListener(buyItem);
        sellButton.onClick.AddListener(sellItem);
    }

    void buyItem()
    {
        if(stock > 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().upgrade(iType);
            stock--;
        }
        else
        {
            buyText.text = "OUT";
        }
        //sm.
    }

    void sellItem()
    {

    }
}
