using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaSpawner : MonoBehaviour {

	[SerializeField] GameObject koopaPrefab;
	GameObject koopa;

	// Use this for initialization
	void Start () {
		koopa = Instantiate(koopaPrefab, new Vector3(0f, -0.8f), Quaternion.identity);

		koopa.SetActive(false);
	}

	void Update()
	{
		if(GameController.instance.CurrentMario.transform.position.x > koopa.transform.position.x -3)
		{
			koopa.SetActive(true);
		}
	}
}
