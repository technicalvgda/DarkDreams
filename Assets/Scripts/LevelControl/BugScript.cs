using UnityEngine;
using System.Collections;

//This is the script for the behavior of the bug
//There MUST be instances of the bug for the hallucination room to take affect
//20+ instances of the bugs make for better hallucination effects 
//However too many will decrease performance of the game
public class BugScript : MonoBehaviour 
{
	//The array of bugs
	private GameObject[] bugArray;
	//Variable for the camera bug
	private GameObject CamBug;
	//Position of the bug
	private Transform bugPos;
	//Position of the player
	private Transform playerPos;
	//The vector location the bug will rotate to
	private Vector3 rotationVector;
	//Temporary storage for the new rotation
	private Vector3 newRotation;
	//The target position of a unit circle
	private Vector3 targetPos;
	//The time that the bug will change direction, randomly set between timeMin and timeMax
	private float changeDirectionTime;
	//The active timer
	private float timer;
	//Movement variable for the bug
	private float movement;
	//Bool value for when the bug touches a wall it will reverse
	public bool touchedWall;
	//The speed of the bug
	public float speed = 8;
	//The minimum time for the bug to rotate directions
	public float timeMin = 2;
	//The maxmimum time for the bug to rotate directions
	public float timeMax = 3;
	//Bool variable for if the bug is moving
	public bool isMoving = false;
	// Use this for initialization
	void Start () 
	{
		//Get the position of the bug
		bugPos = gameObject.GetComponent<Transform> ();
		//Get position of the player
		playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		//Get the array of bugs
		bugArray = GameObject.FindGameObjectsWithTag("Bug");
		//Set change direction and timer so that the bug spawns in a random rotation
		changeDirectionTime = Random.Range (timeMin,timeMax);
		timer = changeDirectionTime - 0.01f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Loop through the bug array
		for (int i = 0; i < bugArray.Length; i++) 
		{
			//If the bug is a camera bug
			if (bugArray [i].name.Contains ("Camera")) 
			{
				CamBug = bugArray [i];
				//and if the camera bug is moving
				if (CamBug.GetComponent<BugScript> ().isMoving) 
				{					
					//Ignore the collision of the bug and the camera bug
					Physics2D.IgnoreCollision (bugPos.GetComponent<Collider2D> (), 
					                           CamBug.transform.GetComponent<Collider2D>(),
					                           true);
				}
			}
		}
		//Ignore the collision of the bug and the player
		Physics2D.IgnoreCollision (bugPos.GetComponent<Collider2D> (), 
		                           playerPos.GetComponent<Collider2D> (), true);
		//Randomly set the time for the bug to change direction between timeMin and timeMax
		changeDirectionTime = Random.Range (timeMin,timeMax);
		//Increment the timer
		timer += Time.deltaTime;
		//variable to make the bug move
		movement = speed * Time.deltaTime;
		//If the timer has reached the time to change direction
		if (timer > changeDirectionTime) 
		{
			//Get random location on a unit sphere
			targetPos = Random.onUnitSphere;
			//multiply the location to make it a farther location
			targetPos *= 10;
			//temporary storage of the unit sphere location
			newRotation = targetPos;
			//Set z to 0
			targetPos.z = 0f;
			//Make the vector for the bug to rotate
			rotationVector = Vector3.RotateTowards(bugPos.localPosition, newRotation, 360, 5);
			//set x and y to 0 since rotation is done on z axis
			rotationVector.x = 0f;
			rotationVector.y = 0f;
			//rotate the bug
			bugPos.Rotate(rotationVector);
			//reset the timer
			timer -= changeDirectionTime;
		}
		//Make the bug reverse direction if it touches a wall
		if (touchedWall == false) 
		{
			transform.Translate (transform.right * movement, Space.World);
		}
		else
			transform.Translate (transform.right * -movement, Space.World);
	}
	//Function to flip the bug
	public void FlipBug()
	{
		//facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	//detects collision between the bug and the bug wall
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "BugWall") {			
			touchedWall = !touchedWall;
			FlipBug ();
		}
	}
}
