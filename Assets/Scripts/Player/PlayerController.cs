using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float speed;
	[SerializeField] float jumpForce;
	[SerializeField] GameObject marioDie;



	Vector2 groundCheckSize = new Vector2(0.09f, 0f);
	Animator animator;
	Rigidbody2D player;
	Collider2D colliderP;
	SpriteRenderer spriteRenderer;
	bool grounded;
	bool duck;
	float moveX;
	bool visited;

	void Start () {
		player = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		colliderP = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		GameController.instance.FacingRight = true;
		spriteRenderer.flipX = !GameController.instance.FacingRight;
		player.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!GameController.instance.Paused)
		{
			grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);
			
			if(!GameController.instance.Dead)
			{
				if(!duck)
				{
					colliderP.sharedMaterial.friction = 0.0f;
					player.sharedMaterial.friction = 0.0f;
					float tempSpeed = speed;
					
					moveX = Input.GetAxis("Horizontal");
					animator.SetFloat("Speed", Math.Abs(moveX));
					animator.SetBool("Ground", grounded);
					if(Input.GetKey(KeyCode.LeftShift))
					{
						tempSpeed = speed;
						speed *= 1.3f;
					}
					if(moveX > 0 && !GameController.instance.FacingRight)
					{
						Flip();
					}
					else if(moveX < 0 && GameController.instance.FacingRight)
					{
						Flip();
					}
					player.velocity = new Vector2(moveX * speed, player.velocity.y);
					speed = tempSpeed;
				}
				else
				{
					colliderP.sharedMaterial.friction = 0.4f;
					player.sharedMaterial.friction = 0.4f;
				}
			}
		}
	}

	void Update()
	{
		if(!GameController.instance.Paused)
		{
			if((GameController.instance.PlayerState == -1 || GameController.instance.Timer < 0) && !visited)
			{
				AudioManager.instance.Stop(GameController.instance.Music);
				AudioManager.instance.Play("MarioDie");
				GameController.instance.Dead = true;
				player.constraints = RigidbodyConstraints2D.FreezeAll;
				colliderP.enabled = false;
				spriteRenderer.enabled = false;
				Instantiate(marioDie, transform.position, Quaternion.identity, transform);
				visited = true;
			}
			if(Input.GetKeyDown(KeyCode.Space) && grounded == true)
			{
				player.AddForce(new Vector2(0f, jumpForce));
				if(GameController.instance.PlayerState == 0)
				{
					AudioManager.instance.Play("JumpSmall");
				}
				else
				{
					AudioManager.instance.Play("JumpBig");
				}
			}
			
			// if(Input.GetKeyDown(KeyCode.DownArrow))
			// {
			// 	duck = true;
			// 	animator.SetBool("Duck", duck);
			// }
			// else if(Input.GetKeyUp(KeyCode.DownArrow))
			// {
			// 	duck = false;
			// 	animator.SetBool("Duck", duck);
			// }
			
			if(GameController.instance.PlayerState != 0)
			{
				duck = Input.GetKey(KeyCode.DownArrow);
				animator.SetBool("Duck", duck);
			}
		}
	}

	void Flip()
	{
		GameController.instance.FacingRight = !GameController.instance.FacingRight;
		spriteRenderer.flipX = !GameController.instance.FacingRight;
	}
}
