using UnityEngine;
using System.Collections;

public class PatrollingMonsterSpawner : MonoBehaviour {

	//The object that it will spawn, set in the inspector
	public GameObject obj;
	//Minimum time for object to spawn
	public float spawnMin = 4f;
	//Maximum time for object to spawn
	public float spawnMax = 5f;
	//Maximum amount of objects to be active at one time
	public int maxPool = 1;
	//Keeps track of how many are currently active
	public int currentPool = 0;
	//Set in inspector to make the spawner spawn objects that go left or right
	public bool facingRight;

	// For spawner activation in respect to a room
	private Vector2 roomSize;
	GameObject room;

	// Use this for initialization
	void Start () 
	{
		//Make the sprite invisible during game. Sprite will be visible so you can see where it is in Scene mode
		GetComponent<Renderer>().material.color = Color.clear;
		//Reference to room, using Temp Fading Room for now
		room = GameObject.Find("TempFadingRoom");
		roomSize.x = room.GetComponent<Renderer>().bounds.size.x;
		roomSize.y = room.GetComponent<Renderer>().bounds.size.y;
		//Start the spawn
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the current pool has reached the maximum...
		if (currentPool >= maxPool) 		
		{	//then stop the spawning
			CancelInvoke();
		}
		else
		{	//else keep spawning within a certain window
			Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
		}
		
		Debug.Log("Room Size: " + roomSize.x + " " + roomSize.y);
	}
	//Spawn method
	void Spawn()
	{
		//Spawn the object
		Instantiate(obj, transform.position, Quaternion.identity);
		//Increase amount in pool by 1
		currentPool += 1;
	}
}