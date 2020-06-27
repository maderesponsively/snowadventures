using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPickup : MonoBehaviour
{
    
    [SerializeField]
    private float _initialLevel = 1f;
    private float _currentLevel;
    [SerializeField]
    private float _waitTime = 0.65f;
    [SerializeField]
    private float _decrementalValue = 0.5f;
    private bool _isCollecting = false;

    [SerializeField]
    private Sprite _halfSprite;
    private PlayerMovement _playerMovement;
    private PlayerSnowCollection _playerSnowCollection;

    IEnumerator _coroutine;

    void Start()
    {
        _currentLevel = _initialLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        _playerSnowCollection = player.GetComponent<PlayerSnowCollection>();
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_playerSnowCollection.isFull == false)
            {
                _isCollecting = true;
                _playerMovement.isCollecting(true);
                _coroutine = PickUpCoroutine();
                StartCoroutine(_coroutine);

            }
        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (_playerSnowCollection.isFull == true)
    //        {
    //            if (_isCollecting == true)
    //            {
    //                _isCollecting = false;
    //                StopCoroutine(_coroutine);
    //            }
    //        }
    //    }
    //}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isCollecting = false;
            _playerMovement.isCollecting(false);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    IEnumerator PickUpCoroutine()
    {
        while (_isCollecting == true)
        {
            yield return new WaitForSeconds(_waitTime);

            _currentLevel -= _decrementalValue;
            _playerSnowCollection.IncrementSnow(_decrementalValue);

            if (_currentLevel < _initialLevel && _currentLevel > 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = _halfSprite;
            }
            else
            {
                Destroy(this.gameObject);
            }

        }

    }
}