using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockSpawner : MonoBehaviour {
	[SerializeField] GameObject brickBlockPrefab;

	// Use this for initialization
	void Start () {
		Instantiate(brickBlockPrefab, this.transform.position, Quaternion.identity, this.transform);
	}
}
