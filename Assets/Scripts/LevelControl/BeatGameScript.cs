using UnityEngine;
using System.Collections;

//If the current item position is undesirable adjustments MUST be made so that the monster and silhouette
//will stop in front of the player to have a clean looking cutscene. Adjust the fadingSpeed to achieve such a result.
public class BeatGameScript : MonoBehaviour 
{
	//Bool variable for if the cutscene has activated
	private bool cutsceneActivated;
	//The background for the white out. Drag and drop throug the inspector to set
	public CanvasGroup whiteOutImage;
	//The background for the darkness. Drag and drop throug the inspector to set
	public CanvasGroup darknessImage;
	//The distance between the monster and the ground so the silhouette is on the ground
	private Vector3 silhouetteMonsterOffset;
	//The silhouette object
	private GameObject silhouette;
	//The spotlight for the hunter monster
	private GameObject spotlight;
	//Variable for the alpha of anything fading. Need to set to 1 after every object
	private float fadingAlpha;
	//Variable for the alpha of anything appearing. Need to set to 0 after every object
	private float appearingAlpha;
	//The script variable for the final level cutscene. Needed so the monster can stay active on the basement level
	private FinalLevelCutscene finalLevelCutscene;
	//The hunter enemy
	private GameObject hunterEnemy;
	//The basement
	private GameObject basementRoom;
	//The player
	private GameObject player;
	//The variable to control how fast something fades
	public float fadingSpeed = 1f;
	Vector2 clickPosition;
	//CameraFollowScript cameraScript;
	float clickOffsetY = 1;
	float clickOffsetX = 1;
	// Use this for initialization
	void Start () 
	{
		//Set the fadingAlpha to 1 so that things will fade correctly
		fadingAlpha = 1;
		//Find the final level script
		finalLevelCutscene = GameObject.Find ("HallwayRoom1").GetComponent<FinalLevelCutscene> ();
		//Find the basement room
		basementRoom = GameObject.Find ("EndingBasement");
		//Find the silhouette
		silhouette = GameObject.Find ("tempSilhouette");
		//Find the hunter monster
		hunterEnemy = GameObject.FindGameObjectWithTag ("HunterEnemy");
		//Get the spotlight for the hunter monster
		spotlight = GameObject.Find ("hunter_spotlight_test_1");
		//Make the silhouette invisible
		silhouette.GetComponent<Renderer> ().material.color = Color.clear;
		//Make it inactive until the ending cutscene starts
		silhouette.SetActive (false);
		//find the player
		player = GameObject.FindGameObjectWithTag ("Player");
		clickPosition = new Vector2(0f, 0f);
	}
	
	// Update is called once per framead
	void Update () 
	{
		//If both the hunterEnemy and silhouette are both active
		if (hunterEnemy.activeSelf == true && silhouette.activeSelf == true) 
		{
			//Position the silhouette 
			silhouetteMonsterOffset = hunterEnemy.transform.position;
			silhouetteMonsterOffset.y  = hunterEnemy.transform.position.y-10;
			silhouette.transform.position = silhouetteMonsterOffset;
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		
		float xNegPosition = transform.position.x - clickOffsetX;
		float xPosPosition = transform.position.x + clickOffsetX;
		float yPosPosition = transform.position.y + clickOffsetY;
		float yNegPosition = transform.position.y - clickOffsetY;
		
		///get position of click
		clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		//Needed so the player won't be able to spam the space bar to glitch the cutscene
		if (cutsceneActivated == false) 
		{
			if (Input.GetKeyDown(KeyCode.Space) || ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
			                                        (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) && Input.GetMouseButtonDown(0)))
			{
				if (other.GetComponent<PlayerControl>() == null)
				{
					return;
				}
				StartCoroutine(_BeatGame ());
			}
		}

	}
	//The cutscene
	IEnumerator _BeatGame()
	{
		//Make it true so the player can't spam space and glitch the cutscene
		cutsceneActivated = true;		
		//Make it so the hunter's linecast won't trigger it to run fast or make the player lose the game if there is a collision
		player.GetComponent<PlayerControl> ().hide = true;

		Debug.Log ("You beat the game!");
		//Make it so player can't move
		player.GetComponent<PlayerControl> ().normalSpeed = 0;
		//Disable the final level cutscene script so the hunter can stay active
		finalLevelCutscene.enabled = false;
		//Activate the silhoutte
		silhouette.SetActive (true);
		//Activate the hunter
		hunterEnemy.SetActive (true);
		//Correctly position the hunter
		hunterEnemy.transform.position = new Vector3 (player.transform.position.x -40, player.transform.position.y + 10, player.transform.position.z);
		//While the hunter is visible
		while (hunterEnemy.GetComponent<Renderer>().material.color.a >= 0f) 
		{
			//If the hunter is in the basement since it spawns offscreen
			if(basementRoom.GetComponent<Renderer>().bounds.Contains (hunterEnemy.transform.position))
			{
				//Make the room brighter
				darknessImage.alpha -= Time.deltaTime * fadingSpeed;
				//Make things fade
				fadingAlpha  -= Time.deltaTime * fadingSpeed;
				//Make things appear
				appearingAlpha += Time.deltaTime*fadingSpeed;
				//Make the silhouette appear
				silhouette.GetComponent<Renderer>().material.color  = new Color(1,1,1,appearingAlpha);
				//Make the hunter disappear
				hunterEnemy.GetComponent<Renderer>().material.color = new Color(1,1,1,fadingAlpha);
				//Make the spotlight disappear
				spotlight.GetComponent<Renderer>().material.color = new Color(1,1,1,fadingAlpha);
			}
			yield return null;
		}
		//Make the hunter inactive
		hunterEnemy.SetActive (false);
		//Reset the fadingAlpha
		fadingAlpha = 1;
		//Wait 2 seconds
		yield return new WaitForSeconds (2f);
		//While the silhouette is visisble
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
