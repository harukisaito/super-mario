using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellDetector : MonoBehaviour {

	[SerializeField] GameObject dieAnimation;
	[SerializeField] GameObject scorePoints;

	SpriteRenderer spriteRendererParent;
	Collider2D colliderDetector;
	Collider2D colliderParent;
	Rigidbody2D parent;

	// Use this for initialization
	void Start () {
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		colliderParent = GetComponentInParent<Collider2D>();
		parent = GetComponentInParent<Rigidbody2D>();

		colliderDetector = GetComponent<Collider2D>();
	}

	void Update()
	{
		// Debug.Log(transform.localPosition.y);
		if(spriteRendererParent.enabled)
		{
			colliderDetector.enabled = true;
		}
		else if(!spriteRendererParent.enabled)
		{
			colliderDetector.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "koopaShell" && gameObject.transform.parent.tag != "Koopa" || other.gameObject.tag == "FireBall" || GameController.instance.PlayerState > 2 && other.gameObject.tag == "Player")
		{
			Die();
		}
		if(other.gameObject.tag == "BrickBottom")
		{
			if(other.gameObject.transform.parent.root.position.y > 0.01)
			{
				Die();
			}
		}
	}

	public void Die()
	{
		AudioManager.instance.Play("Kick");
		// Disable parent
		spriteRendererParent.enabled = false;
		colliderParent.enabled = false;
		parent.constraints = RigidbodyConstraints2D.FreezeAll;

		// disable this collider
		colliderDetector.enabled = false;

		// play dying animation
		Destroy(Instantiate(dieAnimation, transform.parent.position, Quaternion.identity, transform.parent), 1f);
		Destroy(Instantiate(scorePoints, transform.position, Quaternion.identity, transform.parent), 1f);
	}
}
