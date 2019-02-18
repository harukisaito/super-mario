using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaTopController : MonoBehaviour {

	[SerializeField] GameObject goombaScript;
	[SerializeField] GameObject scorePoints;

	Animator animator;
	Rigidbody2D goomba;
	Collider2D colliderTop;
	SpriteRenderer spriteRendererParent;
	GoombaController goombaController;



	void Start () {
		animator = GetComponentInParent<Animator>();
		goomba = GetComponentInParent<Rigidbody2D>();
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();

		goombaController = goombaScript.GetComponent<GoombaController>();

		colliderTop = GetComponent<Collider2D>();
	}
	
	void Update () {
		if(!goombaController.hit)
		{
			if(GameController.instance.Transition || !spriteRendererParent.enabled || GameController.instance.PlayerState > 2)
			{
				colliderTop.enabled = false;
			}
			else
			{
				colliderTop.enabled = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.position.y > transform.position.y + 0.08)
		{
			if(other.gameObject.tag == "Player")
			{
				AudioManager.instance.Play("Stomp");
				goombaController.hit = true;
				colliderTop.enabled = false;
				goomba.constraints = RigidbodyConstraints2D.FreezeAll;
				GameController.instance.Score += 100;
				animator.SetTrigger("GoombaSquashed");
				Invoke("DisableSprite", 0.3f);
				Destroy(Instantiate(scorePoints, transform.position, Quaternion.identity, transform.parent), 1f);
				GameController.instance.Goombas.Remove(gameObject);
			}
		}
	}

	void DisableSprite()
	{
		spriteRendererParent.enabled = false;
	}
}
