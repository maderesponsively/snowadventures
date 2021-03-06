﻿using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    [HideInInspector] public Vector3 Position { get { return transform.position; } }

    private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
	}

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 11);
    }

    public void Push(Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
		//Debug.Log(collision.gameObject);

		//Destroy(this.gameObject);
	}
}