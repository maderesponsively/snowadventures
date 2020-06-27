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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerSnowCollection.currentSnow >= _snowIncrement)
            {
                _buildSnowman = true;
                _playerMovement.isBuilding(true);
                _coroutine = buildSnowmanCoroutine();
                StartCoroutine(_coroutine);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _buildSnowman = false;
            _playerMovement.isBuilding(false);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    void UpdateSprite()
    {
        float steps = buildStepSprites.Length / _snowLevelMax;

        for (int i = 0; i < buildStepSprites.Length; ++i)
        {
            float x = _snowLevel * steps;
            float y = i * steps / steps;
            float z = i + 1 * steps / steps;

            if (x >= y && x < z)
            {
                this.GetComponent<SpriteRenderer>().sprite = buildStepSprites[i];
            }
        };
    }

    IEnumerator buildSnowmanCoroutine()
    {
        bool complete = _snowLevel < _snowLevelMax;
        bool playerHasSnow = _playerSnowCollection.currentSnow >= _snowIncrement;

        while (_buildSnowman == true && complete && playerHasSnow)
        {
            yield return new WaitForSeconds(waitTime);

            _snowLevel += _snowIncrement;
            snowBar.SetSnowLevel(_snowLevel);
            _playerSnowCollection.DecrementSnow(_snowIncrement);

            UpdateSprite();
        }
    }
}