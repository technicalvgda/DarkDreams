using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public GameObject pausePanel;
	public GameObject gameOverPanel;
	private bool paused=false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!gameOver ())
		{
			if (Input.GetKeyDown (KeyCode.Escape))
				togglePause ();
		}
		if (gameOver())
		{
			Time.timeScale= 0f;
		}
	}
	void togglePause()
	{
		if (paused)
		{
			Time.timeScale = 1f;
			pausePanel.SetActive (false);
			paused = false;
		}
		else
		{
			Time.timeScale = 0f;
			pausePanel.SetActive (true);
			paused = true;
		}
	}
	bool gameOver()
	{
		if (/*Monster detects you and the game is over, then the game will be over and the pause scren will not work*/)
		{
			gameOverPanel.SetActive (true);
			return true;
		}
		else
		{
			return false;
		}
	}
}