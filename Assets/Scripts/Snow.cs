using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField]
    private float _waitTime = 1.3f;
    private bool _collectSnow = false;
    private float _snowWeight = 1;
    private float _snowLevel = 1;

    private IEnumerator coroutine;

    public Sprite depleteSnowPile;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                if (_collectSnow == false)
                {
                    _collectSnow = true;

                    player.StartCollectingSnow();

                    coroutine = SnowCollectionCoroutine(player);

                    StartCoroutine(coroutine);
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();

            if (player != null)
            {
                _collectSnow = false;

                player.StopCollectingSnow();
                StopCoroutine(coroutine);
            }
        }
    }

    IEnumerator SnowCollectionCoroutine(Player player)
    {
        while(_collectSnow == true)
        {
            if (_snowLevel != _snowWeight / 2)
            {
                yield return new WaitForSeconds(_waitTime / 2);
                _snowLevel = _snowWeight / 2;
                player.CollectedSnow();

                this.GetComponent<SpriteRenderer>().sprite = depleteSnowPile;
            }

            yield return new WaitForSeconds(_waitTime / 2);

            player.CollectedSnow();
            player.StopCollectingSnow();

            Destroy(this.gameObject);
        }
    }
}