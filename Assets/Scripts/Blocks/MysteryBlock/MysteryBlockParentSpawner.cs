using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlockParentSpawner : MonoBehaviour {

	[SerializeField] GameObject mysteryBlockParentPrefab;

	private GameObject[] mysteryBlocks = new GameObject[3];

	void Start () {
		mysteryBlocks[0] = Instantiate(mysteryBlockParentPrefab, new Vector3(-13.52f, -0.34f, 0), Quaternion.identity);
		mysteryBlocks[1] = Instantiate(mysteryBlockParentPrefab, new Vector3(-4.4f, -0.34f, 0), Quaternion.identity);
		mysteryBlocks[2] = Instantiate(mysteryBlockParentPrefab, new Vector3(0.56f, 0.3f, 0), Quaternion.identity);
	}
}
