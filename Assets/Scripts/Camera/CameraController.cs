using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 undergroundSpawnPos =  new Vector3(-16.5f, -1.5f);
	Vector3 pipeExitSpawnPos = new Vector3(9.28f, -0.43f);
	Vector3 undergroundCameraPos = new Vector3(-15.81f, -2.4f, -10);

	bool underground;
	
	void LateUpdate () {

		if(GameController.instance.CurrentMario.transform.position.x >= transform.position.x && !underground)
		{
			transform.position = new Vector3(GameController.instance.CurrentMario.transform.position.x, 0, -10);
		}
		if(GameController.instance.CurrentMario.transform.position == pipeExitSpawnPos)
		{
			transform.position = new Vector3(GameController.instance.CurrentMario.transform.position.x, 0, -10);
			underground = false;
		}
		if(GameController.instance.CurrentMario.transform.position == undergroundSpawnPos)
		{
			transform.position = undergroundCameraPos;
			underground = true;
		}
		if(GameController.instance.CurrentMario.transform.position.x >= transform.position.x && GameController.instance.CurrentMario.transform.position.x <= -15.6 && underground)
		{
			transform.position = new Vector3(GameController.instance.CurrentMario.transform.position.x, -2.4f, -10);
		}
		if(GameController.instance.CurrentMario.transform.position.x >= 14)
		{
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(15, transform.position.y, -10), 0.05f);
			transform.position = smoothedPosition;
		}
	}
}
