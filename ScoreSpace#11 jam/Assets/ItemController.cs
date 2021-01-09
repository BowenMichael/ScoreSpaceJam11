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

    public Text nameText;
    public Text descText;
    public Text sellText;
    public Text buyText;
    public Button buyButton;
    public Button sellButton;


    enum itemType
    {
        UNKOWN = -1,

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

    // Update is called once per frame
    void Update()
    {
        
    }

    void buyItem()
    {
        //sm.
    }

    void sellItem()
    {

    }
}
