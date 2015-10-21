﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour
{
	public GameObject hunterEnemy;
	public GameObject wall;
	float wallMargin;
	const float wallOffset = 20;

	GameObject player;
	GameObject cam;

	// Use this for initialization
	void Start ()
	{
		Setup ();
		StartCoroutine (_Cutscene ());
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void Setup()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");

		// Move the player to the center
		player.transform.position = new Vector3 (128, player.transform.position.y, 0);

		// Figure out where the left wall is so the camera's panning can stop there
		wallMargin = wall.transform.position.x + wallOffset;
	}
	
	IEnumerator _Cutscene()
	{
		yield return new WaitForSeconds (0.5f);

		// Lock the camera once it finishes positioning itself
		cam.GetComponent<CameraFollowScript> ().enabled = false;

		yield return new WaitForSeconds (1);

		// Pan camera to left until it hits the wall
		while (cam.transform.position.x > wallMargin)
		{
			cam.transform.position += new Vector3(-0.1f, 0, 0);
			yield return null;
		}

		yield return new WaitForSeconds (1);

		// move the hunter
		hunterEnemy.SetActive (true);

		yield return new WaitForSeconds (0.5f);

		// pan back to player
		while (cam.transform.position.x < player.transform.position.x)
		{
			cam.transform.position += new Vector3(0.2f, 0, 0);
			yield return null;
		}
		cam.GetComponent<CameraFollowScript> ().enabled = true;

		yield return null;
	}
}