using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalLevelCutscene : MonoBehaviour
{
	private GameObject door;
	private bool activated;
	private Bounds levelBounds;
	private float minY;
	private float maxY;
	private GameObject hunterEnemy;
	private GameObject wall;
	private float wallMargin;
	private const float wallOffset = 20;	
	private GameObject player;
	private PlayerControl playerScript;
	private GameObject cam;
	private PauseScript pause;
	//Temporary variable to hold the value of the default speed
	private float defaultSpeedTemp;
	// Use this for initialization
	void Start ()
	{
		door = GameObject.Find ("DoorLeft");
		activated = false;
		wall = GameObject.Find ("LeftWall");
		hunterEnemy = GameObject.FindGameObjectWithTag ("HunterEnemy");
		hunterEnemy.SetActive (false);
		levelBounds = this.transform.GetComponent<Renderer>().bounds;
		minY = levelBounds.center.y - levelBounds.extents.y;
		maxY = levelBounds.center.y + levelBounds.extents.y;
		player = GameObject.FindGameObjectWithTag ("Player");
		cam = Camera.main.gameObject;
		pause = Camera.main.GetComponent<PauseScript>();
		pause.busy = true;
		//player.transform.position = new Vector3 (128, player.transform.position.y, 0);
		playerScript = player.GetComponent<PlayerControl>();
		wallMargin = wall.transform.position.x + wallOffset;	
		defaultSpeedTemp = playerScript.defaultSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player.transform.position.y > minY && player.transform.position.y < maxY) {
			//Needed because the player was able to spam A and D to "break" out of the cutscene
			//and move around while the cutscene is activating.
			if (activated == false) {
				playerScript.defaultSpeed = 0;
				playerScript.normalSpeed = 0;
			}
			//Make the door unuseabe by the player when the player enters the long hall
			door.GetComponent<TeleportDoors> ().exit = null;
			//If the cutscene hasn't been activated yet
			if (activated == false) {
				//Deactivate the door, set activated to true and start cutscene
				if (Camera.main.transform.position.x == player.transform.position.x) {					
					activated = true;
					StartCoroutine (_Cutscene ());
				}
			}			
		} 
		else 
		{
			hunterEnemy.SetActive(false);
		}
	}
	//The cutsccene
	IEnumerator _Cutscene()
	{
		playerScript.normalSpeed = 0f;
		yield return new WaitForSeconds (0.5f);
		
		// Lock the camera once it finishes positioning itself
		cam.GetComponent<CameraFollowScript> ().enabled = false;
		
		yield return new WaitForSeconds (1);
		
		// Pan camera to left until it hits the wall
		while (cam.transform.position.x > wallMargin)
		{
			cam.transform.position += new Vector3(-0.2f, 0, 0);
			yield return null;
		}
		
		yield return new WaitForSeconds (1);
		
		// move the hunter
		hunterEnemy.SetActive (true);
		
		yield return new WaitForSeconds (0.5f);
		
		// pan back to player
		while (cam.transform.position.x < player.transform.position.x)
		{
			cam.transform.position += new Vector3(0.4f, 0, 0);
			yield return null;
		}
		cam.GetComponent<CameraFollowScript> ().enabled = true;
		//prevent player from moving until end of cutscene
		playerScript.defaultSpeed = defaultSpeedTemp;
		playerScript.normalSpeed = playerScript.defaultSpeed;
		pause.busy = false;
		//doorScript.enabled = true;
		yield return null;
	}
}
