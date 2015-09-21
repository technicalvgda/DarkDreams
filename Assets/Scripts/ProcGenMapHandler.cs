﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProcGenMapHandler : MonoBehaviour {

    //Declarations
    public ProcGenMap procGenMap;

    //Declare list of available tiles and creates an array for them too
    public List<int> AvailableTiles = new List<int>();
    public GameObject[] tiles;
    public int noOfTiles;


	// Use this for initialization
	void Start () 
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
	void Update () 
    {
	
	}
}