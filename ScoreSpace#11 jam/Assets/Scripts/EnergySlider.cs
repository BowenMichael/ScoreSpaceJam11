using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    private Slider slide;
    public Gradient g;
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
    }

    public void setEnergy(float energy)//normalized energy value
    {
        slide.value = energy;
        slide.fillRect.gameObject.GetComponent<Image>().color = g.Evaluate(energy);
    }
}
