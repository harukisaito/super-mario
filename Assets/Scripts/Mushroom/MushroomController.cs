using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour {
	[SerializeField] Vector2 mushroomVelocity;
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float mushroomSpeed;
	[SerializeField] GameObject scorePoints;

	

	[HideInInspector] public Vector2 speed;
	public bool facingRight = true;


	Rigidbody2D mushroom;
	Vector2 groundCheckSize = new Vector2(0.16f, 0.2f);
	Collider2D colliderM;
	SpriteRenderer spriteRenderer;
	float timer = 0;
	bool grounded;
	bool taken;

	void Start () {
		mushroom = GetComponent<Rigidbody2D>();
		speed = new Vector2(mushroomSpeed, 0);
		colliderM = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		colliderM.enabled = false;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if(timer > 0.5 && !taken)
		{
			colliderM.enabled = true;
		}
	}
	
	void FixedUpdate () {
		
		grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);

		if(grounded && facingRight)
		{
			mushroom.velocity = mushroomVelocity;
		}
		if(grounded && !facingRight)
		{
			mushroom.velocity = -mushroomVelocity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			taken = true;
			colliderM.enabled = false;
			spriteRenderer.enabled = false;
			mushroom.constraints = RigidbodyConstraints2D.FreezeAll;

			Destroy(Instantiate(scorePoints, transform.position, Quaternion.identity, transform), 1f);
			Destroy(gameObject, 1f);
		}
		if(other.gameObject.layer != LayerMask.NameToLayer("Block") && other.gameObject.tag != "Player" && other.gameObject.tag != "Ground")
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
			mushroom.AddForce(speed);
		}
		else
		{
			mushroom.AddForce(-speed);
		}
	}
}
