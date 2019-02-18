using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlockController : MonoBehaviour {

	[SerializeField] GameObject fireFlowerPrefab;	
	[SerializeField] GameObject mushroomPrefab;
	Animator animator;
	bool visited;
	// Use this for initialization
	void Start () {
		animator = GetComponentInParent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.position.y < transform.parent.position.y -0.08f)
		{
			animator.SetBool("Hit", true);
			if(GameController.instance.PlayerState == 0 || GameController.instance.PlayerState == 3)
			{
				if(!visited)
				{
					Instantiate(mushroomPrefab, this.transform.parent.position, Quaternion.identity, this.transform.parent);
					visited = true;
					AudioManager.instance.Play("PowerUpAppears");
				}
				else
				{
					AudioManager.instance.Play("Bump");
				}
			}
			if(GameController.instance.PlayerState >= 1 && GameController.instance.PlayerState != 3)
			{
				if(!visited)
				{
					Instantiate(fireFlowerPrefab, this.transform.parent.position, Quaternion.identity, this.transform.parent);
					visited = true;
					AudioManager.instance.Play("PowerUpAppears");
				}
				else
				{
					AudioManager.instance.Play("Bump");
				}
			}
		}
	}
}
