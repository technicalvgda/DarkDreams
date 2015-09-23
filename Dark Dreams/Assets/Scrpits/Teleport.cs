using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	//Apply this script to the doors to create teleporters
	//assign another door to be the exit in the inspector of this door
	//Door needs box collider and "is trigger" box checked
	public Transform exit;
	void OnTriggerStay2D(Collider2D other)
	{
		//When player is touching the door press "W" to activate
		if (Input.GetKeyDown (KeyCode.W))
			TeleportToExit (other);
	}
	void TeleportToExit(Collider2D other)
	{
		//Takes player to the door assigned as exit from the first door
		other.transform.position = exit.transform.position;
	}
}
