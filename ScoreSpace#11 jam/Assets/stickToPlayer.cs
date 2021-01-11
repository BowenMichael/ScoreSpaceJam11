using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class stickToPlayer : MonoBehaviour
{
    public GameObject plr;
    public Gradient g;
    public Slider slide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(plr.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f) + new Vector3(0.0f, 20.0f, 0.0f);
        
    }

    public void setStun(float value, float max)
    {
        slide.value = value / max;
        slide.fillRect.gameObject.GetComponent<Image>().color = g.Evaluate(value);
        //t.text = value.ToString();
    }

    public void resetStun()
    {
        slide.value = 0;
        gameObject.SetActive(false);

    }
}
