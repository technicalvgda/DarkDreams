//Programmer: Ken Miller
//Dark Dreams - Patrolling Enemy Vision

using UnityEngine;
using System.Collections;

public class PatrollingEnemyVision : MonoBehaviour {

	//Player variable for when the linecast hits the player
	public GameObject player; 
	//Start position for the line cast
	private Vector2 startPos; 
	//End position for the line cast
	private Vector2 endPos;	  
	//Tests if the monster is moving left or right
	private bool facingRight = false;
	// Update is called once per frame
	void Update () 
	{
		//Get position of enemy dduring each frame
		Vector2 currentEnemyPos = gameObject.transform.position; 
		//initialize the starting position every frame
		startPos = currentEnemyPos; 
		//initialize the end position every frame
		endPos = currentEnemyPos; 
		//enemy facing left change position of end of line cast to follow change of enemy position
		if (!facingRight) {
			//change x position to keep the line cast a certain distance. Change this variable to increase or decrease
			//the length of the monsters vision. THIS IS FOR THE MONSTER FACING LEFT
			endPos.x -= 4.0f;
			//currentEnemyPos.x -= 1.0f * Time.deltaTime; //DEBUG PURPOSES TO TEST IF LINE CAST MOVES WITH MONSTER
		} 
		//enemy facing right change position of line cast to follow enemy change of position
		else 
		{
			//change x position to keep the line cast a certain distance. Change this variable to increase or decrease
			//the length of the monsters vision. THIS IS FOR THE MONSTER FACING RIGHT
			endPos.x += 4.0f; 
			//currentEnemyPos.x += 1.0f*Time.deltaTime;  //DEBUG PURPOSES TO TEST IF LINE CAST MOVES WITH MONSTER
		}
		//gameObject.transform.position = currentEnemyPos; //DEBUG PURPOSES TO TEST IF LINE CAST MOVES WITH MONSTER

		//Visually see the line cast in scene mode, NOT GAME
		Debug.DrawLine (startPos, endPos, Color.green); 
		//Making the line cast
		RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast (endPos, startPos); 
		//When the monster sees the player
		if(EnemyVisionTrigger.collider.tag == "Player")
		{
			//FlipEnemy(); //DEBUG PURPOSES TO SEE IF THE LINECAST REVERSES WHEN PATROLLING MONSTER REVERSES MOVEMENT

			//Code currently set to when the player collides with the line cast the monster moves toward the player faster
			//Can be changed to what content team decides what will happen when player crosses the monster vision
			if (!facingRight) 
			{
				currentEnemyPos.x -= 1.5f * Time.deltaTime;
			} 
			else 
			{
				currentEnemyPos.x += 3.5f*Time.deltaTime;
			}
			gameObject.transform.position = currentEnemyPos;
		}
	}
	 //Function to reverse enemy movemeny position, left or right, to test if line cast flips along with the monster
	void FlipEnemy(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}