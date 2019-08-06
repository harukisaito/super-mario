using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour {

	[SerializeField] GameObject debrisUL;
	[SerializeField] GameObject debrisUR;
	[SerializeField] GameObject debrisLL;
	[SerializeField] GameObject debrisLR;

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
	}

	void Update()
	{
		if(!spriteRenderer.enabled)
		{
			player.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		if(GameController.instance.Finish)
		{
			player.velocity = new Vector2(0, -0.9f);
		}
		if(MarioSpawner.Instance.InStarMode)
		{
			MarioSpawner.Instance.StarTimer += Time.deltaTime;
			if(MarioSpawner.Instance.StarTimer >= 10)
			{
				StartCoroutine(TransformMario(0f, GameController.instance.TempState));
				MarioSpawner.Instance.InStarMode = false;
				AudioManager.instance.Stop("Invincibility");
				AudioManager.instance.Play(GameController.instance.Music);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(!GameController.instance.Dead)
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
		
			if(other.gameObject.tag == "Mushroom")
			{
				animator.SetTrigger("Grow");
				GameController.instance.Score.currentScore += 1000;
				if(GameController.instance.PlayerState == 3)
				{
					StartCoroutine(TransformMario(1f, 4));
					GameController.instance.TempState = 1;
				}
				else
				{
					StartCoroutine(TransformMario(1f, 1));
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
				GameController.instance.TempState = GameController.instance.PlayerState;
				MarioSpawner.Instance.InStarMode = true;
				AudioManager.instance.Stop(GameController.instance.Music);
				AudioManager.instance.Play("Invincibility");
				if(GameController.instance.PlayerState < 1)
				{
					StartCoroutine(TransformMario(0f, 3));
				}
				else if(GameController.instance.PlayerState == 1)
				{
					StartCoroutine(TransformMario(0f, 4));
				}
				else if(GameController.instance.PlayerState == 2)
				{
					StartCoroutine(TransformMario(0f, 5));
				}
			}
		}

		if(other.gameObject.tag == "Goal")
		{
			animator.SetTrigger("Goal");
			Invoke("Goal", 0.2f);
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
					MarioSpawner.Instance.PlayAnimation("pipe");
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
					MarioSpawner.Instance.PlayAnimation("pipe");
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
					animator.SetTrigger("Grow");
					StartCoroutine(TransformMario(1f, 2));
					
					AudioManager.instance.Play("PowerUp");
				}
				else if(GameController.instance.PlayerState >= 3)
				{
					GameController.instance.TempState = 2;
				}
				GameController.instance.Score.currentScore += 1000;
			}
			if(other.gameObject.tag == "Coin")
			{
				AudioManager.instance.Play("Coin");
				GameController.instance.Coins ++;
				Destroy(other.gameObject);
			}
			if(GameController.instance.PlayerState > 0 && GameController.instance.PlayerState < 3)
			{
				if(other.gameObject.tag == "GoombaSide" || other.gameObject.tag == "KoopaSide")
				{
					GameController.instance.Transition = true;
					animator.SetTrigger("Hit");
					StartCoroutine(TransformMario(1f, GameController.instance.PlayerState - 1));
					AudioManager.instance.Play("Pipe");
				}
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

	IEnumerator TransformMario(float time ,int state)
	{
		GameController.instance.Transition = true;
		yield return new WaitForSeconds(time);
		MarioSpawner.Instance.TransformMario(state);
		GameController.instance.Transition = false;
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
		MarioSpawner.Instance.PlayAnimation("castle");
		Invoke("InCastle", 2f);
		AudioManager.instance.Play("StageClear");
	}

	void InCastle()
	{
		GameController.instance.InCastle = true;
	}
}
