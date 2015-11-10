using UnityEngine;
using System.Collections;

//This is the controller for the rooms and for the bugs
//There MUST be one instance of this object within the game
//There MUST be at least one instance of the camera bug to get the 4th wall effect
public class BugRoomController : MonoBehaviour 
{
	//The vertical extent of the camera
	private float vertExtent;
	//The horizontal extent of the camera
	private float horzExtent;
	//Variable for the 4th wall camera bug
	private GameObject CamBug;
	//Array of rooms
	private GameObject[] roomAll;
	//Array of bugs
	private GameObject[] bugArray;
	//Array of bug walls
	private GameObject[] bugWallArray;
	//The player
	private GameObject player;
	//The spawn area for the bugs
	private Bounds spawnLoc;
	//The position of the player
	private Vector3 playerPos;
	// Use this for initialization
	void Start () 
	{
		//Make the controller invisible on start up
		gameObject.GetComponent<Renderer> ().enabled = false;
		//Find the player
		player = GameObject.FindGameObjectWithTag ("Player");
		//Get the players position
		playerPos = player.transform.position;
		//Find the rooms and put them in the array
		roomAll = GameObject.FindGameObjectsWithTag("Room");
		//Find the bugs and put them in the array
		bugArray = GameObject.FindGameObjectsWithTag("Bug");
		//Find the walls and put them in the array
		bugWallArray = GameObject.FindGameObjectsWithTag ("BugWall");
		//Loop to make walls invisible during start up
		for (int i = 0; i < bugWallArray.Length; i++) 
		{
			bugWallArray[i].GetComponent<Renderer>().enabled = false;
		}
		//Loop to find the camera bug
		for (int i = 0; i < bugArray.Length; i++) 
		{
			if(bugArray[i].name.Contains("Camera"))
				CamBug = bugArray[i];
		}
		//Activate teleportBug
		teleportBug ();
	}
	
	// Update is called once per frame
	void Update () {

		//Update the player position;
		playerPos = player.transform.position;
		//Get the verticle extent of the camera every frame so it is resizable with the camera
		vertExtent = Camera.main.orthographicSize;
		//Get the horizontal extent of the camera every frame so it is resizable with the camera
		horzExtent = vertExtent * Screen.width / Screen.height;

		//Iterate through the room array
		for (int i = 0; i < roomAll.Length; i++) 
		{
			//If the room is a BugHallucinationRoom
			if (roomAll [i].name.Contains ("Bug")) 
			{
				//If the player is within the bounds of the room...
				if (roomAll [i].GetComponent<Renderer> ().bounds.Contains (playerPos)) 
				{
					//Get the bounds of the room the player is in
					spawnLoc = roomAll [i].GetComponent<Renderer>().bounds;
					//Loop through the array of bugs
					for (int j = 0; j < bugArray.Length; j++) 
					{
						//Make the bug active
						bugArray [j].SetActive (true);
						//If the bug isn't moving...
						if (bugArray[j].GetComponent<BugScript>().isMoving == false)
						{
							//Then make it move
							bugArray [j].GetComponent<BugScript> ().isMoving = true;
							//and "spawn" the bug in a random location within the spawning area
							bugArray [j].transform.position = new Vector3 (Random.Range ((spawnLoc.center.x - spawnLoc.extents.x * 0.5f),
							                                                             (spawnLoc.center.x + spawnLoc.extents.x * 0.5f)),
							                                               Random.Range ((spawnLoc.center.y - spawnLoc.extents.y * 0.5f),
							              												((spawnLoc.center.y + spawnLoc.extents.y * 0.5f))),
							                                               0);

							//Randomly "spawn" the camera bug within a spawning area on the camera view
							if(bugArray[j].name.Contains ("Camera"))
							{
								CamBug = bugArray[j];
								CamBug.transform.position = new Vector3 (Random.Range ((Camera.main.transform.position.x-horzExtent * 0.75f),
								                                                       (Camera.main.transform.position.x+horzExtent * 0.75f)),
								                                         Random.Range ((Camera.main.transform.position.y-vertExtent * 0.75f),
								              										  ((Camera.main.transform.position.y+vertExtent * 0.75f))),
								                                         0);
							}

						}
					}
				}
			}
			//If the room is NOT a BugHallucinationRoom..
			else
			{
				//And if the player is within the bounds of any room..
				if (roomAll [i].GetComponent<Renderer> ().bounds.Contains (playerPos)) 
				{
					//Deactivate the bugs
					for(int j = 0; j < bugArray.Length; j++)
					{
						bugArray[j].GetComponent<BugScript>().isMoving = false;
						bugArray[j].SetActive(false);
					}
				}
			}
		}
	}
	void LateUpdate()
	{
		//Loop through the bug array
		for (int i = 0; i < bugArray.Length; i++) 
		{
			//If the bug is a camera bug
			if(bugArray[i].name.Contains ("Camera"))
			{
				CamBug = bugArray[i];
				//and if the camera bug is moving
				if (CamBug.GetComponent<BugScript> ().isMoving) 
				{
					//and if the camera bug is outside the camera view
					if (CamBug.transform.position.x < Camera.main.transform.position.x-horzExtent||
					    CamBug.transform.position.x > Camera.main.transform.position.x+horzExtent||
					    CamBug.transform.position.y < Camera.main.transform.position.y-vertExtent||
					    CamBug.transform.position.y > Camera.main.transform.position.y+vertExtent) 
					{
						//then flip and reverse the movement of the bug
						CamBug.GetComponent<BugScript>().FlipBug();
						CamBug.GetComponent<BugScript>().touchedWall = !CamBug.GetComponent<BugScript>().touchedWall;
					}
				}
			}
		}
	}
	//Sometimes the bugs ghost through the bugwalls so this is used to take the escaped ones and
	//put them back in the active area
	void teleportBug()
	{
		//Loop through the room array
		for (int i = 0; i < roomAll.Length; i++) 
		{
			//If the room is a BugHallucinationRoom
			if (roomAll [i].name.Contains ("Bug")) 
			{
				//If the player is within the bounds of the room...
				if (roomAll [i].GetComponent<Renderer> ().bounds.Contains (playerPos)) 
				{
					//loop through the bug array
					for(int j = 0; j < bugArray.Length;j++)
					{
						//check to see if the bug is outside of the room bounds and if the bug is NOT a camera bug
						if(!roomAll[i].GetComponent<Renderer>().bounds.Contains (bugArray[j].transform.position)&& !bugArray[j].name.Contains("Camera"))
						{
							//then move it back into the active area
							bugArray [j].transform.position = new Vector3 (Random.Range ((spawnLoc.center.x - spawnLoc.extents.x * 0.5f),
							                                                             (spawnLoc.center.x + spawnLoc.extents.x * 0.5f)),
							                                               Random.Range ((spawnLoc.center.y - spawnLoc.extents.y * 0.5f),
							              												((spawnLoc.center.y + spawnLoc.extents.y * 0.5f))),
							                                              				 0);
						}
					}
				}
			}
		}
		//Call the function every however many seconds
		Invoke ("teleportBug", 1f);
	}
}