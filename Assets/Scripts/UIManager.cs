using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerSnowBar;
    [SerializeField]
    private GameObject _snowmanSnowBar;

    public void SetPlayerSnowBarMax(float snow)
    {
        Slider slider = _playerSnowBar.GetComponent<Slider>();
        slider.maxValue = snow;
        slider.value = 0;
    }

    public void SetSnowLevel(float snow)
    {
        Slider slider = _playerSnowBar.GetComponent<Slider>();
        slider.value = snow;
    }

    public void SetSnowmanSnowBarMax(float snow)
    {
        Slider slider = _snowmanSnowBar.GetComponent<Slider>();
        slider.maxValue = snow;
        slider.value = 0;
    }

    public void SetSnowmanSnowBarLevel(float snow)
    {
        Slider slider = _snowmanSnowBar.GetComponent<Slider>();
        slider.value = snow;
    }
}
