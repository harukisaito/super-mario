using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlagController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		Invoke("DisableAnimator", 1f);
	}

	void DisableAnimator()
	{
		animator.enabled = false;
	}
}
