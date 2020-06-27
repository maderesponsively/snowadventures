using UnityEngine;

public class PlayerSnowCollection : MonoBehaviour
{
    public bool isFull = false;

    [SerializeField]
    private float _maxSnow = 3f;
    public float currentSnow = 0f;

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
        currentSnow += value;

        snowBar.SetSnowLevel(currentSnow);

        if (currentSnow == _maxSnow)
        {
            isFull = true;
        }
    }

    public void DecrementSnow(float value)
    {
        if (currentSnow >= value)
        {
            isFull = false;
            currentSnow -= value;
            snowBar.SetSnowLevel(currentSnow);
        }  
    }
}
