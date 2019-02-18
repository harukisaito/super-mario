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
		shroomController.AddForce(shroomController.facingRight, shroomController.speed);
	}
}