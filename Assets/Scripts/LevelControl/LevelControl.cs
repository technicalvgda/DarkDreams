using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelControl : MonoBehaviour
{
    // public float MAX_TIME = 5.0f;
    public float timer = 5.0f;
    // float fadeTime = 3.0f;    //disabled for compiler error -joel
    public GameObject overlay;

    GameObject player;
    GameObject gameOverPanel;

    Image overlay_image;

    // GameObject retryButton;
    // GameObject menuButton;
    // GameObject spottedCue;
    // Use this for initialization

    private Vector2 initialPlayerPos;

    void Start()
    {
        Time.timeScale = 1f;
        // spottedCue = GameObject.Find("SpottedIndicator");
        // spottedCue.SetActive(false);
        player = GameObject.Find("Player");
        initialPlayerPos = player.transform.position;

        gameOverPanel = GameObject.Find("GameOverPanel");
        // retryButton = GameObject.Find("RetryButton");
        // menuButton = GameObject.Find("MenuButton");
        gameOverPanel.SetActive(false);
        overlay.SetActive(false);
        overlay_image = overlay.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over Screen and its options
        // Activates game over screen when player is not alive
        if (player.GetComponent<PlayerControl>().isAlive == false)
        {
            // Disables player control script
            player.GetComponent<PlayerControl>().enabled = false; 

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                StartCoroutine(fadeToBlack());
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    // The retry button when the game over screen pops up
    public void Retry()
    {   
        // Set player position to initial position
        player.transform.position = initialPlayerPos;

        // Set gameover overlay to false
        gameOverPanel.SetActive(false);
        
        // Set player to alive
        player.GetComponent<PlayerControl>().isAlive = true;

        // Unpause time? not sure -wilb
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {   //The main menu button when the game over screen pops up
        Time.timeScale = 1f;
        //load first level in hierarchy
        Application.LoadLevel(0);

    }
    public IEnumerator fadeToBlack()
    {
        overlay.SetActive(true);
        Color c = overlay_image.color;


        float progress = 0.0f;

        while (progress < 250)
        {
            progress += .015f;
            c.a = progress;
            //Debug.Log(progress);
            overlay_image.color = c;

            yield return null;
        }

        //c.a = 255.0f;
        //overlay_image.color = c;

    }
}
