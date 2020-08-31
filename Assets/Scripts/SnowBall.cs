using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    //[HideInInspector] public Vector3 position { get { return transform.position; } }

	
	public GameObject snowPile;
	public GameObject player;
	private float _snowValue;

	private GameObject _snowPile;

    void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
	}

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 11);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.tag == "Ground") {
			_snowPile = Instantiate(snowPile, collision.contacts[0].point, Quaternion.identity);
			
			SnowPickup snowPickupScript = _snowPile.GetComponent<SnowPickup>();

			if(snowPickupScript != null) {
				snowPickupScript.SetSnowLevel(_snowValue);
			}
		}
		Destroy(this.gameObject);
	}

	public void Push(Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void SetValue(float value)
	{
		_snowValue = value;
	}

	public void ActivateRb()
	{
		rb.isKinematic = false;
	}

	public void DeactivateRb()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}
}