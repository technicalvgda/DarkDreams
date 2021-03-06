﻿using UnityEngine;
using System.Collections;

//If the current item position is undesirable adjustments MUST be made so that the monster and silhouette
//will stop in front of the player to have a clean looking cutscene. Adjust the fadingSpeed to achieve such a result.
public class BeatGameScript : MonoBehaviour 
{
    AudioHandlerScript audioHandler;
    //Bool variable for if the cutscene has activated
    private bool cutsceneActivated;
	//The background for the white out. Drag and drop throug the inspector to set
	public CanvasGroup whiteOutImage;
	//The background for the darkness. Drag and drop throug the inspector to set
	public CanvasGroup darknessImage;
	//The distance between the monster and the ground so the silhouette is on the ground
	private Vector3 silhouetteMonsterOffset;
	//The silhouette object
	//private GameObject silhouette;
	//The spotlight for the hunter monster
	private GameObject spotlight;
	//Variable for the alpha of anything fading. Need to set to 1 after every object
	private float fadingAlpha;
	//Variable for the alpha of anything appearing. Need to set to 0 after every object
	private float appearingAlpha;
	//The script variable for the final level cutscene. Needed so the monster can stay active on the basement level
	private FinalLevelCutscene finalLevelCutscene;
	//The hunter enemy
	public GameObject hunterEnemy;
    //the hunters fog
    private GameObject hunterFog;
	//The basement
	private GameObject basementRoom;
    //The bulb
    public GameObject lightBulb;
    //this objects particle glow
    public ParticleSystem glow;
    //The player
    private GameObject player;
	//The variable to control how fast something fades
	public float fadingSpeed = 1f;
	Vector2 clickPosition;
	//CameraFollowScript cameraScript;
	float clickOffsetY = 1;
	float clickOffsetX = 1;

    bool playerContact = false;
	// Use this for initialization
	void Start () 
	{
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        glow = GameObject.Find("EndGlow").GetComponent<ParticleSystem>();
		//Set the fadingAlpha to 1 so that things will fade correctly
		fadingAlpha = 1;
		//Find the final level script
		finalLevelCutscene = GameObject.Find ("HallwayRoom1").GetComponent<FinalLevelCutscene> ();
		//Find the basement room
		basementRoom = GameObject.Find ("EndingBasement");
		//Find the silhouette
		//silhouette = GameObject.Find ("tempSilhouette");
		//Find the hunter monster
		//hunterEnemy = GameObject.FindGameObjectWithTag ("HunterEnemy");
        hunterFog = GameObject.Find("FogLeft");
		//Get the spotlight for the hunter monster
		spotlight = GameObject.Find ("hunter_spotlight_test_1");
		//Make the silhouette invisible
		//silhouette.GetComponent<Renderer> ().material.color = Color.clear;
		//Make it inactive until the ending cutscene starts
		//silhouette.SetActive (false);
		//find the player
		player = GameObject.FindGameObjectWithTag ("Player");
		clickPosition = new Vector2(0f, 0f);
	}
	
	// Update is called once per framead
	void Update () 
	{
        /*
		//If both the hunterEnemy and silhouette are both active
		if (hunterEnemy.activeSelf == true && silhouette.activeSelf == true) 
		{
			//Position the silhouette 
			silhouetteMonsterOffset = hunterEnemy.transform.position;
			silhouetteMonsterOffset.y  = hunterEnemy.transform.position.y-10;
			silhouette.transform.position = silhouetteMonsterOffset;
		}
        */
        //Needed so the player won't be able to spam the space bar to glitch the cutscene
        if (cutsceneActivated == false)
        {

            if (playerContact == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (player.GetComponent<PlayerControl>() == null)
                {
                    return;
                }
                StartCoroutine(_BeatGame());
            }
            else if (playerContact == true && Input.GetMouseButtonDown(0))
            {

                RaycastHit2D[] hits;
                hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100);
                for (int i = 0; i < hits.Length; i++)
                {

                    RaycastHit2D hit = hits[i];
                    if (hit.collider.tag == "EndObject")
                    {
                        if (player.GetComponent<PlayerControl>() == null)
                        {
                            return;
                        }
                        StartCoroutine(_BeatGame());
                    }

                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            playerContact = true;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("Player not touching");
            playerContact = false;

        }
    }
   
	//The cutscene
	IEnumerator _BeatGame()
	{
		//Make it true so the player can't spam space and glitch the cutscene
		cutsceneActivated = true;
        lightBulb.SetActive(true);
        glow.emissionRate = 0;
        glow.startLifetime = 0;

        //Make it so the hunter's linecast won't trigger it to run fast or make the player lose the game if there is a collision
        player.GetComponent<PlayerControl> ().hide = true;

		Debug.Log ("You beat the game!");
        darknessImage.alpha = 0;
        //Make it so player can't move
        player.GetComponent<PlayerControl> ().normalSpeed = 0;
		//Disable the final level cutscene script so the hunter can stay active
		finalLevelCutscene.enabled = false;
		//Activate the silhoutte
		//silhouette.SetActive (true);
		//Activate the hunter
		hunterEnemy.SetActive (true);
        player.GetComponent<Animator>().SetBool("Walking", false);
		//Correctly position the hunter
		hunterEnemy.transform.position = new Vector3 (player.transform.position.x -20, player.transform.position.y + 10, player.transform.position.z);
        //While the hunter is visible
       // 
        hunterFog.SetActive(false);
        yield return new WaitForSeconds(5f);
        hunterEnemy.GetComponent<CutsceneHunter>().StopSpeed();
        hunterEnemy.GetComponent<CutsceneHunter>().anim.SetBool("End", true);
        yield return new WaitForSeconds(1f);
        audioHandler.PlayVoice(null);
        yield return new WaitForSeconds(2f);
        hunterEnemy.SetActive(false);
      
        
        //Make the hunter inactive
       
		//Reset the fadingAlpha
		fadingAlpha = 1;
		//Wait 2 seconds
		yield return new WaitForSeconds (2f);
        //While the silhouette is visisble
        /*
		while (silhouette.GetComponent<Renderer>().material.color.a >= 0f) 
		{
			//Make things fade
			fadingAlpha -= Time.deltaTime*fadingSpeed;
			//Make the silhouette disappear
			silhouette.GetComponent<Renderer>().material.color  = new Color(1,1,1,fadingAlpha);
			//When the silhouette is partially invisible
			if(silhouette.GetComponent<Renderer>().material.color.a <= 0.40f)
			{
				//Start whiting out the screen
				whiteOutImage.alpha += Time.deltaTime*fadingSpeed;
			}

			yield return null;
		}
        */
        //Finish whiting out the screen
       
        while (whiteOutImage.alpha  < 1f) 
		{
			whiteOutImage.alpha += Time.deltaTime*fadingSpeed;
			yield return null;
		}
		//Wait 1 second
		yield return new WaitForSeconds (1f);
		//Load the credits scene
		Application.LoadLevel ("EndCreditsScene");
		yield return null;
	}
}
