using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioSpawner : MonoBehaviour {

	[SerializeField] GameObject[] marioPrefabs;
	[SerializeField] GameObject[] marioPipeAnimations;
	[SerializeField] GameObject[] marioCastleAnimations;
	GameObject [] marios;

	[HideInInspector]
	public GameObject currentMario;
	public bool InStarMode {get; set;}
	public float StarTimer {get; set;}


	public static MarioSpawner Instance;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		marios = new GameObject[marioPrefabs.Length];
		for(int i = 0; i < marioPrefabs.Length; i++)
		{
			marios[i] = Instantiate(marioPrefabs[i], transform.position, Quaternion.identity, this.transform);
			marios[i].SetActive(false);
		}
	}

	void Start()
	{
		SetCurrentMario();
	}

	void SetCurrentMario()
	{
		int playerState = GetCurrentState();
		if(playerState < 5)
		{
			currentMario = marios[playerState];
		}
		else
		{
			currentMario = marios[playerState -1];
		}
		currentMario.SetActive(true);
		GameController.instance.CurrentMario = currentMario;
	}

	int GetCurrentState()
	{
		return GameController.instance.PlayerState;
	}

	public void TransformMario(int state)
	{
		Vector3 tempPos = GameController.instance.CurrentMario.transform.localPosition;
		GameController.instance.CurrentMario.SetActive(false);
		GameController.instance.PlayerState = state;
		SetCurrentMario();
		GameController.instance.CurrentMario.transform.localPosition = tempPos;
	}

	public void PlayAnimation(string type)
	{
		if(type == "pipe")
		{
			Instantiate(marioPipeAnimations[GetCurrentState()], currentMario.transform.localPosition, Quaternion.identity, currentMario.transform);
		}
		else if(type == "castle")
		{
			Instantiate(marioCastleAnimations[GetCurrentState()], currentMario.transform.localPosition, Quaternion.identity, currentMario.transform);
		}
	}
}
