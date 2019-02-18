using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneupBlockController : MonoBehaviour {

	[SerializeField] GameObject oneUpMushroom;

	SpriteRenderer spriteRendererParent;
	bool visited;
	// Use this for initialization
	void Start () {
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		spriteRendererParent.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.position.y < transform.parent.position.y -0.08f)
		{
			if(!visited)
			{
				Debug.Log("HELLO");
				visited = true;
				spriteRendererParent.enabled = true;
				Instantiate(oneUpMushroom, this.transform.parent.position, Quaternion.identity, this.transform.parent);
				AudioManager.instance.Play("PowerUpAppears");
			}
		}
	}
}
