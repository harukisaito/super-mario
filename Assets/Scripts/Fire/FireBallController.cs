using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

	[SerializeField] Vector2 velocity;
	Rigidbody2D fireball;
	GameObject fireballExplosionPrefab;
	Animator animator;

	void Start () {
		fireball = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		fireball.velocity = velocity;

		Invoke("RemoveFromList", 3f);
		Destroy(gameObject, 3f);

		if(!GameController.instance.FacingRight)
		{
			velocity.x = -velocity.x;
		}
	}
	
	void FixedUpdate () {
		if(fireball.velocity.y < velocity.y)
		{
			fireball.velocity = velocity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		fireball.velocity = new Vector2(velocity.x * 1.25f, - velocity.y);

		if(other.gameObject.tag == "Side")
		{
			RemoveFromList();
			fireball.constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetTrigger("Explode");
			Destroy(gameObject, 0.2f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Detector")
		{
			RemoveFromList();
			fireball.constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetTrigger("Explode");
			Destroy(gameObject, 0.2f);
		}
	}

	void RemoveFromList()
	{
		GameController.instance.FireBalls.Remove(fireball);
	}
}
