using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightmareTower : MonoBehaviour {
	
	public const int NUMBER_OF_DOOR_PER_FLOOR = 2;

	//use this for number of rooms on each floor
	private const int ROOMS_PER_FLOOR = 4;

	//size of the room
	public int lengthOfRoom = 60;
	public int heightOfRoom = 27;
	
	//List of doorRoom and otherRoom
	Transform[] doorRoom;
	Transform[] otherRoom;


	
	//To keep track of what level is on each floor
	private int level = 0;
	
	//To change position of instantiated hallway
	private Vector3 sizeOfPrefab = new Vector3(0,0,0);
	
	//Variables to check for generation and destroy
	private bool canGenerate;
	private bool generated = false;
	
	//List of hallways
	private List<Transform> evenHallway = new List<Transform>();
	private List<Transform> oddHallway = new List<Transform> ();

	//Queue of hallways to be removed
	private Queue<Transform> hallToRemoveE = new Queue<Transform>();
	private Queue<Transform> hallToRemoveO = new Queue<Transform>();

	//Door triggered
	private GameObject doorToBeTriggered;

    //score tracker
    public GameObject ScoreText;
    HighScore ScoreScript;

	void Awake() {
        //unload any assets not being used
        Resources.UnloadUnusedAssets();

        //initialize reference to score
        ScoreScript = ScoreText.GetComponent<HighScore>();
  

        //initialize door room array
        doorRoom = Resources.LoadAll<Transform>("DoorRooms");
        if (doorRoom.Length < ROOMS_PER_FLOOR) {
			Debug.Log("Please make sure there are at least 4 door rooms");
			return;
		}
		canGenerate = false;
		initializeRooms ();
		
	}
	
	// Use this for initialization
	void Start () {
        
        instantiateRoomsAtStart();
		//Invoke repeat to generate hallway if needed
		InvokeRepeating ("generate", ROOMS_PER_FLOOR-1, ROOMS_PER_FLOOR-1);        
	}

	// Update is called once per frame
	void Update () {
        
        //use this to set the door to be triggered
        if (doorToBeTriggered != null) {
			if (doorToBeTriggered.GetComponent<TeleportDoors> ().used) {
                //Able to generate once the player enters a room
                ScoreScript.currentScore += 1;
                canGenerate = true;
				generated = true;
				doorToBeTriggered.GetComponent<TeleportDoors> ().used = false;
			}
		}
	}

	//Call when generate
	void generate() {
		//Check if generation is ok.
		if (canGenerate) {
			if (level % 2 == 0) {
				canGenerate = false;
				initializeRooms();
				generateEven();
				oddHallway.Clear ();
				connectEven ();

			}
			else if (level % 2 == 1) {
				canGenerate = false;
				initializeRooms();
				generateOdd ();
				evenHallway.Clear ();
				connectOdd();
			}
			//Destroy hallway that is not used.
			Invoke("destroyHallway", ROOMS_PER_FLOOR);
		}
	}

	//Spawn the rooms at the beginning
	void instantiateRoomsAtStart() {
        
       
        generateEven ();
		generateOdd ();
		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Remove the script of the first rooms 2 for even floor 1 for odd floor
			if (gameObj.name == "Door To Be Removed E" + (level - 2) || gameObj.name == "Door To Be Removed O" + (level - 1)) {
				Destroy (gameObj.GetComponent ("TeleportDoors"));
			}
			//Set exit for Even Floor
			else if (gameObj.name == "Door To Be Connected E" + (level - 2)) {
				doorToBeTriggered = gameObj;
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Removed O" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
			}
		}

		//clear hallway after used
		evenHallway.Clear ();
	
		foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Set exit for Odd Floor
		 	if (gameObj.name == "Door To Be Connected O" + (level-1)) {
				foreach(GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if(door.name == "Door To Be Connected E" + (level-2)) {
						gameObj.GetComponent<TeleportDoors>().exit = door.transform;
					}
				}
			}
		}

		//clear hallway after used
		oddHallway.Clear ();

	
	}

	//generateEvenFloor
	void generateEven() {
		for(int i = 0; i < ROOMS_PER_FLOOR; i++) {
			Transform obj = (Transform)(Instantiate(evenHallway[i], transform.position + sizeOfPrefab , transform.rotation));
			sizeOfPrefab.x += lengthOfRoom;
			obj.name = "New Room E " + i;
			if (i == 0) {
				obj.name = "Room to Remove E";
				string s1 = evenHallway [0].name;
				string[] doorFirstBot = s1.Split (' ');
				obj.Find ("Door" + doorFirstBot[1]).name = "Door To Be Removed E" + level;
				
			}
			if (i == ROOMS_PER_FLOOR-1) {
				string s2 = evenHallway[evenHallway.Count-1].name;
				string[] doorLastBot = s2.Split(' ');
				obj.Find ("Door" + doorLastBot[1]).name = "Door To Be Connected E" + level;
			}

			//Waiting to be removed
			hallToRemoveE.Enqueue(obj);
		}

		//Change level
		level++;
		sizeOfPrefab.x = 0;
		
		//Size/Gap between each level (height of rooms)
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y += level;
	}
	
	//Connect generateEven
	void connectEven() {;
		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Remove the script of the first rooms 2 for even floor 1 for odd floor
			if (gameObj.name == "Door To Be Removed E" + (level-1) || gameObj.name == "Door To Be Removed O" + (level - 2)) {
				Destroy (gameObj.GetComponent ("TeleportDoors"));
			}
			//Set exit for Even Floor
			else if (gameObj.name == "Door To Be Connected O" + (level-2)) {
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Removed E" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
				doorToBeTriggered = gameObj;
			}
			else if (gameObj.name == "Door To Be Connected E" + (level)) {

				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Removed O" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
			}
			//Set the last door to the same spot to avoid error
			else if (gameObj.name == "Door To Be Connected E" + (level-1)) {
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Connected E" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
			}
		}
	}

	//Connect generateOdd
	void connectOdd() {
		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Remove the script of the first rooms 2 for even floor 1 for odd floor
			if (gameObj.name == "Door To Be Removed O" + (level-1) || gameObj.name == "Door To Be Removed E" + (level - 2)) {
				Destroy (gameObj.GetComponent ("TeleportDoors"));
			}
			//Set exit for Even Floor
			else if (gameObj.name == "Door To Be Connected E" + (level-2)) {
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Removed O" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
				doorToBeTriggered = gameObj;
			}
			else if (gameObj.name == "Door To Be Connected O" + (level)) {
				
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Removed E" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
			}
			//Set the last door to the same spot to avoid error
			else if (gameObj.name == "Door To Be Connected O" + (level-1)) {
				foreach (GameObject door in GameObject.FindObjectsOfType<GameObject>()) {
					if (door.name == "Door To Be Connected O" + (level - 1)) {
						gameObj.GetComponent<TeleportDoors> ().exit = door.transform;
					}
				}
			}
		} //end outer foreach
	}

	void generateOdd() {
		//Spawn Odd
		for (int i = 0; i < ROOMS_PER_FLOOR; i++) {
			Transform obj = (Transform)(Instantiate(oddHallway[i], transform.position + sizeOfPrefab , transform.rotation));
			sizeOfPrefab.x += lengthOfRoom;
			obj.name = "New Room O " + i;
			if (i == 0) {
				obj.name = "Room to Remove O";
				string s3 = oddHallway[0].name;
				string[] doorFirstTop = s3.Split(' ');
				obj.Find ("Door" + doorFirstTop[1]).name = "Door To Be Removed O" + level;  

			}
			if (i == ROOMS_PER_FLOOR-1) {
				string s4 = oddHallway[oddHallway.Count-1].name;
				string[] doorLastTop = s4.Split(' ');
				obj.Find ("Door" + doorLastTop[1]).name = "Door To Be Connected O" + level;  
			}
			//Waiting to be removed
			hallToRemoveO.Enqueue(obj);
		}
		
		//Change level
		level++;
		sizeOfPrefab.x = 0;
		
		//Size/Gap between each level (height of rooms)
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y += level;
	}
	
	
	void randomizeRooms() {
		//shuffle doorRooms
		for (int i = 0; i < doorRoom.Length; i++)
		{
			int idx = Random.Range(i, doorRoom.Length);
			//swap elements
			Transform tmp = doorRoom[i];
			doorRoom[i] = doorRoom[idx];
			doorRoom[idx] = tmp;
		}
		//shuffle regRoom
		for (int i = 0; i < otherRoom.Length; i++)
		{
			int idx = Random.Range(i, otherRoom.Length);
			//swap elements
			Transform tmp = otherRoom[i];
			otherRoom[i] = otherRoom[idx];
			otherRoom[idx] = tmp;
		}
	}
	
	void initializeRooms()
	{
        //initialize monster room array
        otherRoom = Resources.LoadAll<Transform>("MonsterRooms");
        randomizeRooms ();	
		//r to keep track of the room used
		int r = 0;
		//set door room to be the first room on each floor
		evenHallway.Add (doorRoom [r]);
		r++;
		oddHallway.Add (doorRoom [r]);
		r++;
		for (int i = 0; i < ROOMS_PER_FLOOR-NUMBER_OF_DOOR_PER_FLOOR; i++) {
			int randomRoom = Random.Range (0, otherRoom.Length);
			evenHallway.Add (otherRoom [i]);
			oddHallway.Add (otherRoom [randomRoom]);
		}
		//set door room to be the last room on each floor
		evenHallway.Add (doorRoom [r]);
		r++;
		oddHallway.Add (doorRoom [r]);
		r++;
	}
	void destroyHallway() {
		if (generated) {
        
            if (hallToRemoveE.Count != 0 && level % 2 == 1) {
				for (int i = 0; i < ROOMS_PER_FLOOR;i++) {
					Destroy(hallToRemoveE.Dequeue().transform.gameObject);
				}
			}
			else if (hallToRemoveO.Count !=0 && level % 2 == 0 ) {
				for (int i = 0; i < ROOMS_PER_FLOOR;i++) {
					Destroy(hallToRemoveO.Dequeue().transform.gameObject);
				}
			}
			//Unload assets
			Resources.UnloadUnusedAssets() ;
		}
	}
	
}