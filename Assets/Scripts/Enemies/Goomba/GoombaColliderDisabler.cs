using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaColliderDisabler : MonoBehaviour {

	[SerializeField] GameObject goombaScript;

	Collider2D colliderDiagonal;

	SpriteRenderer spriteRendererParent;

	GoombaController goombaController;

	// Use this for initialization
	void Start () {
		colliderDiagonal = GetComponent<Collider2D>();

		spriteRendererParent = GetComponentInParent<SpriteRenderer>();

		goombaController = goombaScript.GetComponent<GoombaController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!spriteRendererParent.enabled || GameController.instance.Transition || goombaController.hit)
		{
			colliderDiagonal.enabled = false;
		}
		else
		{
			colliderDiagonal.enabled = true;
		}
	}
}
