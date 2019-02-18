using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUpBlockColliderController : MonoBehaviour {


	SpriteRenderer spriteRendererParent;
	Collider2D colliderEdge;

	void Start () {
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		colliderEdge = GetComponent<Collider2D>();
		colliderEdge.enabled = false;
	}
	
	void Update () {
		if(spriteRendererParent.enabled)
		{
			colliderEdge.enabled = true;
		}
	}
}
