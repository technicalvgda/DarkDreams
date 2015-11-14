using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightmareTower : MonoBehaviour {
	//size of the room
	public int lengthOfRoom = 60;
	public int heightOfRoom = 27;
	
	//List of doorRoom and otherRoom
	public Transform[] doorRoom;
	public Transform[] otherRoom;

	private int noRoomsPerFloor = 4;
	private int level = 0;
	
	private Vector3 sizeOfPrefab = new Vector3(0,0,0);

	//create each hallway
	private List<Transform> bottomHallway = new List<Transform>();
	private List<Transform> topHallway = new List<Transform>();
	private GameObject player;
	private Vector2 playerPosition;
	private bool canGenerate;

	void Awake() {

		if (doorRoom.Length < 4) {
			Debug.Log("Please make sure there are at least 4 door rooms");
			return;
		}
		player = GameObject.Find ("Player");
		playerPosition = player.transform.position;
		canGenerate = false;
		initializeRooms ();


	}


	// Use this for initialization
	void Start () {

		instantiateRooms ();
		//run every 5 seconds to check if I can generate
		InvokeRepeating ("generate", 0 , 5f);
	
	}
	
	// Update is called once per frame
	void Update () {
		//check if the player has moved to the next floor to generate new floor.
		if (playerPosition.y != player.transform.position.y) {
			canGenerate = true;
			playerPosition.y = player.transform.position.y;
		}
	}

	void generate() {
		Debug.Log ("Call this");
		if (canGenerate) {
			Debug.Log ("YES I CAN GENERATE");
			canGenerate = false;
		}


	}

	void instantiateRooms() {

		for(int i = 0; i < noRoomsPerFloor; i++) {
			Instantiate(bottomHallway[i], transform.position + sizeOfPrefab , transform.rotation);
			sizeOfPrefab.x += lengthOfRoom;
			Debug.Log (bottomHallway[i]);
		}

		level++;
		sizeOfPrefab.x = 0;

		//Size/Gap between each level (height of rooms)
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y *= level;
		
		for (int i = 0; i < noRoomsPerFloor; i++) {
			Instantiate(topHallway[i], transform.position + sizeOfPrefab , transform.rotation);
			sizeOfPrefab.x += lengthOfRoom;
			Debug.Log (topHallway[i]);
		}

		string s1 = bottomHallway[bottomHallway.Count-1].name;
		string s2 = topHallway[0].name;
		string[] doorNumber1 = s1.Split(' ');
		string[] doorNumber2 = s2.Split(' ');


		Transform bottomDoor = GameObject.Find("Door" + doorNumber1[1]).transform;
		bottomDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;
		
		Transform topDoor = GameObject.Find("Door" + doorNumber2[1]).transform;
		topDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;

		s1 = bottomHallway [0].name;
		s2 = topHallway [topHallway.Count - 1].name;
		doorNumber1 = s1.Split(' ');
		doorNumber2 = s2.Split(' ');
		Debug.Log ("doorNumber1: " + doorNumber1[1]);
		Debug.Log ("doorNumber2: " + doorNumber2[1]);
		Transform firstDoor = GameObject.Find("Door" + doorNumber1[1]).transform;
		firstDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber1[1]).transform;
		
		Transform lastDoor = GameObject.Find("Door" + doorNumber2[1]).transform;
		lastDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;



		bottomHallway.Clear ();
		topHallway.Clear ();
		//initializeRooms ();
	}




	void initializeRooms()
	{
		//r to keep track of the room used
		int r = 0;
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
		Debug.Log ("r : " + r);
		//set door room to be the first room on each floor
		bottomHallway.Add (doorRoom [r]);
		r++;
		topHallway.Add (doorRoom [r]);
		r++;
		Debug.Log ("r after: " + r);
		for (int i = 0; i < noRoomsPerFloor-2;i++) {
			int randomRoom = Random.Range (0, otherRoom.Length);
			bottomHallway.Add(otherRoom[i]);
			topHallway.Add(otherRoom[randomRoom]);
		}
		//set door room to be the last room on each floor
		bottomHallway.Add (doorRoom [r]);
		r++;
		topHallway.Add(doorRoom[r]);
		r++;
	}


}
