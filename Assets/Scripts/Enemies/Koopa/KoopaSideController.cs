using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaSideController : MonoBehaviour {

	Collider2D colliderSide;
	SpriteRenderer spriteRenderer;

	void Start () {
		colliderSide = GetComponent<Collider2D>();
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
		// colliderSide.enabled = true;
	}
	
	void Update () {
		if(GameController.instance.Transition || !spriteRenderer.enabled)
		{
			colliderSide.enabled = false;
		}
		else if(!GameController.instance.Transition)
		{
			colliderSide.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(GameController.instance.PlayerState < 3)
		{
			if(other.gameObject.tag == "Player")
			{
				GameController.instance.PlayerState --;
			}
		}
	}
}
