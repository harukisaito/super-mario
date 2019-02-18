using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	[SerializeField] GameObject coinPrefab;
	
	int coinAmount = 7;

	float offsetX = 0.16f;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < coinAmount; i++)
		{
			Vector2 spawnPos = new Vector2(-16.23f + i * offsetX, -2.62f);
			Instantiate(coinPrefab, spawnPos, Quaternion.identity);
		}
		for(int i = 0; i < coinAmount; i++)
		{
			Vector2 spawnPos = new Vector2(-16.23f + i * offsetX, -2.3f);
			Instantiate(coinPrefab, spawnPos, Quaternion.identity);
		}
		for(int i = 0; i < coinAmount - 2; i++)
		{
			Vector2 spawnPos = new Vector2(-16.07f + i * offsetX, -1.98f);
			Instantiate(coinPrefab, spawnPos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
