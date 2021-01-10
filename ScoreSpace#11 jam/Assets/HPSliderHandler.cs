using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSliderHandler : MonoBehaviour
{
    public Slider slide;
    public Gradient g;
    // Start is called before the first frame update
    void Start()
    {
        //slide = GetComponent<Slider>();
    }

    public void setHealth(float health)//normalized energy value
    {
        slide.value = health;
        slide.fillRect.gameObject.GetComponent<Image>().color = g.Evaluate(health);
    }
}
