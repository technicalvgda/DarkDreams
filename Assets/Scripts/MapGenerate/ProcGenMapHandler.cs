using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProcGenMapHandler : MonoBehaviour
{
	//Declarations
	public ProcGenMap procGenMap;
	
	//Declare list of available tiles and creates an array for them too
	public List<int> AvailableTiles = new List<int>();
	public GameObject[] tiles;
	public int noOfTiles;
	// Map is a list of arrays of tiles. Each index of the list is a floor, each index of the floor is a tile/room
	public List<GameObject[]> floors = new List<GameObject[]>(2);
	
	// Use this for initialization
	void Start()
	{
		//Makes sure we know how tiles we have (Set from Unity)
		tiles = new GameObject[13];
		//Populate the list of available tiles
		for (int i = 0; i < noOfTiles; i++)
		{
			AvailableTiles.Add(i);
		}
		//Test list population
		for (int i = 0; i < AvailableTiles.Count; i++)
		{
			Debug.Log(AvailableTiles[i]);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	public void CreateMap()
	{
		while (floors.Count != 2) { // 
			GameObject[] floor = new GameObject[5]; // Create a new floor with 5 empty spaces
			for (int i = 0; i < floor.Length; i++) { // Makes sure the floor will have floor.length tiles placed in it
				
				if (AvailableTiles.Count > 0) {
					//Get a random number
					System.Random rnd = new System.Random ();
					//Set this random number to our index
					int indexToRemove = rnd.Next (0, AvailableTiles.Count);
					//Sets the tile which will be used/removed
					int tileNumber = AvailableTiles [indexToRemove];
					
					//Test this random
					Debug.Log ("Removing " + tileNumber);
					
					GameObject Tile = tiles [tileNumber]; // Creates temp room from array of possible rooms
					floor [i] = Tile; // Assigns room to "empty space" in floor
				}
			} // One floor has been filled with rooms
			floors.Add (floor); // Add floor to map
		} // All floors have been added to map
	}
}