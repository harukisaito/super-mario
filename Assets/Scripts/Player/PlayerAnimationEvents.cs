using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
	Rigidbody2D player;
	Vector2 currentVelocity;

	void Start () {
		player = GetComponent<Rigidbody2D>();
	}


	public void FreezePositionOnConsumption()
	{
		currentVelocity = player.velocity;
		player.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	public void UnfreezePositionAfterConsupmtion()
	{
		player.constraints = RigidbodyConstraints2D.FreezeRotation;
		player.AddForce(currentVelocity);
		player.velocity = currentVelocity;
	}

	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}
}
