using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SliderHandler : MonoBehaviour
{
    public Text text;

    private Slider slider;

    public void AssignSliderValue()
    {
        text.text = "Przeciwnicy: " + slider.value;
        GameData.enemiesToSpawn = (int)slider.value;
    }
    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
        AssignSliderValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
