using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomMapGeneration : MonoBehaviour {
	public int sizeOfMapX = 1;
	public int sizeOfMapY = 1;

	public Transform[] doorRoom;
	public Transform[] regRoom;

	private Stack<Transform> doorRoomUnvisited;
	private Stack<Transform> regRoomUnvisited;

	//create 2 linkedDoor
	private List<int> linkedDoorRooms = new List<int>(2);

	// Use this for initialization
	void Awake() {
		instantiateRooms ();
	}
	
	//shuffle door and reg room
	void initializeRooms() {
<<<<<<< HEAD
<<<<<<< HEAD
		for (int i = 0; i < doorRoom.Length; i++) {
			doorRoomUnvisited.Push (doorRoom [i]);
		}
		for (int i = 0; i < regRoom.Length; i++) {
			regRoomUnvisited.Push (regRoom[i]);
		}

=======
=======
>>>>>>> origin/RandomMapGen
		System.Random rnd = new System.Random ();
		for (int i = 0; i < doorRoom.Length; i++) {
			int randomIndex = rnd.Next(doorRoom.Length); // Gets a random int from 0 up to but not including array length
			Transform tempCopy = doorRoom[randomIndex]; // Makes copy of room at random index
			doorRoom[randomIndex] = doorRoom[i]; // Swap part 1
			doorRoom[i] = tempCopy; // Swap part 2
		}
		for (int i = 0; i < regRoom.Length; i++) { // Same but for regRoom instead of doorRoom
			int randomIndex = rnd.Next (regRoom.Length);
			Transform tempCopy = regRoom [randomIndex];
			regRoom [randomIndex] = regRoom [i];
			regRoom [i] = tempCopy;
		}
<<<<<<< HEAD
>>>>>>> origin/RandomMapGen
=======
>>>>>>> origin/RandomMapGen
	}

	//Generate randomDoorRoom
	void generateRandomDoor() {
		int randomDoor = 0;
		int numberOfLinkedDoor = 0;
		while (numberOfLinkedDoor < 2) {    
			randomDoor = Random.Range (0, sizeOfMapX);
			if (!linkedDoorRooms.Contains (randomDoor)) {
				linkedDoorRooms.Add (randomDoor);
				numberOfLinkedDoor++;
			}
		}
		for (int i = 0; i < linkedDoorRooms.Count; i++) {
			Debug.Log ("LinkedDoor: " + linkedDoorRooms[i]);
		}

	}

	void instantiateRooms() {

		//level is needed to keep track of each floor.
		int level = 0;
		Vector3 sizeOfPrefab = new Vector3 (0,0,0);
		for (int j = 0; j < sizeOfMapY; j++) {
			generateRandomDoor ();
			for (int i = 0; i < sizeOfMapX; i++) {
				if (!linkedDoorRooms.Contains (i)) {

					Instantiate (doorRoom [i], transform.position + sizeOfPrefab, transform.rotation);
					sizeOfPrefab.x += doorRoom [i].localScale.x + doorRoom [i].localScale.x + 
						doorRoom [i].localScale.x + doorRoom [i].transform.right.x;
				} else {
					Instantiate (regRoom [i], transform.position + sizeOfPrefab, transform.rotation);
					sizeOfPrefab.x += doorRoom [i].localScale.x + doorRoom [i].localScale.x + 
						doorRoom [i].localScale.x + doorRoom [i].transform.right.x;
				}
			}
			//Clear the linkedDoor
			linkedDoorRooms.Clear ();
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
