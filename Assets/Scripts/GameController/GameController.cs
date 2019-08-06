using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
// using UnityEditor;

public class GameController : MonoBehaviour {

	[SerializeField] Text scoreText, timeText, coinText, playerLivesText, gameOverText;
	[SerializeField] Image marioImage;
	[SerializeField] GameObject goalFlag;
	[SerializeField] Text[] scores;
	[SerializeField] Text[] scoreLabels;
	[SerializeField] Text controlsText;

	List<Rigidbody2D> fireBalls = new List<Rigidbody2D>();
	List<GameObject> goombas = new List<GameObject>();

	GameObject currentMario;

	public Score Score {get; set;}

	int playerLives = 3;
	int playerState = 0;
	int coins;
	int timeAmount = 400;
	int timer = 6;
	int tempState;

	float time = 0;
	float koopaTimer;
	float timeSpeed = 2.5f;

	bool dead = true;
	bool facingRight = true;
	bool exit;
	bool transition;
	bool underground;
	bool hurry;
	bool loaded;
	bool finish;
	bool inCastle;
	bool paused;
	bool end;
	bool gameOver;
	bool muted;

	string music = "Music";
	string undergroundMusic = "UndergroundMusic";
	string path;
	string jsonPath = "Assets/JSON Files";
	string guid;
	


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

		Screen.SetResolution(800, 700, false, 30);

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

		for(int i = 0; i < scoreLabels.Length; i++)
		{
			scoreLabels[i].enabled = false;
		}
		for(int i = 0; i < scores.Length; i++)
		{
			scores[i].enabled = false;
		}

		Score = new Score();
		if(!Directory.Exists(jsonPath))
		{
			#if UNITY_EDITOR
			guid = UnityEditor.AssetDatabase.CreateFolder("Assets", "JSON Files");
			#endif
		}
		else
		{
			DeserializeFromJson();
		}

		Score.currentScore = 0;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			AudioManager.instance.Play("Pause");
			if(!paused)
			{
				AudioManager.instance.PauseAllSounds();
				Time.timeScale = 0f;
				paused = true;
			}
			else
			{
				AudioManager.instance.UnPauseAllSounds();
				Time.timeScale = 1f;
				paused = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.M))
		{
			if(!muted)
			{
				AudioManager.instance.MuteAllSounds();
				muted = true;
			}
			else
			{
				AudioManager.instance.UnMuteAllSounds();
				muted = false;
			}
		}

		if(playerState == -1 && !loaded)
		{
			Invoke("LoadGameOverScene", 3f);
			loaded = true;
			playerLives -= 1;
		}

		if(gameOver)
		{
			if(Input.anyKey)
			{
				ResetStats();
				gameOverText.enabled = false;
				SceneManager.LoadScene(0);
				AudioManager.instance.Play(music);
				gameOver = false;
			}
		}

		if(timer == 0)
		{
			if(Input.anyKey && end)
			{
				AudioManager.instance.Play("Music");
				ResetStats();
				gameOverText.enabled = false;
				playerLives = 3;
				Score.currentScore = 0;
				coins = 0;
				timer = 6;
				finish = false;
				inCastle = false;
				timeSpeed = 2.5f;
				for(int i = 0; i < scoreLabels.Length; i++)
				{
					scoreLabels[i].enabled = false;
				}
				for(int i = 0; i < scores.Length; i++)
				{
					scores[i].enabled = false;
				}
				SceneManager.LoadScene(0);
			}
		}

		ShowCoins();
		ShowTime();
		TimeController();
		scoreText.text = ShowCurrentScore(Score.currentScore.ToString().Length, -1);
	}

	public void DisableControlsText()
	{
		controlsText.enabled = false;
	}

	string ShowCurrentScore(int digitCount, int index)
	{
		int zeroCount = 6 - digitCount;
		char zero = '0';
		
		String zeroes = new String(zero, zeroCount);

		if(index == -1)
		{
			return (zeroes + Score.currentScore);
		}
		else
		{
			return (zeroes + Score.scores[index]);
		}
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
			Invoke("ShowScoreBoard", 3f);			
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
		if(playerLives >= 0)
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
			AudioManager.instance.StopAllCurrentSounds();
			AudioManager.instance.Play("GameOver");
			gameOverText.text = "GAME OVER";
			playerLives = 3;
			Score.currentScore = 0;
			coins = 0;
			Invoke("GameOver", 5f);
		}
		else
		{
			playerLivesText.enabled = true;
			marioImage.enabled = true;
			playerLivesText.text = "x " + playerLives;
		}
	}

	void GameOver()
	{
		gameOver = true;
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
			Score.currentScore += 100;
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

	void ShowScoreBoard()
	{
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene()
	{
	 	SetScoreArray();
		SerializeToJson(); // SERIALIZE THE CURRENT ARRAY
		yield return SceneManager.LoadSceneAsync(1);
		SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
		for(int i = 0; i < scoreLabels.Length; i++)
		{
			scoreLabels[i].enabled = true;
		}
		for(int i = 0; i < scores.Length; i++)
		{
			scores[i].enabled = true;
		}   
		// NEW SCENE IS LOADED AND SCORE OBJECT IS NULL
		LoadScore();

		string yourScore = ShowCurrentScore(Score.currentScore.ToString().Length, -1);
		scores[4].text = yourScore;

		SerializeToJson();

		yield return new WaitForSeconds(3f);
		end = true;
	}

	void LoadScore()
	{
		DeserializeFromJson(); // SCORE IS NO LONGER NULL
		RankScore(); // SCORES ARE IN RIGHT ORDER SMALL TO BIG

		for(int i = 0; i < Score.scores.Length; i++)
		{
			int index = Score.scores.Length - 1 - i; // LAST INDEX IN ARRAY TO FIRST
			scores[i].text = ShowCurrentScore(Score.scores[ // SCORE ARRAY
				index]
				.ToString().Length,  // LENGTH OF THE STRING
				index); // INDEX IN ARRAY
		}
	}

	void RankScore()
	{
		Array.Sort(Score.scores);
	}

	void SetScoreArray()
	{
		// DESERIALIZE SCORES IN AWAKE SO THAT CURRENT SCORE IS THE ONE DURING PLAY AND NOT THE ONE DESERIALIZED

		if(Score.scores == null || Score.scores.Length == 0) // IF THERE ARE NO SCORES YET INSTANTIATE THE ARRAY
		{
			Score.scores = new int[4];
			Score.scores[0] = Score.currentScore; // AND ADD THE CURRENT SCORE TO THE ARRAY
		}
		else
		{
			for(int i = 0; i < Score.scores.Length; i++) // IF THERE ALREADY IS AN ARRAY ADD THE CURRENT SCORE IF AN INDEX IS EMPTY
			{
				if(Score.scores[i] == 0 || Score.scores[i] < Score.currentScore) // SECOND ARGUMENT WORKS BECAUSE THE ARRAY IS IN ORDER OTHERWISE YOU NEED TO CHECK EVERY ELEMENT FOR ITS SIZE
				{
					Score.scores[i] = Score.currentScore;
					break;
				}
			}
		}
	}

	void SerializeToJson()
	{
		path = Path.Combine(jsonPath, "Scores.json");

		string json = JsonUtility.ToJson(Score, true);
		using(StreamWriter writer = new StreamWriter(path))
		{
			writer.Write(json);
		}
	}

	void DeserializeFromJson()
	{
		string json;

		path = Path.Combine(jsonPath, "Scores.json");
		using(StreamReader reader = new StreamReader(path))
		{
			json = reader.ReadToEnd();
		}
		Score = JsonUtility.FromJson<Score>(json);
	}
}
