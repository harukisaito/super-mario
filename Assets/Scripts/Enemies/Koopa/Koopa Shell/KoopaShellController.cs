using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellController : MonoBehaviour {

	[SerializeField] Vector2 koopaShellVelocity;
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float koopaShellSpeed;
	

	[HideInInspector]
	public Vector2 speed;
	public bool facingRight = true;


	Rigidbody2D koopaShell;
	Vector2 groundCheckSize = new Vector2(1.0f, 1.0f);


	bool grounded;
	public bool moving;
	public bool acclerating;


	void Start () {
		koopaShell = GetComponent<Rigidbody2D>();
		speed = new Vector2(koopaShellSpeed, 0);
		koopaShell.velocity = Vector2.zero;
	}

	void Update()
	{
		if(moving)
		{
			GameController.instance.KoopaTimer = 0;
		}
		if(!moving)
		{
			GameController.instance.KoopaTimer += Time.deltaTime;
		}
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);

		if(acclerating)
		{
			if(grounded)
			{
				if(facingRight)
				{
					koopaShell.velocity = koopaShellVelocity;
				}
				if(!facingRight)
				{
					koopaShell.velocity = -koopaShellVelocity;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(moving)
		{
			if(other.gameObject.layer != LayerMask.NameToLayer("Block") && other.gameObject.tag != "Player" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Mushroom" && other.gameObject.tag != "MainCamera" && other.gameObject.tag != "Goomba")
			{
				facingRight = !facingRight;
				AddForce(facingRight, speed);
			}
		}
	}

	public void AddForce(bool dir, Vector2 speed)
	{
		// if dir == true add force to the right otehrwise left
		if(dir)
		{
			koopaShell.AddForce(speed);
		}
		else if(!dir)
		{
			koopaShell.AddForce(-speed);
		}
	}
}
