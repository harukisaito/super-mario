using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaTopController : MonoBehaviour {

	[SerializeField] GameObject koopaShellPrefab;
	[SerializeField] GameObject scorePoints;
	Collider2D colliderTop;
	GameObject koopaShell;


	SpriteRenderer spriteRenderer;
	Rigidbody2D koopa;

	void Start () {
		colliderTop = GetComponent<Collider2D>();

		spriteRenderer = GetComponentInParent<SpriteRenderer>();
		koopa = GetComponentInParent<Rigidbody2D>();
	}
	
	void Update () {
		if(GameController.instance.KoopaTimer >= 3) // reinstatiate koopa
		{
			// enable parent 
			colliderTop.enabled = true;
			spriteRenderer.enabled = true;
			koopa.constraints = RigidbodyConstraints2D.FreezeRotation;

			GameController.instance.KoopaTimer = 0;
			//destroy shell after 10 sec of not moving 
			Destroy(koopaShell);
		}
		if(GameController.instance.Transition || !spriteRenderer.enabled || GameController.instance.PlayerState > 2) // disable top collider while player is transitioning or the parent collider is off
		{
			colliderTop.enabled = false;
		}
		else if(!GameController.instance.Transition) // enable collider again after transition is over
		{
			colliderTop.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			AudioManager.instance.Play("Stomp");
			// Disable Parent and this collider
			colliderTop.enabled = false;
			spriteRenderer.enabled = false;
			koopa.constraints = RigidbodyConstraints2D.FreezeAll;
			GameController.instance.Score += 100;

			// Instantiate shell 
			koopaShell = Instantiate(koopaShellPrefab, transform.position, Quaternion.identity, transform.parent);
			Destroy(Instantiate(scorePoints, transform.position, Quaternion.identity, transform.parent), 1);
		}
	}
}
