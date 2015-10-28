using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemGeneration : MonoBehaviour {

	//List of items to be spawned in each house.
	public List<Transform> items;

	//The length and height of each room.
	private float l,h;
	
	void Awake() {

		//Get the length and height of the room.
		l = gameObject.GetComponent<RandomMapGeneration>().lengthOfRoom;
		h = gameObject.GetComponent<RandomMapGeneration>().heightOfRoom;
		
	}
	// Assume size of an item is not more than 1/3 in length and 1/3 in height of the size of a room.
	void Start () {
		Stack<Transform> roomList = gameObject.GetComponent<RandomMapGeneration>().roomsWithItem;
		int numberOfRooms = roomList.Count;
		for (int i = 0; i < numberOfRooms; i++ ) {		
			Vector3 roomPosition = new Vector3(roomList.Peek().transform.position.x, roomList.Peek().transform.position.y,
			                                   roomList.Peek().transform.position.z);
			//randomly choose a position on each floor.
			roomPosition.x += Random.Range (-l/3, l/3);
			roomPosition.y += -(h/3);

			//randomly choose an item in an item list.
			int randomItem = Random.Range (0, items.Count);
			Transform itemToSpawn = items [randomItem];

			//spawn an item.
			Instantiate (itemToSpawn, roomPosition, transform.rotation);

			//remove the room that already spawned an item.
			roomList.Pop ();

			//remove an item
			items.Remove(items[randomItem]);
			
		}
	}
	
}
