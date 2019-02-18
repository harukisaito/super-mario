using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

	[SerializeField] Vector2 goombaVelocity;
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float goombaSpeed;
	

	[HideInInspector] public Vector2 speed;
	public bool facingRight = false;

	public bool hit;


	Rigidbody2D goomba;
	Vector2 groundCheckSize = new Vector2(0.16f, 0.2f);
	bool grounded;

	void Start () {
		goomba = GetComponent<Rigidbody2D>();
		speed = new Vector2(goombaSpeed, 0);
		goomba.AddForce(speed);
	}

	void FixedUpdate () {
		
		grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);

		if(grounded && facingRight)
		{
			goomba.velocity = goombaVelocity;
		}
		if(grounded && !facingRight)
		{
			goomba.velocity = -goombaVelocity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer != LayerMask.NameToLayer("Block") && other.gameObject.tag != "Player" && other.gameObject.tag != "Ground" && other.gameObject.tag != "koopaShell" && other.gameObject.tag != "FireBall")
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
			goomba.AddForce(speed);
		}
		else
		{
			goomba.AddForce(-speed);
		}
	}
}
