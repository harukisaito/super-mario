using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {

	[SerializeField] Vector2 velocity;
	Rigidbody2D star;
	Collider2D colliderStar;
	SpriteRenderer spriteRenderer;

	bool facingRight = true;


	void Start()
	{
		star = GetComponent<Rigidbody2D>();
		colliderStar = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}


	void FixedUpdate () {

		if(star.velocity.y < velocity.y)
		{
			star.velocity = velocity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// Debug.Log(other.gameObject.tag);
		if(facingRight)
		{
			star.velocity = new Vector2(velocity.x * 1.25f, - velocity.y);
		}
		else
		{
			star.velocity = new Vector2(-velocity.x * 1.25f, - velocity.y);
		}

		if(other.gameObject.tag == "Player")
		{
			colliderStar.enabled = false;
			Physics2D.IgnoreCollision(colliderStar, other.collider, true);
			spriteRenderer.enabled = false;
			star.constraints = RigidbodyConstraints2D.FreezeAll;
			Destroy(gameObject, 1f);
		}
		if(other.gameObject.tag == "BrickBottom" || other.gameObject.tag == "CoinBottom")
		{
			Physics2D.IgnoreCollision(colliderStar, other.collider, true);
		}
		if(other.gameObject.tag == "Side")
		{
			facingRight = !facingRight;
			AddForce(facingRight, velocity);
		}
	}

	public void AddForce(bool dir, Vector2 speed)
	{
		// if dir == true add force to the right otehrwise left
		if(dir)
		{
			star.AddForce(speed);
		}
		else
		{
			star.AddForce(-speed);
		}
	}
	
}
