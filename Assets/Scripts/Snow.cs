using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField]
    private float _waitTime = 1.4f;

    private bool _collectSnow = false;
    private IEnumerator coroutine;

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
            Player player = collision.transform.GetComponent<Player>();

            if (player != null)
            {
                if (_collectSnow == false)
                {
                    _collectSnow = true;

                    player.CollectSnow();

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
            yield return new WaitForSeconds(_waitTime);

            player.CollectedSnow();

            Destroy(this.gameObject);
        }
    }
}