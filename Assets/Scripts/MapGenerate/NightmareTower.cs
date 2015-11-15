using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightmareTower : MonoBehaviour {
	
	public const int NUMBER_OF_DOOR_PER_FLOOR = 2;
	//size of the room
	public int lengthOfRoom = 60;
	public int heightOfRoom = 27;
	
	//List of doorRoom and otherRoom
	public Transform[] doorRoom;
	public Transform[] otherRoom;
	
	public int noRoomsPerFloor = 4;
	
	
	private int level = 0;
	
	private Vector3 sizeOfPrefab = new Vector3(0,0,0);
	
	//create each hallway
	private List<Transform> bottomHallway = new List<Transform>();
	private List<Transform> topHallway = new List<Transform>();
	
	//Get the the door to trigger the generate method
	private GameObject doorToConnect;
	private Vector2 playerPosition;
	
	//Variables to check for generation
	private bool canGenerate;
	private bool generated = false;
	
	//Last door on the map.
	private string lastDoorOnMap = "";
	private GameObject doorToBeTriggered;
	
	
	
	void Awake() {
		
		if (doorRoom.Length < 4) {
			Debug.Log("Please make sure there are at least 4 door rooms");
			return;
		}
		
		canGenerate = false;
		initializeRooms ();
		
	}
	
	
	// Use this for initialization
	void Start () {
		instantiateRooms ();
		InvokeRepeating ("generate", noRoomsPerFloor-1, noRoomsPerFloor-1);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (doorToBeTriggered.GetComponent<TeleportDoors> ().used) {
			canGenerate = true;
			doorToBeTriggered.GetComponent<TeleportDoors>().used = false;
		}
	}
	
	void generate() {
		Debug.Log ("check if i can generate");
		if (canGenerate) {
			Debug.Log ("YES I CAN GENERATE");
			canGenerate = false;
			generated = true;
			randomizeRooms();
			while (doorRoom[0].name.Equals(lastDoorOnMap) && doorRoom[1].name.Equals(lastDoorOnMap)) {
				randomizeRooms();
			}

			Invoke("destroyHallway", noRoomsPerFloor);
			
		}
	}
	
	void instantiateRooms() {
		for(int i = 0; i < noRoomsPerFloor; i++) {
			Instantiate(bottomHallway[i], transform.position + sizeOfPrefab , transform.rotation);
			sizeOfPrefab.x += lengthOfRoom;
			Debug.Log ("ROOM ADDED TO INSTANTIATEDHALL: " + bottomHallway[i].name);
		}
		
		level++;
		sizeOfPrefab.x = 0;
		
		//Size/Gap between each level (height of rooms)
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y += level;
		
		for (int i = 0; i < noRoomsPerFloor; i++) {
			Instantiate(topHallway[i], transform.position + sizeOfPrefab , transform.rotation);
			sizeOfPrefab.x += lengthOfRoom;
			Debug.Log (topHallway[i]);
		}
		
		level++;
		sizeOfPrefab.x = 0;
		//Size/Gap between each level (height of rooms
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y += level;
		
		string s1 = bottomHallway[bottomHallway.Count-1].name;
		string s2 = topHallway[0].name;
		string[] doorNumber1 = s1.Split(' ');
		string[] doorNumber2 = s2.Split(' ');
		
		Transform bottomDoor = GameObject.Find("Door" + doorNumber1[1]).transform;
		bottomDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;
		
		
		
		Transform topDoor = GameObject.Find("Door" + doorNumber2[1]).transform;
		topDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;
		Destroy (topDoor.GetComponent ("TeleportDoors"));
		
		//Set Trigger door for generation
		doorToBeTriggered = GameObject.Find ("Door" + doorNumber1[1]);
		Debug.Log ("Door to be Triggered: " + doorToBeTriggered.name);
		s1 = bottomHallway [0].name;
		s2 = topHallway [topHallway.Count - 1].name;
		doorNumber1 = s1.Split(' ');
		doorNumber2 = s2.Split(' ');
		Transform firstDoor = GameObject.Find("Door" + doorNumber1[1]).transform;
		Destroy(firstDoor.GetComponent("TeleportDoors"));
		
		Transform lastDoor = GameObject.Find("Door" + doorNumber2[1]).transform;
		lastDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;
		
		lastDoorOnMap = s2;
		
		bottomHallway.Clear ();
		topHallway.Clear ();
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
		randomizeRooms ();
		
		//r to keep track of the room used
		int r = 0;
		Debug.Log ("r : " + r);
		
		
		
		//set door room to be the first room on each floor
		bottomHallway.Add (doorRoom [r]);
		r++;
		topHallway.Add (doorRoom [r]);
		r++;
		Debug.Log ("r after: " + r);
		for (int i = 0; i < noRoomsPerFloor-NUMBER_OF_DOOR_PER_FLOOR; i++) {
			int randomRoom = Random.Range (0, otherRoom.Length);
			bottomHallway.Add (otherRoom [i]);
			topHallway.Add (otherRoom [randomRoom]);
		}
		//set door room to be the last room on each floor
		bottomHallway.Add (doorRoom [r]);
		r++;
		topHallway.Add (doorRoom [r]);
		r++;
	}
	
	void destroyHallway() {
		if (generated) {
			Debug.Log ("DESTROY HALLWAY!!!");
			
			for (int i = 0; i < 4; i++) {
				//Transform destroyObject = GameObject.Find (instantiatedHall[i]+"(Clone)").transform;
				//Debug.Log ("THIS IS TO BE DESTROYED: " + destroyObject.name);
				Debug.Log ("This is going t obe destroyed: " + "(Clone)");

			}
			
		}
		
	}
	
}
