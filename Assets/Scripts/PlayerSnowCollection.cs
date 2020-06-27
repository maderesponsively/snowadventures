using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnowCollection : MonoBehaviour
{
    public bool isFull = false;

    [SerializeField] private float _maxSnow = 3f;
    [SerializeField] private float _currentSnow = 0f;

    public UIPlayerSnowBar snowBar;

    void Start()
    {
        snowBar.SetMaxSnowLevel(_maxSnow);
    }

    void Update()
    {

    }

    public void IncrementSnow(float value)
    {
        _currentSnow += value;

        snowBar.SetSnowLevel(_currentSnow);

        if (_currentSnow == _maxSnow)
        {
            isFull = true;
        }
    }

    public void DecrementSnow(float value)
    {
        
    }
}
