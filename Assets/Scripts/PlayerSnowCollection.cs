using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnowCollection : MonoBehaviour
{
    public bool isFull = false;

    [SerializeField] private float _maxSnow = 3f;
    [SerializeField] private float _currentSnow = 0f;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void IncrementSnow(float value)
    {
        _currentSnow += value;

        if(_currentSnow == _maxSnow)
        {
            isFull = true;
        }
    }

    public void DecrementSnow(float value)
    {
        
    }
}
