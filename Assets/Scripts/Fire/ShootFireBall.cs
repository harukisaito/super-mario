using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour {

	[SerializeField] Rigidbody2D fireBallPrefab;
	[SerializeField] Vector2 velocity;

	Rigidbody2D fireBall;
	Animator animator;

	void Start () {
		fireBall = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if((GameController.instance.PlayerState == 2 || GameController.instance.PlayerState == 5) && GameController.instance.FireBalls.Count < 2)
		{
			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				AudioManager.instance.Play("FireBall");
				animator.SetTrigger("Throw");
				if(GameController.instance.FacingRight)
				{
					fireBall = Instantiate(fireBallPrefab, new Vector2(transform.position.x + 0.15f, transform.position.y), Quaternion.identity);
					GameController.instance.FireBalls.Add(fireBall);
					fireBall.AddForce(velocity);
				}
				else
				{
					fireBall = Instantiate(fireBallPrefab, new Vector2(transform.position.x - 0.2f, transform.position.y), Quaternion.identity);
					GameController.instance.FireBalls.Add(fireBall);
					fireBall.AddForce(new Vector2(-velocity.x, velocity.y));
				}
			}
		}
	}
}
