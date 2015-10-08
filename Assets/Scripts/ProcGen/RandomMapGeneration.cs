using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomMapGeneration : MonoBehaviour {

	//Size of the map
	//each hallway is x
	//each level is y
	public int sizeOfMapX = 1;
	public int sizeOfMapY = 1;

	public Transform[] doorRoom;
	public Transform[] regRoom;

	private Stack<Transform> doorRoomUnvisited = new Stack<Transform>();
	private Stack<Transform> regRoomUnvisited = new Stack<Transform>();

	//DoorRoomEachFloor
	private List<int> doorRoomEachFloor = new List<int>(2);
	//number of door per floor is 2
	private const int NUMBER_OF_DOOR_PER_FLOOR = 2;

	//linkedDoor to keep track of the door to be connected on each floor
	private List<int> linkedDoor = new List<int>();

	//level is needed to keep track of each floor.
	private int level = 0;
	

	// Use this for initialization
	void Awake() {
		initializeRooms ();
		instantiateRooms ();

	}
	
	//shuffle door and reg room.
	void initializeRooms() {
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
		for (int i = 0; i < regRoom.Length; i++)
		{
			int idx = Random.Range(i, regRoom.Length);
			//swap elements
			Transform tmp = regRoom[i];
			regRoom[i] = regRoom[idx];
			regRoom[idx] = tmp;
		} 
		for (int i = 0; i < doorRoom.Length; i++) {
			doorRoomUnvisited.Push (doorRoom [i]);
		}
		for (int i = 0; i < regRoom.Length; i++) {
			regRoomUnvisited.Push (regRoom[i]);
		}
		if (doorRoom.Length < sizeOfMapX* NUMBER_OF_DOOR_PER_FLOOR) {
			Debug.Log ("Door rooms size should be more than " + sizeOfMapX* NUMBER_OF_DOOR_PER_FLOOR);
		}
		if (regRoom.Length < (sizeOfMapX * sizeOfMapY) - (sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR)) {
			Debug.Log ("Regular rooms size should be more than " + ((sizeOfMapX * sizeOfMapY) - (sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR)));
		}
	}

	//Generate randomDoorRoom
	void generateRandomDoor() {
		int randomDoor = 0;
		int numberOfDoorEachFloor = 0;
		while (doorRoomEachFloor.Count < 2) {    
			randomDoor = Random.Range (0, sizeOfMapX);
			if (!doorRoomEachFloor.Contains (randomDoor)) {
				doorRoomEachFloor.Add (randomDoor);

				//Add door to linkedDoor to link each door only 2 doors are connected
				linkedDoor.Add (randomDoor);

				numberOfDoorEachFloor++;
			}
		}
	}

	//set linkedDoorPrefab
	void setLinkedDoorPrefab () {
		//ignore the first and the last door. linkedDoor[0] and linkedDoor[linkedDoor.Count-1]
		//set linkedDoor[1] to connect to linkedDoor[2] and linkedDoor [2] to linkedDoor[1];
		//set linkedDoor[3] to connect to linkedDoor[4] and linkedDoor[4] to linkedDoor[3];
		//Use Transform door1 = GameObject.Find("Door1"*).transform; for each doorRoom on map
		//door1.GetComponent<TeleportDoors().exit = doorRoom[0]*;
		//Note: * means the door can be any door from the prefab.
	
	}


	//Assume that each door and regular room has the same s	ize.

	void instantiateRooms() {

		Vector3 sizeOfPrefab = new Vector3 (0,0,0);
		for (int j = 0; j < sizeOfMapY; j++) {
			generateRandomDoor ();
			for (int i = 0; i < sizeOfMapX; i++) {
				if (doorRoomEachFloor.Contains (i)) {
					//Get each room out of the stack doorRoomUnvisited
					Transform doorR = doorRoomUnvisited.Pop ();
					Instantiate (doorR, transform.position + sizeOfPrefab, transform.rotation);
					sizeOfPrefab.x += doorRoom [i].localScale.x + doorRoom [i].localScale.x + 
						doorRoom [i].localScale.x + doorRoom [i].transform.right.x;
				} else {
					Transform regR = regRoomUnvisited.Pop ();
					Instantiate (regR, transform.position + sizeOfPrefab, transform.rotation);
					sizeOfPrefab.x += doorRoom [i].localScale.x + doorRoom [i].localScale.x + 
						doorRoom [i].localScale.x + doorRoom [i].transform.right.x;
				}
			}
			//Clear the doorRoomEachFloor
			doorRoomEachFloor.Clear ();
			sizeOfPrefab.x = 0;
			sizeOfPrefab.y = 0;
			sizeOfPrefab.z = 0;
			level++;
			sizeOfPrefab.y += doorRoom [j].localScale.y + doorRoom [j].localScale.y + doorRoom [j].localScale.y + 
				doorRoom [j].transform.up.y;
			sizeOfPrefab.y *= level;
		}

	}
	

}
