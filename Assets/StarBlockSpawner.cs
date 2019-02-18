using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlockSpawner : MonoBehaviour {

	[SerializeField] GameObject starBlockPrefab;

	void Start () {
		Instantiate(starBlockPrefab, this.transform.position, Quaternion.identity, this.transform);
	}
}
