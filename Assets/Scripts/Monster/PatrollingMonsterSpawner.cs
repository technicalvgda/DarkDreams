
using UnityEngine;
using System.Collections;

public class PatrollingMonsterSpawner : MonoBehaviour
{

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
    void Start()
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

/*using UnityEngine;
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
	
	/////
	// For spawner activation in respect to a room
	/////
	private bool isSpawning = false;
	
	// room specs
	private Vector2 roomSize;
	private Vector2 roomPosition;
	
	// The buffer for player detection
	public float BUFFER = 0.1f;
	private Bounds playerDetection;
	
	// Reference to room and player
	private GameObject room;
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		GameObject[] roomAll;
			
		//Make the sprite invisible during game. Sprite will be visible so you can see where it is in Scene mode
		GetComponent<Renderer>().material.color = Color.clear;
		
		// Get reference for all of the rooms tagged with "Room", then check to see where spawner is in it
		roomAll = GameObject.FindGameObjectsWithTag("Room");
		// Get reference to player
		player = GameObject.FindWithTag("Player");
		
		// Get the reference for the room the spawner is in
		for (int i = 0; i < roomAll.Length; i++)
		{
			if (roomAll[i].GetComponent<Renderer>().bounds.Contains(this.gameObject.transform.position))
				room = roomAll[i];
		}
				
		// Get current room size
		roomSize.x = room.GetComponent<Renderer>().bounds.size.x;
		roomSize.y = room.GetComponent<Renderer>().bounds.size.y;
		
		// Get current room position
		roomPosition.x = room.transform.position.x;
		roomPosition.y = room.transform.position.y;
		
		// Create bounds to start spawning in the center
		playerDetection = new Bounds(new Vector3(roomPosition.x, roomPosition.y, 0f),
										new Vector3((roomSize.x * BUFFER), roomSize.y, player.transform.position.z));
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check to see if player is detected, start spawn if it is
		if (playerDetection.Contains(player.transform.position) && isSpawning == false)
		{
			//Start the spawn
			Spawn();
			isSpawning = true;
			//Debug.Log("(" + room.name + ") Player has been detected!");
		}
		//If the current pool has reached the maximum...
		if(isSpawning == true)
		{
			if (currentPool >= maxPool) 		
			{	//then stop the spawning
				CancelInvoke();
			}
			else
			{	//else keep spawning within a certain window
				Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
			}
		}
		
		//Debug.Log("(" + room.name + ") Spawning: " + isSpawning);
	}
	//Spawn method
	void Spawn()
	{
		//Spawn the object
		Instantiate(obj, transform.position, Quaternion.identity);
		//Increase amount in pool by 1
		currentPool += 1;
		
		//Debug.Log("(" + room.name + ") Spawned Monster # " + currentPool);
	}
	
	void OnDrawGizmos()
	{	
		Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.4f);
		Gizmos.DrawCube(playerDetection.center, playerDetection.size);
	}
}*/
