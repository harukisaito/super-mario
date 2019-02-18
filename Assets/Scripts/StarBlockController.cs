using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlockController : MonoBehaviour {

	[SerializeField] GameObject starSpawnAnimation;
	[SerializeField] GameObject starPrefab;

	GameObject star;

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
			Debug.Log("HIT");
			animator.SetBool("Hit", true);
			if(!visited)
			{
				Instantiate(starSpawnAnimation, transform.parent.position, Quaternion.identity, transform.parent);
				Invoke("InstantiateStar", 0.5f);
				visited = true;
				AudioManager.instance.Play("PowerUpAppears");
			}
		}
	}

	void InstantiateStar()
	{
		star = Instantiate(starPrefab, new Vector2(0, 0.16f), Quaternion.identity, transform.parent);
		star.transform.localPosition = new Vector3(0, 0.17f);
	}
}
