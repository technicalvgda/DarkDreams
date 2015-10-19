﻿using UnityEngine;
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
	private Vector2 roomPosition;
	
	GameObject room;

	// Use this for initialization
	void Start () 
	{
		GameObject[] roomAll;
		GameObject self;
			
		//Make the sprite invisible during game. Sprite will be visible so you can see where it is in Scene mode
		GetComponent<Renderer>().material.color = Color.clear;
		
		// Get reference to this spawner
		self = GameObject.Find("PatrollingEnemySpawner");
		// Get reference for all of the rooms tagged with "Room", then check to see where spawner is in it
		roomAll = GameObject.FindGameObjectsWithTag("Room");
		
		// Get the reference for the room the spawner is in
		for (int i = 0; i < roomAll.Length - 1; i++)
		{
			if (roomAll[i].GetComponent<Renderer>().bounds.Contains(self.transform.position))
				room = roomAll[i];
		}
				
		// Get current room size
		roomSize.x = room.GetComponent<Renderer>().bounds.size.x;
		roomSize.y = room.GetComponent<Renderer>().bounds.size.y;
		
		// Get current room position
		roomPosition.x = room.transform.position.x;
		roomPosition.y = room.transform.position.y;
		
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
		
		Debug.Log("Room Name: " + room.name);
		Debug.Log("Room Size: " + roomSize);
		Debug.Log("Room Position: " + roomPosition);
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