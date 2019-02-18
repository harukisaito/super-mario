using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellAddForceController : MonoBehaviour {

	[SerializeField] GameObject koopaShellScript;
	[SerializeField] GameObject scorePoints;
	KoopaShellController koopaShellController;
	
	Collider2D colliderForce;
	SpriteRenderer spriteRendererParent;



	void Start () {
		koopaShellController = koopaShellScript.GetComponent<KoopaShellController>();
		colliderForce = GetComponent<Collider2D>();
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
	}

	void Update()
	{
		if(GameController.instance.Transition || koopaShellController.acclerating || !spriteRendererParent.enabled)
		{
			colliderForce.enabled = false;
		}
		else if(!GameController.instance.Transition || !koopaShellController.moving)
		{
			colliderForce.enabled = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(!koopaShellController.moving)
		{
			if(other.gameObject.tag == "Player")
			{
				AudioManager.instance.Play("Kick");
				if(other.transform.position.x < transform.position.x)
				{
					// Debug.Log("ADDFORCE RIGHT");
					koopaShellController.facingRight = true;
					koopaShellController.AddForce(koopaShellController.facingRight, koopaShellController.speed);
				}
				else if(other.transform.position.x > transform.position.x)
				{
					// Debug.Log("ADDFORCE LEFT");
					koopaShellController.facingRight = false;
					koopaShellController.AddForce(koopaShellController.facingRight, koopaShellController.speed);
				}
				koopaShellController.acclerating = true; // activate the updating of the velocity
				Destroy(Instantiate(scorePoints, other.transform.position, Quaternion.identity, transform.parent), 1f);
				Invoke("Moving", 0.05f); 
			}
		}
	}

	void Moving()
	{
		koopaShellController.moving = true;
	}
}
