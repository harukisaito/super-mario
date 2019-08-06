using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAnimationDisabler : MonoBehaviour
{
	MushroomController shroomController;

	void Start()
	{
		shroomController = GetComponent<MushroomController>();
	}

	public void DisableAnimator()
	{
		GetComponent<Animator>().enabled = false;
		if(gameObject.tag == "Mushroom")
		{
			shroomController.AddForce(shroomController.facingRight, shroomController.speed);
		}
	}
}