/* Description: The eyes will follow the player when the player is within a certain
 * distance. Consist of one Objects called 'pupil'. The 'pupil' will move to the edge
 * of the 'eye' when a player is within its viewing distance.
 * 
 * Limitations: 'Pupil' must be placed near the center of the 'eye'
 * 
 * PS: Not sure if it's possible to set constraints in the 'eye' so that the 'pupil'
 * cannot travel beyond the bounds of the 'eye'.
 */

using UnityEngine;
using System.Collections;

public class FollowingEyes : MonoBehaviour
{
	Transform player;				// the player

	Vector3 pupil;					// pupil position

	float maxDistance = .2f;		// max distance pupil will travel
	float activeDistance = 10f;		// activation distance
	float speed = 1f;				// speed of transition


    void Start() {
		// target player
        player = GameObject.FindGameObjectWithTag("Player").transform;
		// set the pupil
		pupil = transform.position;
    }

	void FixedUpdate() {
		Debug.DrawLine(player.position, pupil, Color.magenta);
	}

    void Update() {
		// distance from the eye to the target
		var distanceToTarget = player.transform.position - pupil;
		// limits the distance the pupil travels
		distanceToTarget = Vector3.ClampMagnitude(distanceToTarget, maxDistance);

		// if the player is within the active distance for pupil to move
		if (Vector3.Distance(player.transform.position,pupil) <= activeDistance) {
			transform.position = Vector3.Lerp (transform.position, pupil + distanceToTarget, Time.deltaTime * speed);
		}
	}
}
