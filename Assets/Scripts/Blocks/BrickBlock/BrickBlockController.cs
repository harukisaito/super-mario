using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockController : MonoBehaviour {

	Animator animator;
	Collider2D colliderB;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		animator = GetComponentInParent<Animator>();
		colliderB = GetComponent<Collider2D>();
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
	}

	void Update()
	{
		// Debug.Log(transform.position);
		// Debug.Log(transform.localPosition);
		// Debug.Log(transform.root.position);
		// Debug.Log(transform.root.localPosition);

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.transform.position.y <= transform.parent.position.y - 0.08f)
		{
			if(GameController.instance.PlayerState >= 1 && GameController.instance.PlayerState != 3)
			{
				colliderB.enabled = false;
				spriteRenderer.enabled = false;
				Destroy(transform.parent.gameObject, 1.0f);
			}
			else
			{
				AudioManager.instance.Play("Bump");
				animator.SetTrigger("HitAndSmall");
			}
		}
	}
}
