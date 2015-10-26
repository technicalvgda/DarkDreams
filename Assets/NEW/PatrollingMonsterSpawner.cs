
using UnityEngine;
using System.Collections;

public class PatrollingMonsterSpawner : MonoBehaviour {

	public PatrollingMonster monster;
	//private const int POOL_SIZE = 2;
	//private GameObject[] pool = new GameObject[POOL_SIZE];

	// Amount of time to wait between monster spawns
	public float spawnIntervalMin = 4;
	public float spawnIntervalMax = 5;

	// The monster's facing direction. Set in inspector
	public bool facingRight;

	// Monsters start spawning once the player steps into this
	Bounds playerDetection;

	// The player detection area has a width of ROOM_WIDTH * BUFFER
	public float BUFFER = 0.1f;

	GameObject player;
	GameObject room;

	// Use this for initialization
	void Start () 
	{
		//Make the sprite invisible during game. Sprite will be visible so you can see where it is in Scene mode
		//GetComponent<Renderer>().material.color = Color.clear;

		// Get reference to player
		player = GameObject.FindGameObjectWithTag("Player");

		// Locate the room this spawner is contained im
		GameObject[] roomAll;
		// Get reference for all of the rooms tagged with "Room", then check to see where spawner is in it
		roomAll = GameObject.FindGameObjectsWithTag("Room");
		// Get the reference for the room the spawner is in
		for (int i = 0; i < roomAll.Length; i++)
		{
			if (roomAll[i].GetComponent<Renderer>().bounds.Contains(this.gameObject.transform.position))
				room = roomAll[i];
				// break out when found reference?
		}

		// Creates a thin, tall rectangular bounds at the center of the room.
		// If the player enters this area, start spawning patrols
		playerDetection = new Bounds(
			// Center = center point of the current room
			new Vector3(
				room.transform.position.x,
				room.transform.position.y,
				0),
			// Size = size of the current room, then narrow to around the center
			new Vector3(
				room.GetComponent<Renderer>().bounds.size.x * BUFFER,
				room.GetComponent<Renderer>().bounds.size.y,
				player.transform.position.z)
			);

		StartCoroutine(_Update());
	}

	IEnumerator _Update()
	{
		// Check if the player has entered the bounds
		while (!playerDetection.Contains(player.transform.position))
			yield return null;
		//Debug.Log("(" + room.name + ") Player entered bounds.");

		// Spawn a monster, wait until it has traveled the full length and deactivated itself,
		// wait some time, then reset its position and activate it again
		while (true)
		{
			// "Spawns" the monster by activating it and placing it at the spawner's position
			monster.Set(this.transform.position, facingRight);
			//Debug.Log("(" + room.name + ") Spawned monster.");

			// Do nothing while the monster is active
			while (monster.gameObject.activeSelf)
				yield return null;

			// Wait an interval
			yield return new WaitForSeconds(
				Random.Range(spawnIntervalMin, spawnIntervalMax));
		}
	}

	// Display the bounds in the Scene tab
	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.4f);
		Gizmos.DrawCube(playerDetection.center, playerDetection.size);
	}
}
