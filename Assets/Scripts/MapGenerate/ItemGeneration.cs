using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemGeneration : MonoBehaviour {
	//public string name;
	public List<Transform> items;
	private float l,h;
	
	void Awake() {
		l = gameObject.GetComponent<RandomMapGeneration>().lengthOfRoom;
		h = gameObject.GetComponent<RandomMapGeneration>().heightOfRoom;
		
	}
	// Assume size of an item is not more than 1/2 in length and 1/3 in height of the size of a room.
	void Start () {
		Stack<Transform> roomList = gameObject.GetComponent<RandomMapGeneration>().roomsWithItem;
		Debug.Log ("Room List: " + roomList.Count);
		int numberOfRooms = roomList.Count;
		for (int i = 0; i < numberOfRooms; i++ ) {		
			Vector3 roomPosition = new Vector3(roomList.Peek().transform.position.x, roomList.Peek().transform.position.y,
			                                   roomList.Peek().transform.position.z);
			roomPosition.x += Random.Range (-l/2, l/2);
			roomPosition.y += -(h/3);
			int randomItem = Random.Range (0, items.Count);
			Transform itemToSpawn = items [randomItem];
			Instantiate (itemToSpawn, roomPosition, transform.rotation);
			roomList.Pop ();
			//items.Remove(items[randomItem]);
			
		}
	}
	
}
