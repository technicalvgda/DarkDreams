using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour
{

    //public float MAX_TIME = 5.0f;
    public float timer = 5.0f;
    GameObject player;
    GameObject gameOverPanel;
    //GameObject retryButton;
    //GameObject menuButton;
    GameObject spottedCue;
    // Use this for initialization
    void Start()
    {
        spottedCue = GameObject.Find("SpottedIndicator");
        spottedCue.SetActive(false);
        Time.timeScale = 1f;
        player = GameObject.Find("Player");
        gameOverPanel = GameObject.Find("GameOverPanel");
        //retryButton = GameObject.Find("RetryButton");
        //menuButton = GameObject.Find("MenuButton");
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Game Over Screen and its options
        if (player.GetComponent<PlayerControl>().isAlive == false)
        { //Activates game over screen when player is not alive
            player.GetComponent<PlayerControl>().enabled = false; //Disables player control script

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void Retry()
    { //The retry button when the game over screen pops up
        player.GetComponent<PlayerControl>().isAlive = true;
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    { //The main menu button when the game over screen pops up
        Time.timeScale = 1f;
        Application.LoadLevel("Title Screen");
        
    }

   

}
