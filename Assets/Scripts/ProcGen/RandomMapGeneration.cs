using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomMapGeneration : MonoBehaviour {

	public int sizeOfMapX = 1;
	public int sizeOfMapY = 1;

	public Transform[] doorRoom;
	public Transform[] regRoom;

	private Stack<Transform> doorRoomUnvisited = new Stack<Transform>();
	private Stack<Transform> regRoomUnvisited = new Stack<Transform>();

	//DoorRoomEachFloor
	private List<int> doorRoomEachFloor = new List<int>(2);

	private const int NUMBER_OF_DOOR_PER_FLOOR = 2;
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
		int numberOfLinkedDoor = 0;
		while (doorRoomEachFloor.Count < 2) {    
			randomDoor = Random.Range (0, sizeOfMapX);
			if (!doorRoomEachFloor.Contains (randomDoor)) {
				doorRoomEachFloor.Add (randomDoor);
				numberOfLinkedDoor++;
			}
		}
	}

	//Assume that each door and regular room has the same size.

	void instantiateRooms() {
		//level is needed to keep track of each floor.
		int level = 0;
		Vector3 sizeOfPrefab = new Vector3 (0,0,0);
		for (int j = 0; j < sizeOfMapY; j++) {
			generateRandomDoor ();
			for (int i = 0; i < sizeOfMapX; i++) {
				if (doorRoomEachFloor.Contains (i)) {
					//Get each room out of the stack
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
			//Clear the linkedDoor
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
