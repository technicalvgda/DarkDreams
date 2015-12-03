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
    FinalLevelCutscene endCutscene;
    public GameObject overlay;
    Image overlay_image;
    GameObject gameOverPanel;
    Vector2 initialPlayerPos;
    bool gameOver = false;
    bool gameOverMusicPlaying = false;
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
        if (Application.loadedLevel >= 3 && Application.loadedLevel <= 5)
        {
            openCutscene = GameObject.Find("Cutscene").GetComponent<OpeningCutscene>();
        }
        //only store if final level
        else if(Application.loadedLevel == 6)
        {
            endCutscene = GameObject.Find("HallwayRoom1").GetComponent<FinalLevelCutscene>();
        }
        gameOverPanel = GameObject.Find("GameOverPanel");
        //retryButton = GameObject.Find("RetryButton");
        //menuButton = GameObject.Find("MenuButton");
        gameOverPanel.SetActive(false);
        overlay.SetActive(false);
        overlay_image = overlay.GetComponent<Image>();
        //play music clip #3 (intro music)
        /*
        audioHandler.LoopMusic(false);
        audioHandler.PlayMusic(3);
        */
        //start level music at certain time
        //StartCoroutine("levelMusic");
        
        
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
                audioHandler.StopMusic();
                gameOver = true;
                StartCoroutine(fadeToBlack());
               
                //play music clip #2 (game over music)
                audioHandler.LoopMusic(false);
                //play death crunch sound
                audioHandler.PlaySound(0);
                //start ending music
                StartCoroutine("GameOverMusic");
                //audioHandler.PlayMusic(2);

                gameOverPanel.SetActive(true);
                
                //Time.timeScale = 0f;
                
            }
        }
    }

    // The retry method when the GameOver overlay pops up
    public void Retry()
    {
        if (gameOverMusicPlaying == true)
        {
            gameOver = false;
            gameOverMusicPlaying = false;
            audioHandler.StopMusic();
            // Set player position to initial position in basement
            if (Application.loadedLevelName == "Tutorial Stage")
            {
                if (playerScript.killedByHunter==true)
                {


                }
                else
                {
                    initialPlayerPos = new Vector3(-111, -78, player.transform.position.z);
                }
            }
           
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
            if (openCutscene != null)
            {
                openCutscene.EndCutscene();
                openCutscene.hunterEnemy.SetActive(true);
                StartCoroutine("DespawnBasementHunter");
                playerScript.hunterScript = openCutscene.hunterEnemy.GetComponent<Hunter>();
            }
            if (endCutscene != null)
            {
                endCutscene.EndCutscene();
                endCutscene.activated = false;
                endCutscene.hunterEnemy.SetActive(true);
                StartCoroutine("DespawnBasementHunter");
                playerScript.hunterScript = endCutscene.hunterEnemy.GetComponent<Hunter>();
            }
            if (playerScript.hunterScript != null)
            {
                playerScript.hunterScript.isCaught = false;
                playerScript.hunterScript.killPlayer = false;
                playerScript.hunterScript.anim.SetBool("Kill", false);
                playerScript.hunterScript.StopSpeed();//playerScript.hunterScript.defaultSpeed;
                playerScript.hunterScript.transform.position = playerScript.hunterScript.originalPosition;
                playerScript.hunterScript.transform.rotation = playerScript.hunterScript.originalRotation;
            }
            //resumes chasing monster speed
            if (playerScript.chasingMonsterScript != null)
            {
                playerScript.chasingMonsterScript.speedNormal = 2.0f;
                playerScript.chasingMonsterScript.speedChasing = 4.0f;
            }
            playerScript.killedByHunter = false;
            //play intro music
            /*
            audioHandler.LoopMusic(false);
            audioHandler.PlayMusic(3);
            */
            //play level music
            StartCoroutine("levelMusic");
        }
    }

    public void MainMenu()
    { //The main menu button when the game over screen pops up
        Time.timeScale = 1f;
        //load first level in hierarchy
        Application.LoadLevel(1);

    }
    public IEnumerator DespawnBasementHunter()
    {
        yield return new WaitForSeconds(15f);
        if (openCutscene != null)
        {
            openCutscene.hunterEnemy.SetActive(false);
        }
        if (endCutscene != null)
        {
            endCutscene.hunterEnemy.SetActive(false);
        }
       
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
       // if(Application.loadedLevelName != "Ending Level" && Application.loadedLevelName != "Nightmare")
        yield return new WaitForSeconds(waitTime);
        //play and loop level music
        audioHandler.LoopMusic(true);
        audioHandler.PlayMusic(5);
        yield return null;
    }
    public IEnumerator GameOverMusic()
    {
        StopCoroutine("LevelMusic");
        ///wait for length of intro
        yield return new WaitForSeconds(1);
        //play and loop level music
        audioHandler.LoopMusic(true);
        audioHandler.PlayMusic(2);
        gameOverMusicPlaying = true;
        Time.timeScale = 0f;
        yield return null;
    }
   
}
