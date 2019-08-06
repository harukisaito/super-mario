using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaSideController : MonoBehaviour {

	[SerializeField] GameObject goombaScript;

	SpriteRenderer spriteRendererParent;
	Collider2D colliderSide;
	GoombaController goombaController;

	// Use this for initialization
	void Start () {
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		colliderSide = GetComponent<Collider2D>();

		goombaController = goombaScript.GetComponent<GoombaController>();

		colliderSide.enabled = true;
	}
	
	void Update () {
		if(GameController.instance.Transition || goombaController.hit || !spriteRendererParent.enabled)
		{
			colliderSide.enabled = false;
		}
		else
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
				GameController.instance.Transition = true;
				Invoke("Damage", 0.01f);
			}
		}
	}

	void Damage()
	{
		GameController.instance.PlayerState --;
	}
}
