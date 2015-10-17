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

	// Use this for initialization
	void Start () 
	{
		//Make the sprite invisible during game. Sprite will be visible so you can see where it is in Scene mode
		GetComponent<Renderer>().material.color = Color.clear;
		//Start the spawn
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector2 currentPos = gameObject.transform.position;
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script. 
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
        {
            //If the current pool has reached the maximum...
            if (currentPool >= maxPool)
            {   //then stop the spawning
                CancelInvoke();
            }
            else
            {   //else keep spawning within a certain window
                Invoke("Spawn", Random.Range(spawnMin, spawnMax));
            }
        }
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