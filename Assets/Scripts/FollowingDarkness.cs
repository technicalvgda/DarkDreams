/* AI_FollowingDarkness moves to the right or left depending on the position
 * of the player. Does not contain the algorithm to determine the path to
 * traverse if the player is above or below the current position of Darkness.
 */

using System.Collections;
using UnityEngine;

public class AI_FollowingDarkness : MonoBehaviour
{
	Transform target;			//the player
	Transform darkness;			//the darkness
	float moveSpeed;			//move speed
	float spriteDirection;		//sprite face direction
	
	void Awake()
    {
        darkness = transform;                      //set darkness data
        spriteDirection = transform.localScale.x;  //set sprite flip axis
           }

	void Start()
    {
               //objected tag with 'Player' becomes the target
        target = GameObject.FindWithTag("Player").transform;
           }

	void UpdateFixed()
    {
        Debug.DrawLine(target.position, darkness.position, Color.magenta);
           }

	void Update()
    {
               if (target.position.x > darkness.position.x)
        {
                        //sets the sprite to face to the right
            transform.localScale = new Vector2(spriteDirection, transform.localScale.y);
            
                        //move to the right of the player
            darkness.position += darkness.right * moveSpeed * Time.deltaTime;
                   }
              if (target.position.x < darkness.position.x)
        {
                        //sets the sprite to face to the left
            transform.localScale = new Vector2(-spriteDirection, transform.localScale.y);
            
                        //move to the left of the player
            darkness.position -= darkness.right * moveSpeed * Time.deltaTime;
                   }
           }
}
