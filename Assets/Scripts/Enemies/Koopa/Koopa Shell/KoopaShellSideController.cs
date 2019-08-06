using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellSideController : MonoBehaviour {

	[SerializeField] GameObject koopaShellScript;

	KoopaShellController koopaShellController;

	Collider2D colliderSide;
	SpriteRenderer spriteRendererParent;




	void Start () {
		koopaShellController = koopaShellScript.GetComponent<KoopaShellController>();
		spriteRendererParent = GetComponentInParent<SpriteRenderer>();
		colliderSide = GetComponent<Collider2D>();
	}
	
	void Update () {
		if(GameController.instance.Transition || !koopaShellController.moving || !spriteRendererParent.enabled)
		{
			colliderSide.enabled = false;
		}
		else if(!GameController.instance.Transition || koopaShellController.moving)
		{
			colliderSide.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(GameController.instance.PlayerState < 3)
		{
			if(koopaShellController.moving)
			{
				if(other.gameObject.tag == "Player")
				{
					GameController.instance.Transition = true;
					if(GameController.instance.PlayerState > 0)
					{
						StartCoroutine(TransformMario(GameController.instance.PlayerState - 1));
						other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
					}
					GameController.instance.PlayerState --;
				}
			}
		}
	}

	IEnumerator TransformMario(int state)
	{
		yield return new WaitForSeconds(1f);
		MarioSpawner.Instance.TransformMario(state);

		GameController.instance.Transition = false;
	}
}
