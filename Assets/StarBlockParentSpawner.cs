using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlockParentSpawner : MonoBehaviour {

	[SerializeField] GameObject starBlockParentPrefab;


	void Start () {
		Instantiate(starBlockParentPrefab,new Vector3(-0.72f, -0.34f, 0), Quaternion.identity);
	}
}
