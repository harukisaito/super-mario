using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeAnimationController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		if(GameController.instance.Exit)
		{
			animator.SetTrigger("Exit");
			Invoke("ExitPipe", 1f);
		}
	}

	void ExitPipe()
	{
		animator.SetTrigger("Exiting");
	}
}
