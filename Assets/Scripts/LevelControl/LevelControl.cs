using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {

    public float MAX_TIME = 5.0f;
    public float timer = 5.0f;
    public GameObject player;
    public GameObject gameOverPanel;
    public GameObject retryButton;
    public GameObject quitButton;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        gameOverPanel = GameObject.Find("GameOverPanel");
        retryButton = GameObject.Find("RetryButton");
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

    private void Retry() { //The retry button when the game over screen pops up
        player.GetComponent<PlayerControl>().isAlive = true;
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }

    void MainMenu() { //The main menu button when the game over screen pops up

    }

}
