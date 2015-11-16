using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	private List<Transform> hallToRemoveE = new List<Transform>();
	private List<Transform> hallToRemoveO = new List<Transform>();
	
	
	void Awake() {
		
		if (doorRoom.Length < noRoomsPerFloor) {
			Debug.Log("Please make sure there are at least 4 door rooms");
			return;
		}
		canGenerate = false;
		initializeRooms ();
		
	}
	
	// Use this for initialization
	void Start () {
		instantiateRooms ();
		//InvokeRepeating ("generate", noRoomsPerFloor-1, noRoomsPerFloor-1);        
	}
	void instantiateRooms() {
		
		generateEven ();
		generateOdd ();

		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			if (gameObj.name == "Door To Be Removed E" || gameObj.name == "Door To Be Removed O") {
				Destroy (gameObj.GetComponent("TeleportDoors"));
			}
		}


		string s1 = evenHallway [0].name;
		string s2 = evenHallway[evenHallway.Count-1].name;
		string s3 = oddHallway [0].name;
		string s4 = oddHallway[oddHallway.Count-1].name;
		
		string[] doorFirstBot = s1.Split (' ');
		string[] doorLastBot = s2.Split(' ');
		string[] doorFirstTop = s3.Split (' ');
		string[] doorLastTop = s4.Split (' ');
		
		//Set the bottom door to the first door of the next floor
		//Transform bottomDoorLast = evenHallway[evenHallway.Count-1].Find("Door" + doorLastBot[1]).transform;
		//bottomDoorLast.GetComponent<TeleportDoors>().exit = oddHallway[0].Find("Door" + doorFirstTop[1]).transform;
		
		Debug.Log ("doorLast, doorFirst: " + doorLastBot[1] + ", " + doorFirstTop[1]);

		//Set Trigger door for generation
		/*doorToBeTriggered = GameObject.Find ("Door" + doorNumber1[1]);
        Debug.Log ("Door to be Triggered: " + doorToBeTriggered.name);
        s1 = bottomHallway [0].name;
        s2 = topHallway [topHallway.Count - 1].name;
        doorNumber1 = s1.Split(' ');
        doorNumber2 = s2.Split(' ');
        Transform firstDoor = GameObject.Find("Door" + doorNumber1[1]).transform;
        Destroy(firstDoor.GetComponent("TeleportDoors"));
        
        Transform lastDoor = GameObject.Find("Door" + doorNumber2[1]).transform;
        lastDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;
        
        lastDoorOnMap = doorNumber2[1];
        bottomHallway.Clear ();
        topHallway.Clear ();
        */
	}
	
	
	void generateEven() {
		//Spawn Even
		for(int i = 0; i < noRoomsPerFloor; i++) {
			Transform obj = (Transform)(Instantiate(evenHallway[i], transform.position + sizeOfPrefab , transform.rotation));
			sizeOfPrefab.x += lengthOfRoom;
			obj.name = "New Room E " + i;
			if (i == 0) {
				obj.name = "Room to Remove E";
				string s1 = evenHallway [0].name;
				string[] doorFirstBot = s1.Split (' ');
				Transform firstDoorTop = obj.Find ("Door" + doorFirstBot [1]).transform;
				obj.Find ("Door" + doorFirstBot[1]).name = "Door To Be Removed E";
				
			}
			if (i == noRoomsPerFloor-1) {
				string s2 = evenHallway[evenHallway.Count-1].name;
				string[] doorLastBot = s2.Split(' ');
				Transform lastDoorBot = obj.Find ("Door" + doorLastBot[1]).transform;
				obj.Find ("Door" + doorLastBot[1]).name = "Door To Be Connected E";  
				
			}
			hallToRemoveE.Add (obj);
		}
		//Change level
		level++;
		sizeOfPrefab.x = 0;
		
		//Size/Gap between each level (height of rooms)
		sizeOfPrefab.y += heightOfRoom;
		sizeOfPrefab.y += level;
	}
	
	void generateOdd() {
		//Spawn Odd
		for (int i = 0; i < noRoomsPerFloor; i++) {
			Transform obj = (Transform)(Instantiate(oddHallway[i], transform.position + sizeOfPrefab , transform.rotation));
			sizeOfPrefab.x += lengthOfRoom;
			obj.name = "New Room O " + i;
			if (i == 0) {
				obj.name = "Room to Remove O";
				string s3 = oddHallway[0].name;
				string[] doorFirstTop = s3.Split(' ');
				Transform firstDoorTop = obj.Find ("Door" + doorFirstTop [1]).transform;
				obj.Find ("Door" + doorFirstTop[1]).name = "Door To Be Removed O";  

			}
			if (i == noRoomsPerFloor-1) {
				string s4 = oddHallway[oddHallway.Count-1].name;
				string[] doorLastTop = s4.Split(' ');
				Transform lastDoorTop = obj.Find ("Door" + doorLastTop [1]).transform;
				obj.Find ("Door" + doorLastTop[1]).name = "Door To Be Connected O";  

			}
			hallToRemoveO.Add (obj);
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
		randomizeRooms ();
		
		//r to keep track of the room used
		int r = 0;
		//set door room to be the first room on each floor
		evenHallway.Add (doorRoom [r]);
		r++;
		oddHallway.Add (doorRoom [r]);
		r++;
		for (int i = 0; i < noRoomsPerFloor-NUMBER_OF_DOOR_PER_FLOOR; i++) {
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
	
	
}