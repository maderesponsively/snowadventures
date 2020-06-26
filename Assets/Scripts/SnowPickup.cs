using System.Collections;
using UnityEngine;

public class SnowPickup : MonoBehaviour
{
    
    [SerializeField]
    private float _initialLevel = 1f;
    private float _currentLevel;
    [SerializeField]
    private float _collectionTime = 0.65f;
    [SerializeField]
    private float _decrementalValue = 0.5f;

    [SerializeField]
    private Sprite _halfSprite;

    private bool _collecting = false;
    private IEnumerator _coroutine;
    private PlayerMovement _playerMovement;
    private PlayerSnowCollection _playerSnowCollection;

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
                _collecting = true;
                _playerMovement.StartCollecting();
                _coroutine = PickUpCoroutine();
                StartCoroutine(_coroutine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _collecting = false;
            _playerMovement.EndCollecting();
            StopCoroutine(_coroutine);
        }
    }

    IEnumerator PickUpCoroutine()
    {

        while (_collecting == true)
        {
            yield return new WaitForSeconds(_collectionTime);

            if (_currentLevel > 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = _halfSprite;

                _currentLevel -= _decrementalValue;
                _playerSnowCollection.IncrementSnow(_decrementalValue);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}