using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderAndFillBar : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateFiller(float currentValue, float maxValue)
    {

        float healthPercent = currentValue / maxValue;
        slider.value = healthPercent;


    }
}
