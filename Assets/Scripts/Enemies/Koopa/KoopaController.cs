using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaController : MonoBehaviour {

	[SerializeField] Vector2 koopaVelocity;
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float koopaSpeed;
	
	

	[HideInInspector] public Vector2 speed;
	public bool facingRight = false;


	Rigidbody2D koopa;
	Vector2 groundCheckSize = new Vector2(0.1f, 0.0f);
	Collider2D colliderK;
	SpriteRenderer spriteRenderer;
	bool grounded;

	void Start () {
		koopa = GetComponent<Rigidbody2D>();
		colliderK = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		speed = new Vector2(koopaSpeed, 0);
		koopa.AddForce(speed);
	}

	void Update()
	{
		if(!spriteRenderer.enabled)
		{
			colliderK.enabled = false;
		}
		else if(spriteRenderer.enabled)
		{
			colliderK.enabled = true;
		}
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);

		if(grounded && facingRight)
		{
			koopa.velocity = koopaVelocity;
		}
		if(grounded && !facingRight)
		{
			koopa.velocity = -koopaVelocity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer != LayerMask.NameToLayer("Block") && other.gameObject.tag != "Ground" && other.gameObject.tag != "MainCamera" && other.gameObject.tag != "Player")
		{
			facingRight = !facingRight;
			AddForce(facingRight, speed);
		}
	}

	public void AddForce(bool dir, Vector2 speed)
	{
		// if dir == true add force to the right otehrwise left
		if(dir)
		{
			koopa.AddForce(speed);
		}
		else
		{
			koopa.AddForce(-speed);
		}
	}

	// void Disable()
	// {
	// 	spriteRenderer.enabled = false;
	// 	colliderK.enabled = false;
	// }
}
