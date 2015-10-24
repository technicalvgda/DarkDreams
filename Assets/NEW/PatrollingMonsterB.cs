using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatrollingMonsterB : MonoBehaviour {

	public float speed = 2.0f;
	public int patrolDistance = 5;
	float accumulatedDistance;

	// With periodicPause on, the object moves for moveDuration second and
	// pauses for pauseDuration seconds before moving again. Set in inspector
	public bool periodicPause;
	public float moveDuration;
	public float pauseDuration;
	WaitForSeconds pause;
	
	bool facingRight = true;

	PlayerControl player;
	Transform playerPos;

	// Determine how to update the object
	IEnumerator behavior;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		playerPos = player.GetComponent<Transform>();

		pause = new WaitForSeconds(pauseDuration);
	}
	
	public void Set(Vector3 position, bool facingRight)
	{
		// Stop the coroutine in case this method is called while it's still running
		if (behavior != null)
			StopCoroutine (behavior);

		// Place the object at the specified position, flip its sprite if it's
		// currently facing a different direction, reset some of its attributes
		this.transform.position = position;
		if (this.facingRight != facingRight)
			Flip();
		this.facingRight = facingRight;
		accumulatedDistance = 0;

		this.gameObject.SetActive (true);

		// Set the type of behavior
		if (!periodicPause)
			behavior = _Travel();		// Move continuously
		else
			behavior = _TravelPause();	// Move with periodic pauses

		if (behavior != null)
			StartCoroutine(behavior);
	}
	
	void Update()
	{
		// If this object is not on the same elevation as the player
		if (this.transform.position.y < playerPos.position.y - 10 &&
		    this.transform.position.y > playerPos.position.y + 10)
		{
			// Disable it
			this.gameObject.SetActive(false);
		}
		// It won't get enabled again until the player walks across
		// the center of a room containing the spawner. Probably
	}

	// Move without pausing
	IEnumerator _Travel()
	{
		while (accumulatedDistance < patrolDistance)
		{
			Move ();
			yield return null;
		}
		this.gameObject.SetActive (false);
	}

	// Move moveDuration seconds before pausing for pauseDuration seconds
	IEnumerator _TravelPause()
	{
		while (true)
		{
			float time = 0;
			while (time < moveDuration)
			{
				Move ();
				if (accumulatedDistance >= patrolDistance)
				{
					this.gameObject.SetActive (false);
					yield break;
				}
				time += Time.deltaTime;
				yield return null;
			}
			yield return pause;
		}
	}

	void Move()
	{
		float movement = speed * Time.deltaTime;
		if (!facingRight)
			transform.Translate(-movement, 0, 0);
		else
			transform.Translate(movement, 0, 0);

		accumulatedDistance += movement;
	}

	void Flip()
	{
		transform.localScale.Scale(new Vector3(-1, 1, 1));
		// do extra things related to flipping here
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//If the player collides with the patrolling enemy and not hiding
		if (col.gameObject.tag == "Player" && player.hide == false)
		{
			//Monster stops moving
			speed = 0;
		}
	}
}

// Old code, Ctrl+Alt+C

//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public class PatrollingMonster : MonoBehaviour {
//	
//	Vector3 startPos;
//	private bool facingRight = true;
//	float movement;
//	public float speed = 2.0f;
//	float distance;
//	public int patrolDistance = 5;
//	//to access methods and variables from the spawner
//	private PatrollingMonsterSpawner obj;
//	PlayerControl player;
//	Transform playerPos;
//	
//	// Use this for initialization
//	void Start ()
//	{
//		//Get the starting position of the enemy
//		startPos = gameObject.transform.position;
//		//Grab the spawner and get its script component
//		obj = GameObject.Find("PatrollingEnemySpawner").GetComponent<PatrollingMonsterSpawner> ();
//		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
//		//If the spawner is facing left...
//		if (!obj.facingRight)
//		{   //then flip the monsters it spawns to go left
//			FlipEnemy();
//		}
//		
//	}
//	
//	// Update is called once per framed
//	void Update () 
//	{	
//		
//		//Update the enemy position every frame
//		Vector2 currentPos = gameObject.transform.position;
//		
//		//Calculate the distance it has traveled
//		distance = currentPos.x - startPos.x;
//		//Enemy facing and movement to the left
//		
//		playerPos = GameObject.Find("Player").GetComponent<Transform>();
//		//Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
//		// If the player is on our floor, run the script.
//		if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
//		{
//			if (!facingRight)
//			{
//				movement = speed * Time.deltaTime;
//				transform.Translate(-movement, 0, 0);
//			}
//			//Enemy facing and movement to the right
//			else
//			{
//				movement = speed * Time.deltaTime;
//				transform.Translate(movement, 0, 0);
//			}
//			//If the monster has reached its target distance...	
//			if (distance > patrolDistance || distance < -patrolDistance)
//			{   //then...
//				//Decrement the spawners current pool
//				obj.currentPool -= 1;
//				//Destroy the enemy once it reaches its target distance
//				Destroy(gameObject);
//			}
//		}
//		else
//		{
//			//do nthing
//		}
//	}
//	
//	//Function to reverse enemy movemeny position, left or right, to 
//	//test if line cast flips along with the monster
//	void FlipEnemy()
//	{
//		facingRight = !facingRight;
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		transform.localScale = theScale;
//	}
//	void OnTriggerEnter2D(Collider2D col)
//	{
//		//If the player collides with the patrolling enemy and not hiding
//		if (col.gameObject.tag == "Player" && player.hide == false)
//		{
//			//Monster stops moving
//			speed = 0;
//		}
//	}
//}