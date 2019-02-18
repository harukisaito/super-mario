using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockColliderDisabler : MonoBehaviour {

	SpriteRenderer spriteRendererParent;
	Collider2D colliderEdge;

	void Start () {
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		colliderEdge = GetComponent<Collider2D>();
	}
	
	void Update () {
		if(!spriteRendererParent.enabled)
		{
			colliderEdge.enabled = false;
		}
	}
}
