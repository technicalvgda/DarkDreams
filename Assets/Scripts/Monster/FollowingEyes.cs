/* Description: The eyes will follow the player when the player is within a certain
 * distance. Consist of one Objects called 'pupil'. The 'pupil' will move to the edge
 * of the 'eye' when a player is within its viewing distance. Once the player is out
 * of the activation distance, the 'pupil' will reset its position.
 * 
 * Limitations: 'Pupil' must be placed near the center of the 'eye'
 * 
 * Notes: Not sure if it's possible to set constraints in the 'eye' so that the 'pupil'
 * cannot travel beyond the bounds of the 'eye'.
 *
 * Change Log
 *  v1.1
 *      Increase the track speed of the pupil.
 *      Added speedResetPos and pupilStart variables.
 *  v1.0
 *      Created the script. 
 */

using UnityEngine;
using System.Collections;

public class FollowingEyes : MonoBehaviour
{
	Transform player;				// the player

	Vector3 pupil;					// pupil position
    Vector3 pupilStart;             // pupil initial position

	float maxDistance = .15f;		// max distance pupil will travel
	float activeDistance = 15f;		// activation distance
	float speedTrackPlayer = 5f;	// speed of eye movement
    float speedResetPos = .8f;      // speed of eye reset position

    void Start() {
		// target player
        player = GameObject.FindGameObjectWithTag("Player").transform;
		// set the pupil
		pupil = pupilStart = transform.position;
    }

    void FixedUpdate() {
        if (Vector3.Distance(player.transform.position, pupil) <= activeDistance) {
            Debug.DrawLine(player.position, pupil, Color.magenta);
        }
	}

    void Update() {
		// distance from the eye to the target
		var distanceToTarget = player.transform.position - pupil;

        // distance from current pupil position to starting pupil position
        var distanceToStart = pupilStart - pupil;

		// limits the distance the pupil travels
		distanceToTarget = Vector3.ClampMagnitude(distanceToTarget, maxDistance);

        // if the player is within the active distance for pupil to move
        if (Vector3.Distance(player.transform.position, pupil) <= activeDistance) {
            transform.position = Vector3.Lerp(transform.position, pupil + distanceToTarget,
                                              Time.deltaTime * speedTrackPlayer);
        }
        // resets to initial position if player is not in range
        else {
            transform.position = Vector3.Lerp(transform.position, pupil + distanceToStart,
                                              Time.deltaTime * speedResetPos);
        }
	}
}
