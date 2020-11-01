using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField]
    private float _snowLevelMax = 6f;

    [SerializeField]
    private float _snowLevel = 0f;
    private float _snowIncrement = 0.5f;
    private float waitTime = 0.65f;
    private bool _buildSnowman = false;
    private PlayerMovement _playerMovement;
    private PlayerSnowCollection _playerSnowCollection;
    
    private IEnumerator _coroutine;

    public UISnowmanBar snowBar;
    public Sprite[] buildStepSprites;
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        _playerSnowCollection = player.GetComponent<PlayerSnowCollection>();
        _playerMovement = player.GetComponent<PlayerMovement>();

        snowBar.SetMaxSnowLevel(_snowLevelMax);
    }

    void Update() {
        float scale = buildStepSprites.Length / _snowLevelMax;

        for (int i = 0; i < buildStepSprites.Length; ++i)
        {
            if( Mathf.Approximately(_snowLevel * scale, ((i + 1) * scale) / scale )) {
                 this.GetComponent<SpriteRenderer>().sprite = buildStepSprites[i];
            }
        };

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_playerSnowCollection.currentSnow >= _snowIncrement)
            {
                _buildSnowman = true;

                // Replace with accessing animator directly from play animation
                _playerMovement.isBuilding(true);
                _coroutine = buildSnowmanCoroutine();
                StartCoroutine(_coroutine);
            }
        }

        if (other.gameObject.tag == "Snowball")
        {
            SnowBall snowBall = other.GetComponent<SnowBall>();

            if(snowBall != null && _snowLevel < _snowLevelMax)
            {
                _snowLevel += snowBall.snowValue;
                snowBar.SetSnowLevel(_snowLevel);
                Destroy(other.gameObject);
            }
        }

        // if tag show ball
        // check the snowball value then update snowlevel
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _buildSnowman = false;

            // Replace with accessing animator directly from play animation
            _playerMovement.isBuilding(false);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }


    IEnumerator buildSnowmanCoroutine()
    {
        while (_buildSnowman && _snowLevel < _snowLevelMax && _playerSnowCollection.currentSnow >= _snowIncrement)
        {
            yield return new WaitForSeconds(waitTime);

            _snowLevel += _snowIncrement;
            snowBar.SetSnowLevel(_snowLevel);
            _playerSnowCollection.DecrementSnow(_snowIncrement);
        }

        // Replace with accessing animator directly from play animation
        _playerMovement.isBuilding(false);
    }
}