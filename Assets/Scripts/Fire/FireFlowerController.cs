using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerController : MonoBehaviour {

	[SerializeField] GameObject scorePoints;

	Collider2D colliderF;
	SpriteRenderer spriteRenderer;

	float timer;
	bool visited;
	// Use this for initialization
	void Start () {
		colliderF = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		colliderF.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= 0.5f && !visited)
		{
			colliderF.enabled = true;
			visited = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			colliderF.enabled = false;
			spriteRenderer.enabled = false;

			Destroy(Instantiate(scorePoints, transform.position, Quaternion.identity, transform), 1f);
			Destroy(gameObject, 1f);

		}
	}


}
