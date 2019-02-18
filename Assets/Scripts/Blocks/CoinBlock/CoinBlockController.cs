using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockController : MonoBehaviour {

	[SerializeField] GameObject coinPrefab;
	bool visited;

	Animator animator;

	void Start()
	{
		animator = GetComponentInParent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player" && other.transform.position.y < transform.parent.position.y - 0.08f)
		{
			if(!visited)
			{
				animator.SetBool("Hit", true);
				Destroy(Instantiate(coinPrefab, new Vector3(0, 0.16f, 0), Quaternion.identity, this.transform.parent), 0.5f);
				visited = true;
				AudioManager.instance.Play("Coin");
			}
			else
			{
				AudioManager.instance.Play("Bump");
			}
		}
	}
}
