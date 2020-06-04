using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public int currentGameLevel;
	private GameObject playership;
	public static GameManager instance;
	public GameObject MenuUi;
	public GameObject GameUi;
	private string mode;
	private int score;
	private int highscore;
	private int lives;
	private Text scoreText;
	private Text highscoreText;
	private Text livesText;

    // Start is called before the first frame update
    void Start()
    {
			instance = this;
			highscore = 0;
			Camera.main.transform.position = new Vector3(0f, 30f, 0f);
			Camera.main.transform.LookAt(Vector3.zero, new Vector3(0f, 0f, 1f));
			ChangeScreen("StartScreen");
    }
		public void StartGame()
		{
			currentGameLevel = 0;
			score = 0;
			lives = 4;
			ChangeScreen("MainScreen");
			CreatePlayerShip();
			StartNextLevel();
			scoreText = GameObject.Find("Score").GetComponent<Text> ();
			highscoreText = GameObject.Find("Highscore").GetComponent<Text> ();
			livesText = GameObject.Find("Lives").GetComponent<Text> ();
		}
		public void IncrementScore(int howMuch)
		{
			score += howMuch;
			if (score > highscore){
				highscore = score;
			}
		}

    // Update is called once per frame
    void Update()
    {
			if (mode == "MainScreen")
			{
				scoreText.text = "Score:- " + score;
				livesText.text = "Lives:- " + lives;
				highscoreText.text = "Highscore:- " + highscore;
				CheckRemainingAsteroids();
			}


    }

		void ChangeScreen(string newMode)
		{
			mode = newMode;

			if (mode == "StartScreen")
			{
				MenuUi.SetActive(true);
				GameUi.SetActive(false);
			}
			if (mode == "MainScreen")
			{
				MenuUi.SetActive(false);
				GameUi.SetActive(true);
			}
		}

	public void CreatePlayerShip()
	{
		if (lives > 1)
		{
			lives -= 1;
			playership = (GameObject)Instantiate(Resources.Load("Spaceship"));
		}
		else
		{
			DestroyAllAsteroids();
			ChangeScreen("StartScreen");
		}
	}

	public void StartNextLevel()
	{
		playership.GetComponent<Spaceship>().ResetShip();
		currentGameLevel += 1;
		for (int i = 0; i < currentGameLevel; i++)
		{
			int side = Random.Range(0, 4);
			float Xrand = Random.Range(0, Screen.width);
			float Yrand = Random.Range(0, Screen.height);
			Vector3 asteroidPos = new Vector3(0f, 0f, 30f);
			if (side == 0)
			{
				asteroidPos.x = 30f;
				asteroidPos.y = Yrand;
			}
			if (side == 1)
			{
				asteroidPos.x = Xrand;
				asteroidPos.y = 30f;
			}
			if (side == 2)
			{
				asteroidPos.x = Screen.width - 30f;
				asteroidPos.y = Yrand;
			}
			if (side == 3)
			{
				asteroidPos.x = Xrand;
				asteroidPos.y = Screen.height - 30f;
			}
			Vector3 asteroidPosWorld = Camera.main.ScreenToWorldPoint(asteroidPos);
			GameObject asteroid = (GameObject)Instantiate(Resources.Load("Asteroid"));
			asteroid.transform.position = asteroidPosWorld;
		}
	}
    void DestroyAllAsteroids()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Asteroid");
    foreach(GameObject enemy in enemies)
      GameObject.Destroy(enemy);
  }
	void CheckRemainingAsteroids()
{
	GameObject[] enemies = GameObject.FindGameObjectsWithTag("Asteroid");
	if (enemies.Length == 0)
	{
		StartNextLevel();
	}

}

}
