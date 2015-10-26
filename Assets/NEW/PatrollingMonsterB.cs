using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatrollingMonsterB : MonoBehaviour {

	private bool facingRight = true;
	public float speed = 2.0f;
	public float patrolDistance = 5;
	float accumulatedDistance = 0;

	//to access methods and variables from the spawner
	//private PatrollingMonsterSpawner obj;
	PlayerControl player;
	Transform playerPos;

	// Use this for initialization
	void Start ()
	{
		//Grab the spawner and get its script component
		//obj = GameObject.Find("PatrollingEnemySpawner").GetComponent<PatrollingMonsterSpawner> ();
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		playerPos = player.GetComponent<Transform>();
		//If the spawner is facing left...
		//if (!obj.facingRight)
		//{   //then flip the monsters it spawns to go left
		//	FlipEnemy();
		//}
	}

	public void Set(Vector3 position, bool facingRight)
	{
		// Place the object at the specified position, flip its sprite if it's
		// currently facing a different direction, reset some of its attributes
		this.transform.position = position;
		if (this.facingRight != facingRight)
			FlipEnemy();
		this.facingRight = facingRight;
		accumulatedDistance = 0;
		
		this.gameObject.SetActive (true);
	}
	
	// Update is called once per framed
	void Update () 
	{	
		//Update the enemy position every frame
		Vector2 currentPos = gameObject.transform.position;

		//Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
		// If the player is on our floor, run the script.
		if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
		{
			float movement = speed * Time.deltaTime;
			if (!facingRight)
				transform.Translate(-movement, 0, 0);
			else
			//Enemy facing and movement to the right
				transform.Translate(movement, 0, 0);
			accumulatedDistance += movement;

			//If the monster has reached its target distance...	
			if (accumulatedDistance >= patrolDistance)
			{   //then...
				// Disable the object
				this.gameObject.SetActive (false);

				//Decrement the spawners current pool
				//obj.currentPool -= 1;
				//Destroy the enemy once it reaches its target distance
				//Destroy(gameObject);
			}
		}
	}
	
	//Function to reverse enemy movemeny position, left or right, to 
	//test if line cast flips along with the monster
	void FlipEnemy()
	{
		//facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
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
