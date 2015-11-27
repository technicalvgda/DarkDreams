using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelControl : MonoBehaviour
{
    AudioHandlerScript audioHandler;
    //time to wait until starting level music
    float waitTime = 12f;
    //public float MAX_TIME = 5.0f;
    public float timer = 5.0f;
   // float fadeTime = 3.0f;    //disabled for compiler error -joel
    GameObject player;
	PlayerControl playerScript;
    OpeningCutscene openCutscene;
    public GameObject overlay;
    Image overlay_image;
    GameObject gameOverPanel;
    Vector2 initialPlayerPos;
    bool gameOver = false;
    //GameObject retryButton;
    //GameObject menuButton;
    //GameObject spottedCue;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        // spottedCue = GameObject.Find("SpottedIndicator");
        // spottedCue.SetActive(false);
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerControl> ();
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        // set the initial position of the player
        initialPlayerPos = player.transform.position;
        //only store this if the player is on level 1, 2, or 3
        if (Application.loadedLevel >= 1 && Application.loadedLevel <= 3)
        {
            openCutscene = GameObject.Find("Cutscene").GetComponent<OpeningCutscene>();
        }
        gameOverPanel = GameObject.Find("GameOverPanel");
        //retryButton = GameObject.Find("RetryButton");
        //menuButton = GameObject.Find("MenuButton");
        gameOverPanel.SetActive(false);
        overlay.SetActive(false);
        overlay_image = overlay.GetComponent<Image>();
        //play music clip #3 (intro music)
        audioHandler.LoopMusic(false);
        audioHandler.PlayMusic(3);
        //start level music at certain time
        StartCoroutine("levelMusic");
        
        
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
            if (timer <= 0 && gameOver == false)
            {
                gameOver = true;
                StartCoroutine(fadeToBlack());
               
                //play music clip #2 (game over music)
                audioHandler.LoopMusic(false);
                //play death crunch sound
                audioHandler.PlaySound(0);
                audioHandler.PlayMusic(2);
                gameOverPanel.SetActive(true);
                
                Time.timeScale = 0f;
                
            }
        }
    }

    // The retry method when the GameOver overlay pops up
    public void Retry()
    {
        gameOver = false;
        // Set player position to initial position in basement
        player.transform.position = initialPlayerPos;
       
        // Set GameOver and fadetoblack overlay to false
        gameOverPanel.SetActive(false);
        overlay.SetActive(false);

        // Set player to alive and enable movement
        playerScript.isAlive = true;
        playerScript.enabled = true;
        playerScript.hide = false;
        playerScript.normalSpeed = playerScript.defaultSpeed;
        player.GetComponent<SpriteRenderer>().color = playerScript.initialColor;
        playerScript.StopCoroutine("SpawnHunterMonster");
        // Resume time
        Time.timeScale = 1f;
        //resumes hunter speed and resets position
        openCutscene.EndCutscene();
        openCutscene.hunterEnemy.SetActive(true);
        StartCoroutine("DespawnBasementHunter");
        playerScript.hunterScript = openCutscene.hunterEnemy.GetComponent<Hunter>();
		playerScript.hunterScript.isCaught = false;
		playerScript.hunterScript.speed = 5.0f;
		playerScript.hunterScript.transform.position = playerScript.hunterScript.originalPosition;
        playerScript.hunterScript.transform.rotation = playerScript.hunterScript.originalRotation;
        //resumes chasing monster speed
        if (playerScript.chasingMonsterScript != null)
        {
            playerScript.chasingMonsterScript.speedNormal = 2.0f;
            playerScript.chasingMonsterScript.speedChasing = 4.0f;
        }
        //play intro music
        audioHandler.LoopMusic(false);
        audioHandler.PlayMusic(3);
        //play level music
        StartCoroutine("levelMusic");
    }

    public void MainMenu()
    { //The main menu button when the game over screen pops up
        Time.timeScale = 1f;
        //load first level in hierarchy
        Application.LoadLevel(0);

    }
    public IEnumerator DespawnBasementHunter()
    {
        yield return new WaitForSeconds(15f);
        openCutscene.hunterEnemy.SetActive(false);
        yield return null;
    }
    public IEnumerator fadeToBlack()
    {
        overlay.SetActive(true);
        Color c = overlay_image.color;


        float progress = 1.0f;

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
    public IEnumerator levelMusic()
    {
        ///wait for length of intro
        yield return new WaitForSeconds(waitTime);
        //play and loop level music
        audioHandler.LoopMusic(true);
        audioHandler.PlayMusic(5);
        yield return null;
    }
}
