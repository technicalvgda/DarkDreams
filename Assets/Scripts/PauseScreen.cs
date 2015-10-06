using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {
	public GameObject pausePanel;
	private bool paused=false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			paused = !paused;
		}
		if(paused)
		{
			Time.timeScale=0;
			pausePanel.SetActive(true);
		}
		if(!paused)
		{
			Time.timeScale=1;
			pausePanel.SetActive(false);
		}
		
	}
	
}