using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlockSpawner : MonoBehaviour {

	[SerializeField] GameObject mysteryBlockPrefab;

	void Start () {
		Instantiate(mysteryBlockPrefab, this.transform.position, Quaternion.identity, this.transform);
	}
}
