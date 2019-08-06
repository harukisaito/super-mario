using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellTopController : MonoBehaviour {

	[SerializeField] GameObject koopaShellScript;
	[SerializeField] GameObject scorePoints;
	KoopaShellController koopaShellController;

	// Use this for initialization
	Rigidbody2D koopaShellParent;
	Collider2D colliderTop;
	SpriteRenderer spriteRendererParent;


	// Use this for initialization
	void Start () {
		koopaShellController = koopaShellScript.GetComponent<KoopaShellController>();
		koopaShellParent = GetComponentInParent<Rigidbody2D>();
		colliderTop = GetComponent<Collider2D>();
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
	}

	void Update()
	{
		if(GameController.instance.Transition || !koopaShellController.moving || !spriteRendererParent.enabled)
		{
			colliderTop.enabled = false;
		}
		else if(!GameController.instance.Transition || koopaShellController.moving)
		{
			colliderTop.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(koopaShellController.moving && !GameController.instance.Transition)
		{
			if(other.gameObject.tag == "Player")
			{
				GameController.instance.Score.currentScore += 100;
				Destroy(Instantiate(scorePoints, other.transform.position, Quaternion.identity, transform.parent), 1f);
				koopaShellParent.velocity = Vector2.zero; // stop the shell
				koopaShellController.acclerating = false; // stop updating the velocity in the main controller
				Invoke("NotMoving", 0.2f); // delay this so that you cant add force during this duration
			}
		}
	}

	void NotMoving()
	{
		koopaShellController.moving = false;
	}
}
