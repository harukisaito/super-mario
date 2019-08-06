using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {

	[SerializeField] GameObject score100;
	[SerializeField] GameObject score500;
	[SerializeField] GameObject score800;
	[SerializeField] GameObject score1000;
	[SerializeField] GameObject score2000;
	[SerializeField] GameObject score5000;

	
	Rigidbody2D flag;

	// Use this for initialization
	void Start () {
		flag = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(transform.position.y < -0.64f)
		{
			flag.velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			flag.velocity = new Vector2(0, -0.9f);
			if(other.transform.position.y <= 1 && other.transform.position.y > 0.6)
			{
				Destroy(Instantiate(score5000, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 5000;
			}
			if(other.transform.position.y <= 0.6 && other.transform.position.y > 0.1)
			{
				Destroy(Instantiate(score2000, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 2000;
			}
			if(other.transform.position.y <= 0.1 && other.transform.position.y > -0.1)
			{
				Destroy(Instantiate(score1000, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 1000;
			}
			if(other.transform.position.y <= -0.1 && other.transform.position.y > -0.3)
			{
				Destroy(Instantiate(score800, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 800;
			}
			if(other.transform.position.y <= -0.3 && other.transform.position.y > -0.5)
			{
				Destroy(Instantiate(score500, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 500;
			}
			if(other.transform.position.y <= -0.5)
			{
				Destroy(Instantiate(score100, transform.position, Quaternion.identity, other.transform), 1f);
				GameController.instance.Score.currentScore += 100;
			}
		}
	}
	
}
