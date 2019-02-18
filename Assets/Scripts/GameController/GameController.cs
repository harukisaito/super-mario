using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField] Text scoreText;
	[SerializeField] Text timeText;
	[SerializeField] Text coinText;
	[SerializeField] Text playerLivesText;
	[SerializeField] Text winnerText;
	[SerializeField] Text gameOverText;

	[SerializeField] Image marioImage;

	[SerializeField] GameObject goalFlag;

	List<Rigidbody2D> fireBalls = new List<Rigidbody2D>();
	List<GameObject> goombas = new List<GameObject>();

	GameObject currentMario;


	int playerLives = 3;
	int playerState = 0;
	int coins;
	int score;
	int timeAmount = 400;
	int timer = 6;
	int tempState;

	float time = 0;
	float koopaTimer;
	float timeSpeed = 2.5f;

	bool dead;
	bool facingRight = true;
	bool exit;
	bool transition;
	bool underground;
	bool hurry;
	bool loaded;
	bool finish;
	bool inCastle;
	bool paused;

	string music = "Music";
	string undergroundMusic = "UndergroundMusic";


	public int PlayerState
	{
		get {return playerState;}
		set {playerState = value;}
	}
	public int PlayerLives
	{
		get {return playerLives;}
		set {playerLives = value;}
	}
	public int Coins
	{
		get {return coins;}
		set {coins = value;}
	}
	public int Score
	{
		get {return score;}
		set {score = value;}
	}
	public int Timer
	{
		get {return timer;}
		set {timer = value;}
	}
	public int TempState
	{
		get {return tempState;}
		set {tempState = value;} 
	}


	public float KoopaTimer
	{
		get {return koopaTimer;}
		set {koopaTimer = value;}
	}


	public bool Dead
	{
		get {return dead;}
		set {dead = value;}
	}
	public bool FacingRight
	{
		get {return facingRight;}
		set {facingRight = value;}
	}
	public bool Exit
	{
		get {return exit;}
		set {exit = value;}
	}
	public bool Transition
	{
		get {return transition;}
		set {transition = value;}
	}
	public bool Underground
	{
		get {return underground;}
		set {underground = value;}
	}
	public bool Finish
	{
		get {return finish;}
		set {finish = value;}
	}
	public bool InCastle
	{
		get {return inCastle;}
		set {inCastle = value;}
	}
	public bool Paused
	{
		get {return paused;}
		set {paused = value;}
	}


	public string Music
	{
		get {return music;}
		set {music = value;}  
	}
	public string UndergroundMusic
	{
		get {return undergroundMusic;}
		set {undergroundMusic = value;}  
	}


	public List<Rigidbody2D> FireBalls
	{
		get {return fireBalls;}
		set {fireBalls = value;}
	}

	public List<GameObject> Goombas
	{
		get {return goombas;}
		set {goombas = value;}
	}

	public GameObject CurrentMario
	{
		get {return currentMario;}
		set {currentMario = value;}
	}

    public static GameController instance;

    void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		marioImage.enabled = false;
		playerLivesText.enabled = false;
		gameOverText.enabled = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			AudioManager.instance.Play("Pause");
			if(!paused)
			{
				if(underground)
				{
					AudioManager.instance.Pause(undergroundMusic);
				}
				else
				{
					AudioManager.instance.Pause(music);
				}
				Time.timeScale = 0f;
				paused = true;
			}
			else
			{
				if(underground)
				{
					AudioManager.instance.UnPause(undergroundMusic);
				}
				else
				{
					AudioManager.instance.UnPause(music);
				}
				Time.timeScale = 1f;
				paused = false;
			}
		}
		if(playerState == -1 && !loaded)
		{
			Invoke("LoadGameOverScene", 3f);
			loaded = true;
			playerLives -= 1;
		}

		if(timer == 0)
		{
			if(Input.anyKey)
			{
				AudioManager.instance.Play("Music");
				ResetStats();
				winnerText.enabled = false;
				gameOverText.enabled = false;
				playerLives = 3;
				score = 0;
				coins = 0;
				timer = 6;
				finish = false;
				inCastle = false;
				timeSpeed = 2.5f;
				SceneManager.LoadScene(0);
			}
		}

		ShowCoins();
		ShowTime();
		TimeController();
		ShowScore(score.ToString().Length);
	}

	void ShowScore(int digitCount)
	{
		int zeroCount = 6 - digitCount;
		char zero = '0';
		
		String zeroes = new String(zero, zeroCount);

		scoreText.text = zeroes + score;
	}

	void ShowTime()
	{
		if(inCastle)
		{
			TimeToScore();
		}
		if(playerState != -1 && timer > 5)
		{
			time += Time.deltaTime * timeSpeed;
			int intTime = (int)time;
			timer = timeAmount - intTime;
			timeText.text = "" + timer;
		}
		else if(timer != 0 && finish)
		{
			AudioManager.instance.Play("Coin");
			timer = 0;
			timeText.text = "" + timer;
			Instantiate(goalFlag, new Vector2(15.76f, 0), Quaternion.identity);
			winnerText.text = "YOU DID IT! PRESS ANY KEY TO CONTINUE";
			winnerText.enabled = true;
		}
	}

	void ShowCoins()
	{
		if(coins < 10)
		{
			coinText.text = "x0" + coins;
		}
		else
		{
			coinText.text = "x" + coins;
		}
	}

	void LoadGameOverScene()
	{
		SceneManager.LoadScene(1);
		Invoke("EnablePlayerLivesLeft", 0.04f);
		if(playerLives > -1)
		{
			Invoke("LoadGameScene", 2f);	
		}
	}

	void LoadGameScene()
	{
		playerLivesText.enabled = false;
		gameOverText.enabled = false;
		marioImage.enabled = false;
		SceneManager.LoadScene(0);
		AudioManager.instance.Play("Music");
		ResetStats();
	}

	void EnablePlayerLivesLeft()
	{
		if(playerLives < 0)
		{
			gameOverText.enabled = true;
			AudioManager.instance.Play("GameOver");
			gameOverText.text = "GAME OVER";
			playerLives = 3;
			score = 0;
			coins = 0;
		}
		else
		{
			playerLivesText.enabled = true;
			marioImage.enabled = true;
			playerLivesText.text = "x  " + playerLives;
		}
	}

	void ResetStats()
	{
		timeAmount = 400;
		time = 0;
		loaded = false;
		playerState = 0;
		dead = false;
		exit = false;
		transition = false;
	}

	void TimeToScore()
	{

		timeSpeed = 100;
		if((timer / 10) > 1)
		{
			score += 100;
		}
	}

	void TimeController()
	{
		if(!finish)
		{
			if(timer < 100)
			{
				if(!underground && !hurry)
				{
					AudioManager.instance.Stop("Music");
					AudioManager.instance.Play("HurryMusic");
					music = "HurryMusic";
					undergroundMusic = "HurryUndergroundMusic";
					hurry = true;
				}
				else if(underground && !hurry)
				{
					AudioManager.instance.Stop("UndergroundMusic");
					AudioManager.instance.Play("HurryUndergroundMusic");
					undergroundMusic = "HurryUndergroundMusic";
					music = "HurryMusic";
					hurry = true;
				}
			}
		}
	}
}
