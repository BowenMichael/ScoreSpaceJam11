using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    private Slider slide;
    public Gradient g;
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
    }

    public void setEnergy(float energy, float maxEnergy)//normalized energy value
    {
        slide.value = energy / maxEnergy;
        slide.fillRect.gameObject.GetComponent<Image>().color = g.Evaluate(energy);
        t.text = ((int)energy) + "/" + (int)maxEnergy;
    }
}
