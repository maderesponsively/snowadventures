using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections;
//using System.Linq;

public class Snowman : MonoBehaviour
{
    [SerializeField]
    private float _snowLevelMax = 8f;
    [SerializeField]
    private float _snowLevel = 0f;

    // Can i make this shared?
    private float _snowIncrement = 0.5f; 

    public Sprite[] buildStepSprites;

    private bool _buildSnowman = false;

    private IEnumerator coroutine;

    //private UIManager _uIManager;

    // Start is called before the first frame update
    void Start()
    {
        //_uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //_uIManager.SetSnowmanSnowBarMax(_snowLevelMax);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateSprite()
    {
        float other = buildStepSprites.Length / _snowLevelMax;

        for (int i = 0; i < buildStepSprites.Length; ++i)
        {

            float x = _snowLevel * other;
            float y = i * other / other;
            float z = i + 1 * other / other;

            if (x >= y && x < z)
            {
                this.GetComponent<SpriteRenderer>().sprite = buildStepSprites[i];

                Debug.Log("Updating sprite?");
            }

        };
    }


    private void UpdateSnowLevel()
    {
        _snowLevel = _snowLevel + _snowIncrement;
        //_uIManager.SetSnowmanSnowBarLevel(_snowLevel);
        UpdateSprite();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                if (_buildSnowman == false)
                {
                    _buildSnowman = true;

                    coroutine = buildSnowmanCoroutine();

                    StartCoroutine(coroutine);
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();

            if (player != null)
            {
                _buildSnowman = false;

                StopCoroutine(coroutine);
            }
        }
    }


    IEnumerator buildSnowmanCoroutine()
    {
        while (_buildSnowman == true && _snowLevel < _snowLevelMax)
        {

            yield return new WaitForSeconds(0.25f);

            UpdateSnowLevel();
        }
    }



}