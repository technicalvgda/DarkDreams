using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomMapGeneration : MonoBehaviour {
	public int sizeOfMapX = 1;
	public int sizeOfMapY = 1;

	public Transform[] doorRoom;
	public Transform[] regRoom;

	private int linkedDoorRoom;

	// Use this for initialization
	void Awake() {

	}
	
	//shuffle door and reg room
	void initializeRooms() {
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
	}

	//Generate randomDoorRoom
	void generateRandomDoor() {

	}

	void instantiateRooms() {
	

	}

}
