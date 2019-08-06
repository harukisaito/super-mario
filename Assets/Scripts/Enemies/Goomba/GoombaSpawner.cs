using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaSpawner : MonoBehaviour {

	[SerializeField] GameObject goombaPrefab;

	void Start () {
		GameController.instance.Goombas.Clear();

		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-13.4f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-10f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-8.5f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-8.9f, -0.8f), Quaternion.identity));

		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-4.15f, 0.47f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-3.95f, 0.47f), Quaternion.identity));

		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-1.4f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(-1.2f, -0.8f), Quaternion.identity));


		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(1f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(1.2f, -0.8f), Quaternion.identity));

		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(2.5f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(2.7f, -0.8f), Quaternion.identity));

		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(3.5f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(3.7f, -0.8f), Quaternion.identity));
		
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(10.5f, -0.8f), Quaternion.identity));
		GameController.instance.Goombas.Add(Instantiate(goombaPrefab, new Vector3(11.5f, -0.8f), Quaternion.identity));

		foreach(GameObject goomba in GameController.instance.Goombas)
		{
			goomba.SetActive(false);
		}
	}	

	void Update()
	{
		for(int i = 0; i < GameController.instance.Goombas.Count; i++)
		{
			if(GameController.instance.CurrentMario.transform.position.x > GameController.instance.Goombas[i].transform.position.x -3)
			{
				GameController.instance.Goombas[i].SetActive(true);
			}
		}
	}
}
