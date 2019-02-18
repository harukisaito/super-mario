using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour {

	[SerializeField] GameObject debrisUL;
	[SerializeField] GameObject debrisUR;
	[SerializeField] GameObject debrisLL;
	[SerializeField] GameObject debrisLR;

	[SerializeField] GameObject smallMario;
	[SerializeField] GameObject bigMario;
	[SerializeField] GameObject fireMario;
	[SerializeField] GameObject smallStarMario;
	[SerializeField] GameObject bigStarMario;

	GameObject[] marios = new GameObject[6];
	GameObject[] marioAnimations = new GameObject[6];
	GameObject[] marioWalkAnimations = new GameObject[6];

	[SerializeField] GameObject smallMarioAnimation;
	[SerializeField] GameObject bigMarioAnimation;
	[SerializeField] GameObject fireMarioAnimation;
	[SerializeField] GameObject smallMarioWalkAnimation;
	[SerializeField] GameObject bigMarioWalkAnimation;
	[SerializeField] GameObject fireMarioWalkAnimation;


	GameObject currentMarioAnimation;
	GameObject currentMarioWalkAnimation;

	SpriteRenderer spriteRenderer;
	Rigidbody2D player;
	Animator animator;

	bool visited;
	bool visitedExit;



	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		player = GetComponent<Rigidbody2D>();
		animator = GetComponentInParent<Animator>();

		marios[0] = smallMario;
		marios[1] = bigMario;
		marios[2] = fireMario;
		marios[3] = smallStarMario;
		marios[4] = bigStarMario;
		marios[5] = bigStarMario;

		marioAnimations[0] = smallMarioAnimation;
		marioAnimations[1] = bigMarioAnimation;
		marioAnimations[2] = fireMarioAnimation;

		marioWalkAnimations[0] = smallMarioWalkAnimation;
		marioWalkAnimations[1] = bigMarioWalkAnimation;
		marioWalkAnimations[2] = fireMarioWalkAnimation;

		SetCurrentMario();
	}

	void Update()
	{
		Debug.Log(GameController.instance.PlayerState);
		if(!spriteRenderer.enabled)
		{
			player.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		if(GameController.instance.Finish)
		{
			player.velocity = new Vector2(0, -0.9f);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// Debug.Log("COLLISION = " + other.gameObject.tag);
		if(!GameController.instance.Dead)
		{
		// Debug.Log(GameController.instance.PlayerState);
		// Debug.Log("COLLISION " + other.gameObject.tag);
			if(transform.position.y <= other.transform.position.y -0.08)
			{
				if(other.gameObject.tag == "BrickBottom")
				{
					if(GameController.instance.PlayerState >= 1 && GameController.instance.PlayerState != 3)
					{
						Destroy(Instantiate(debrisUL, other.transform.parent.position + new Vector3(-0.04f, 0.04f, 0), Quaternion.identity, other.transform), 1f);
						Destroy(Instantiate(debrisUR, other.transform.parent.position + new Vector3(0.04f, 0.04f, 0), Quaternion.identity, other.transform), 1f);
						Destroy(Instantiate(debrisLL, other.transform.parent.position + new Vector3(-0.04f, -0.04f, 0), Quaternion.identity, other.transform), 1f);
						Destroy(Instantiate(debrisLR, other.transform.parent.position + new Vector3(0.04f, -0.04f, 0), Quaternion.identity, other.transform), 1f);
						AudioManager.instance.Play("BreakBlock");
					}
				}
				if(other.gameObject.tag == "CoinBottom")
				{
					GameController.instance.Coins ++;
				}
			}
			if(other.gameObject.tag == "Mushroom")
			{
				animator.SetTrigger("Grow");
				GameController.instance.Score += 1000;
				if(GameController.instance.PlayerState == 3)
				{
					Invoke("SmallStarToBigStar", 1f);
					GameController.instance.TempState ++;
				}
				else
				{
					Invoke("SmallToBig", 1f);
				}
				AudioManager.instance.Play("PowerUp");
			}
			if(other.gameObject.tag == "OneUpMushroom")
			{
				GameController.instance.PlayerLives ++;
				AudioManager.instance.Play("OneUp");
			}
			if(other.gameObject.tag == "Star")
			{
				GameController.instance.TempState = GetCurrentState();
				if(GameController.instance.PlayerState < 1)
				{
					TransformMario(3);
				}
				else if(GameController.instance.PlayerState == 1)
				{
					TransformMario(4);
				}
				else if(GameController.instance.PlayerState == 2)
				{
					TransformMario(5);
				}
				AudioManager.instance.Stop(GameController.instance.Music);
				AudioManager.instance.Play("Invincibility");
				Invoke("TurnBackToNormal", 10f);
			}
		}
			if(other.gameObject.tag == "Goal")
			{
				animator.SetTrigger("Goal");
				Invoke("Goal", 0.2f);
				AudioManager.instance.Stop("FlapPole");
			}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enterable Pipe")
		{
			if(Input.GetKey(KeyCode.DownArrow))
			{
				if(!visited) // prevent multiple instantiations
				{	
					SetCurrentMario(); // WHY DO I HAVE TO DO THIS
					// Debug.Log(currentMarioAnimation);
					Instantiate(currentMarioAnimation, transform.position, Quaternion.identity, transform);
					AudioManager.instance.Stop(GameController.instance.Music);
					AudioManager.instance.Play("Pipe");
				}
				visited = true;
				spriteRenderer.enabled = false;
				Invoke("MoveToUnderground", 1f);
			}
		}
		if(other.gameObject.tag == "Pipe Exit")
		{
			if(Input.GetKey(KeyCode.RightArrow))
			{
				GameController.instance.Exit = true;
				if(!visitedExit)
				{
					Instantiate(currentMarioAnimation, transform.position, Quaternion.identity, transform);
					AudioManager.instance.Stop(GameController.instance.UndergroundMusic);
					AudioManager.instance.Play("Pipe");
				}
				visitedExit = true;
				spriteRenderer.enabled = false;
				Invoke("MoveAboveGround", 1f);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!GameController.instance.Dead)
		{
			if(other.gameObject.tag == "FireFlower")
			{
				if(GameController.instance.PlayerState < 2)
				{
					TransformMario(2);
					AudioManager.instance.Play("PowerUp");
				}
				else if(GameController.instance.PlayerState >= 3)
				{
					GameController.instance.TempState = 2;
				}
				GameController.instance.Score += 1000;
			}
			if(other.gameObject.tag == "Coin")
			{
				AudioManager.instance.Play("Coin");
				GameController.instance.Coins ++;
				Destroy(other.gameObject);
			}
			// Debug.Log(GameController.instance.PlayerState);
			if(GameController.instance.PlayerState > 0 && GameController.instance.PlayerState < 3)
			{
				ShrinkMario(other, "GoombaSide");
				ShrinkMario(other, "KoopaSide");
			}
		}
		if(other.gameObject.tag == "Pole")
		{
			GameController.instance.Dead = true; // CLEANER SOLUTION???
			player.constraints = RigidbodyConstraints2D.FreezeAll;
			player.constraints = RigidbodyConstraints2D.FreezePositionX;
			player.constraints = RigidbodyConstraints2D.FreezeRotation;
			GameController.instance.Finish = true;
			animator.SetTrigger("Finish");
			AudioManager.instance.Stop(GameController.instance.Music);
			AudioManager.instance.Play("FlagPole");
		}
	}

	void MoveToUnderground()
	{
		GameController.instance.Underground = true;
		transform.position = new Vector2(-16.5f, -1.5f);
		spriteRenderer.enabled = true;
		player.constraints = RigidbodyConstraints2D.FreezeRotation;
		AudioManager.instance.Play(GameController.instance.UndergroundMusic);
	}

	void MoveAboveGround()
	{
		AudioManager.instance.Play("Pipe");
		transform.position = new Vector2(9.28f, -0.43f);
		Invoke("EnableSpriteRenderer", 1f);
	}

	void EnableSpriteRenderer()
	{
		GameController.instance.Underground = false;
		spriteRenderer.enabled = true;
		player.constraints = RigidbodyConstraints2D.FreezeRotation;
		AudioManager.instance.Play(GameController.instance.Music);
	}

	public void DeTransform()
	{
		GameController.instance.Transition = true;
		if(bigMario.activeSelf)
		{
			animator.SetTrigger("Hit");
			Invoke("BigToSmall", 1f);
		}
		if(fireMario.activeSelf)
		{
			animator.SetTrigger("Hit");
			Invoke("FireToBig", 1f);
		}
		AudioManager.instance.Play("Pipe");
	}

	void SmallToBig()
	{
		TransformMario(1);
	}

	void SmallStarToBigStar()
	{
		TransformMario(4);
		// Debug.Log(GameController.instance.PlayerState);
	}

	void BigToSmall()
	{
		bigMario.SetActive(false);
		smallMario.SetActive(true);
		SetCurrentMario();
		GameController.instance.Transition = false;
		smallMario.transform.position = bigMario.transform.position;
	}

	void FireToBig()
	{
		fireMario.SetActive(false);
		bigMario.SetActive(true);
		SetCurrentMario();
		GameController.instance.Transition = false;
		bigMario.transform.position = fireMario.transform.position;
	}

	void ShrinkMario(Collider2D other, string enemy)
	{
		if(other.gameObject.tag == enemy)
		{
			DeTransform();
		}
	}

	void TransformMario(int state)
	{
		Vector3 tempPos = GameController.instance.CurrentMario.transform.position;
		GameController.instance.CurrentMario.SetActive(false);
		GameController.instance.PlayerState = state;
		SetCurrentMario();
		GameController.instance.CurrentMario.SetActive(true);
		GameController.instance.CurrentMario.transform.position = tempPos;
	}

	void SetCurrentMario()
	{
		GameController.instance.CurrentMario = marios[GetCurrentState()];
		currentMarioAnimation = marioAnimations[GetCurrentState()];
		currentMarioWalkAnimation = marioWalkAnimations[GetCurrentState()];
		// Debug.Log(GetCurrentState());
		// Debug.Log(currentMarioWalkAnimation);
		// Debug.Log(currentMarioAnimation);
		spriteRenderer.flipX = !GameController.instance.FacingRight;
	}

	int GetCurrentState()
	{
		return GameController.instance.PlayerState;
	}

	void Goal()
	{
		transform.position = new Vector2(14.9f, -0.6f);
		spriteRenderer.flipX = true;
		Invoke("WalkToCastle", 0.2f);
	}

	void WalkToCastle()
	{
		spriteRenderer.enabled = false;
		Instantiate(currentMarioWalkAnimation, transform.position, Quaternion.identity, transform);
		Invoke("InCastle", 2f);
		AudioManager.instance.Play("StageClear");
	}

	void InCastle()
	{
		GameController.instance.InCastle = true;
	}

	void TurnBackToNormal()
	{
		TransformMario(GameController.instance.TempState);
		AudioManager.instance.Stop("Invincibility");
		AudioManager.instance.Play(GameController.instance.Music);
	}
}
