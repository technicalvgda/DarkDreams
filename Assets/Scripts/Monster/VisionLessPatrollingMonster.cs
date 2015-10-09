using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisionLessPatrollingMonster : MonoBehaviour {
	
	Vector3 startPos;
	private bool facingRight = true;
	float movement;
	public float speed = 2.0f;
	float distance;
	public int patrolDistance = 5;
	//to access methods and variables from the spawner
	private VisionLessPatrollingMonsterSpawner obj;
	PlayerControl player;
	// Use this for initialization
	void Start ()
	{
		//Get the starting position of the enemy
		startPos = gameObject.transform.position;
		//Grab the spawner and get its script component
		obj = GameObject.Find("VisionLessPatrolEnemySpawner").GetComponent<VisionLessPatrollingMonsterSpawner> ();
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		//If the spawner is facing left...
		if (!obj.facingRight) 
		{	//then flip the monsters it spawns to go left
			FlipEnemy ();
		}
	}
	
	// Update is called once per framed
	void Update () 
	{	

		//Update the enemy position every frame
		Vector2 currentPos = gameObject.transform.position;

		//Calculate the distance it has traveled
		distance = currentPos.x - startPos.x;		
		//Enemy facing and movement to the left
		if (!facingRight) 
		{
			movement = speed * Time.deltaTime;
			transform.Translate (-movement, 0, 0);
		}
		//Enemy facing and movement to the right
		else 
		{
			movement = speed * Time.deltaTime;
			transform.Translate (movement, 0, 0);
		}
		//If the monster has reached its target distance...	
		if (distance > patrolDistance || distance < -patrolDistance) 
		{	//then...
			//Decrement the spawners current pool
			obj.currentPool -= 1;
			//Destroy the enemy once it reaches its target distance
			Destroy (gameObject);				
		}
	}

	//Function to reverse enemy movemeny position, left or right, to 
	//test if line cast flips along with the monster
	void FlipEnemy()
	{
		facingRight = !facingRight;
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