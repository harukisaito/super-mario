using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockParentSpawner : MonoBehaviour {

	[SerializeField] GameObject coinBlockParentPrefab;

	GameObject[] coinBlocks = new GameObject[13];

	// Use this for initialization
	void Start () {
		coinBlocks[0] = Instantiate(coinBlockParentPrefab, new Vector3(-14.32f, -0.34f, 0), Quaternion.identity);
		// coinBlocks[1] = Instantiate(coinBlockParentPrefab, new Vector3(-13.52f, -0.34f, 0), Quaternion.identity);
		coinBlocks[2] = Instantiate(coinBlockParentPrefab, new Vector3(-13.2f, -0.34f, 0), Quaternion.identity);
		// coinBlocks[3] = Instantiate(coinBlockParentPrefab, new Vector3(-4.4f, -0.34f, 0), Quaternion.identity);
		coinBlocks[4] = Instantiate(coinBlockParentPrefab, new Vector3(0.08f, -0.34f, 0), Quaternion.identity);
		coinBlocks[5] = Instantiate(coinBlockParentPrefab, new Vector3(0.56f, -0.34f, 0), Quaternion.identity);
		coinBlocks[6] = Instantiate(coinBlockParentPrefab, new Vector3(1.04f, -0.34f, 0), Quaternion.identity);
		coinBlocks[7] = Instantiate(coinBlockParentPrefab, new Vector3(10.32f, -0.34f, 0), Quaternion.identity);
		coinBlocks[8] = Instantiate(coinBlockParentPrefab, new Vector3(-13.36f, 0.3f, 0), Quaternion.identity);
		coinBlocks[9] = Instantiate(coinBlockParentPrefab, new Vector3(-1.84f, 0.3f, 0), Quaternion.identity);
		// coinBlocks[10] = Instantiate(coinBlockParentPrefab, new Vector3(0.56f, 0.3f, 0), Quaternion.identity);
		coinBlocks[11] = Instantiate(coinBlockParentPrefab, new Vector3(3.76f, 0.3f, 0), Quaternion.identity);
		coinBlocks[12] = Instantiate(coinBlockParentPrefab, new Vector3(3.92f, 0.3f, 0), Quaternion.identity);

	}
	
}
