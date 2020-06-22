using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSnowBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSnowLevel(int snow)
    {
        slider.maxValue = snow;
        slider.value = 0;
    }

    public void SetSnowLevel(int snow)
    {
        slider.value = snow;
    }

}
