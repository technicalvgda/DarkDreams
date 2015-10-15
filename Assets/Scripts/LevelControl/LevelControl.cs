using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelControl : MonoBehaviour
{

    //public float MAX_TIME = 5.0f;
    public float timer = 5.0f;
    float fadeTime = 3.0f;
    GameObject player;
    public GameObject overlay;
    Image overlay_image;
    GameObject gameOverPanel;
    //GameObject retryButton;
    //GameObject menuButton;
    GameObject spottedCue;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        spottedCue = GameObject.Find("SpottedIndicator");
        spottedCue.SetActive(false);
        player = GameObject.Find("Player");
        gameOverPanel = GameObject.Find("GameOverPanel");
        //retryButton = GameObject.Find("RetryButton");
        //menuButton = GameObject.Find("MenuButton");
        gameOverPanel.SetActive(false);
        overlay.SetActive(false);
        overlay_image = overlay.GetComponent<Image>();
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
                StartCoroutine(fadeToBlack());
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
