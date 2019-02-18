using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockParentSpawner : MonoBehaviour {

	[SerializeField] GameObject brickBlockParentPrefab;

	GameObject[] brickBlocks = new GameObject[31];
	// Use this for initialization
	void Start () {
		brickBlocks[0] = Instantiate(brickBlockParentPrefab, new Vector3(-13.68f, -0.34f, 0), Quaternion.identity);
		brickBlocks[1] = Instantiate(brickBlockParentPrefab, new Vector3(-13.36f, -0.34f, 0), Quaternion.identity);
		brickBlocks[2] = Instantiate(brickBlockParentPrefab, new Vector3(-13.04f, -0.34f, 0), Quaternion.identity);
		brickBlocks[3] = Instantiate(brickBlockParentPrefab, new Vector3(-4.56f, -0.34f, 0), Quaternion.identity);
		brickBlocks[4] = Instantiate(brickBlockParentPrefab, new Vector3(-4.24f, -0.34f, 0), Quaternion.identity);
		brickBlocks[5] = Instantiate(brickBlockParentPrefab, new Vector3(-1.84f, -0.34f, 0), Quaternion.identity);
		brickBlocks[6] = Instantiate(brickBlockParentPrefab, new Vector3(-0.88f, -0.34f, 0), Quaternion.identity);
		// brickBlocks[7] = Instantiate(brickBlockParentPrefab, new Vector3(-0.72f, -0.34f, 0), Quaternion.identity);
		brickBlocks[8] = Instantiate(brickBlockParentPrefab, new Vector3(2f, -0.34f, 0), Quaternion.identity);
		brickBlocks[9] = Instantiate(brickBlockParentPrefab, new Vector3(3.76f, -0.34f, 0), Quaternion.identity);
		brickBlocks[10] = Instantiate(brickBlockParentPrefab, new Vector3(3.92f, -0.34f, 0), Quaternion.identity);
		brickBlocks[11] = Instantiate(brickBlockParentPrefab, new Vector3(10f, -0.34f, 0), Quaternion.identity);
		brickBlocks[13] = Instantiate(brickBlockParentPrefab, new Vector3(10.16f, -0.34f, 0), Quaternion.identity);
		brickBlocks[14] = Instantiate(brickBlockParentPrefab, new Vector3(10.48f, -0.34f, 0), Quaternion.identity);
		brickBlocks[15] = Instantiate(brickBlockParentPrefab, new Vector3(-4.08f, 0.3f, 0), Quaternion.identity);
		brickBlocks[16] = Instantiate(brickBlockParentPrefab, new Vector3(-3.92f, 0.3f, 0), Quaternion.identity);
		brickBlocks[17] = Instantiate(brickBlockParentPrefab, new Vector3(-3.76f, 0.3f, 0), Quaternion.identity);
		brickBlocks[18] = Instantiate(brickBlockParentPrefab, new Vector3(-3.6f, 0.3f, 0), Quaternion.identity);
		brickBlocks[19] = Instantiate(brickBlockParentPrefab, new Vector3(-3.44f, 0.3f, 0), Quaternion.identity);
		brickBlocks[20] = Instantiate(brickBlockParentPrefab, new Vector3(-3.28f, 0.3f, 0), Quaternion.identity);
		brickBlocks[21] = Instantiate(brickBlockParentPrefab, new Vector3(-3.12f, 0.3f, 0), Quaternion.identity);
		brickBlocks[22] = Instantiate(brickBlockParentPrefab, new Vector3(-2.96f, 0.3f, 0), Quaternion.identity);
		brickBlocks[23] = Instantiate(brickBlockParentPrefab, new Vector3(-2.32f, 0.3f, 0), Quaternion.identity);
		brickBlocks[24] = Instantiate(brickBlockParentPrefab, new Vector3(-2.16f, 0.3f, 0), Quaternion.identity);
		brickBlocks[25] = Instantiate(brickBlockParentPrefab, new Vector3(-2f, 0.3f, 0), Quaternion.identity);
		brickBlocks[26] = Instantiate(brickBlockParentPrefab, new Vector3(2.48f, 0.3f, 0), Quaternion.identity);
		brickBlocks[27] = Instantiate(brickBlockParentPrefab, new Vector3(2.64f, 0.3f, 0), Quaternion.identity);
		brickBlocks[28] = Instantiate(brickBlockParentPrefab, new Vector3(2.8f, 0.3f, 0), Quaternion.identity);
		brickBlocks[29] = Instantiate(brickBlockParentPrefab, new Vector3(3.6f, 0.3f, 0), Quaternion.identity);
		brickBlocks[30] = Instantiate(brickBlockParentPrefab, new Vector3(4.08f, 0.3f, 0), Quaternion.identity);
	}
}
