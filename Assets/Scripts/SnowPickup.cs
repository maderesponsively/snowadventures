using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SnowPile {
 
    public Sprite fullPile;
    public Sprite halfPile;
 
}

public class SnowPickup : MonoBehaviour
{
    private float _snowLevel = 1f;
    [SerializeField] private float _waitTime = 0.65f;
    [SerializeField] private float _decrementalValue = 0.5f;
    private bool _isCollecting = false;

    [SerializeField] private Sprite _halfSprite;
    private PlayerMovement _playerMovement;
    private PlayerSnowCollection _playerSnowCollection;



    private SpriteRenderer snowRender;
    [SerializeField] private SnowPile[] _snowPile;
    private int _index;

    IEnumerator _coroutine;

    void Awake() {
        _index = Random.Range(0, _snowPile.Length);
        snowRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        _playerSnowCollection = player.GetComponent<PlayerSnowCollection>();
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (_snowLevel == 1) {
            snowRender.sprite = _snowPile[_index].fullPile;
        }
        if (_snowLevel == 0.5) {
            snowRender.sprite = _snowPile[_index].halfPile;
        }
        if (_snowLevel == 0) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_playerSnowCollection.isFull == false)
            {
                _isCollecting = true;

                // Replace with accessing animator directly from play animation
                _playerMovement.isCollecting(true);
                _coroutine = PickUpCoroutine();
                StartCoroutine(_coroutine);

            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isCollecting = false;

            // Replace with accessing animator directly from play animation
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

            _snowLevel -= _decrementalValue;
            _playerSnowCollection.IncrementSnow(_decrementalValue);
        }
    }

    public void SetSnowLevel(float value)
    {
        _snowLevel = value;
    }
}