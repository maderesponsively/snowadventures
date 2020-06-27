﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSnowBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSnowLevel(float snow)
    {
        slider.maxValue = snow;
        slider.value = 0;
    }

    public void SetSnowLevel(float snow)
    {
        slider.value = snow;
    }

}
