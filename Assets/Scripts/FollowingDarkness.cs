/* AI_FollowingDarkness moves to the right or left depending on the position
 * of the player. Does not contain the algorithm to determine the path to
 * traverse if the player is above or below the current position of Darkness.
 */

using System.Collections;
using UnityEngine;

public class AI_FollowingDarkness : MonoBehaviour {
	Transform target;			//the player
	Transform darkness;			//the darkness
	float moveSpeed;			//move speed
	float spriteDirection;		//sprite face direction
	
	void Awake() {
        moveSpeed = 3;                              //set moveSpeed to TEST VALUE 3
		darkness = transform;						//set Darkness data
		spriteDirection = transform.localScale.x;	//set sprite direction transform
	}

	void Start() {
		//objected tag with 'Player' becomes the target
		target = GameObject.FindWithTag ("Player").transform;
	}

	void UpdateFixed() {
		Debug.DrawLine (target.position, darkness.position, Color.magenta);
	}

    void Update() {
        //move to the right of the player and makes the sprite face to the right
        if (target.position.x > darkness.position.x) {
            transform.localScale = new Vector2(spriteDirection, transform.localScale.y);
            darkness.position += darkness.right * moveSpeed * Time.deltaTime;
        }
        //move to the left of the player and makes the sprite face to the left
        if (target.position.x < darkness.position.x) {
            transform.localScale = new Vector2(-spriteDirection, transform.localScale.y);
            darkness.position -= darkness.right * moveSpeed * Time.deltaTime;
        }
	}
}