//Programmer: Ken Miller
//Dark Dreams - Patrolling Enemy Vision

using UnityEngine;
using System.Collections;

public class PatrollingEnemyVision : MonoBehaviour {
	
	//Start position for the line cast
	private Vector2 startPos; 
	//End position for the line cast
	private Vector2 endPos;	 
	//Variable to set distance of the monster's vision
	public float lineCastDistance = 0f;
	//Tests if the monster is moving left or right
	private bool facingRight = false;
	// Update is called once per frame
	void Update () 
	{
		//Get position of enemy during each frame
		Vector2 currentEnemyPos = gameObject.transform.position; 
		//initialize the starting position every frame
		startPos = currentEnemyPos; 
		//initialize the end position every frame
		endPos = currentEnemyPos; 
		//enemy facing left change position of line cast to follow change of enemy position
		if (!facingRight) {
			//change x position to keep the line cast a certain distance. 
			//THIS IS FOR THE MONSTER FACING LEFT
			endPos.x -= lineCastDistance;
		} 
		//enemy facing right change position of line cast to follow enemy change of position
		else 
		{
			//change x position to keep the line cast a certain distance. 
			//THIS IS FOR THE MONSTER FACING RIGHT
			endPos.x += lineCastDistance; 
		}
		
		//Visually see the line cast in scene mode, NOT GAME
		Debug.DrawLine (startPos, endPos, Color.green); 
		//Making the line cast
		RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast (endPos, startPos); 
		//When the monster sees the player
		if(EnemyVisionTrigger.collider.tag == "Player")
		{
			//Currently set to flip the monster to see that the line cast does 
			//reverse when the patrolling monster flips and moves the other way. 
			//When content team decides what would happen when the the 
			//patrolling monster sees the player add the code in this if block
			FlipEnemy(); 
			
			//CODE TO BE ADDED IN THE FUTURE
			
		}
	}
	//Function to reverse enemy movemeny position, left or right, to 
	//test if line cast flips along with the monster
	void FlipEnemy(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}