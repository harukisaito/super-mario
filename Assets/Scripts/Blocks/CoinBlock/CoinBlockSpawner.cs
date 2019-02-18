using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockSpawner : MonoBehaviour {

	[SerializeField] GameObject coinBlockPrefab;
	// Use this for initialization
	void Start () {
		Instantiate(coinBlockPrefab, this.transform.position, Quaternion.identity, this.transform);
	}
}
