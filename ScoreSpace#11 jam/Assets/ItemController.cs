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
    private bool notPurchasableFlag;
    private float timeForNotPurcahaseableFlag = 1.5f;
    private float timeSinceLastNotPuchaseFlag = 0f;


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
        if (stock > 0)
        {
            sm.buyItem(this, buyPrice);
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
    private void Update()
    {
        
        if (notPurchasableFlag)
        {
            //Debug.Log(timeSinceLastNotPuchaseFlag);
            if (timeSinceLastNotPuchaseFlag > timeForNotPurcahaseableFlag)
            {
                notPurchasableFlag = false;
                buyText.text = buyPrice.ToString();
                buyText.color = Color.green;
            }
            timeSinceLastNotPuchaseFlag += Time.unscaledDeltaTime;
        }
    }

    public void notPuchasable(string reason)
    {
        buyText.text = reason;
        buyText.color = Color.red;
        notPurchasableFlag = true;
        timeSinceLastNotPuchaseFlag = 0f;
    }
}
