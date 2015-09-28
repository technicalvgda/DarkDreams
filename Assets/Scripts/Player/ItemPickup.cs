using UnityEngine;
using System.Collections;

/*This script is to be placed inside "item" objects. It's purpose is to add the score into the player script.
 * 
 * 
 * Instructions: 
 1)  Drag this script in your Scripts folder in Unity.
 2)  Create an object. This will be an "item" the player can "pick up."
 3)  In the inspector tab, add a "Box Collider 2D" component and check "Is Trigger" option.
 4)  Drag this script to your item object.
 5)  In the inspector tab, locate your "Item Pickup(Script)" script and find the option "Item Add".
 6)  Insert the amount you wish to add (ideally this will be 1) and hit enter.
 7)  Open your PlayerControl script and add this code into it:

//to count item pickups
public static int itemCounter;

// adds points in  the Player Script
public static void AddPoints (int itemAdd){
	itemCounter += itemAdd; //adds amount to current score
	Debug.Log ("Score: " + itemCounter); //confirms the player has picked up the object (track amount). this is removeable.
}


 * 8) Save both your PlayerControl script and ItemPickup script. Run the game!
 * Note: In the event that you are not allowed or cannot see the "Item Add" option, try skipping to step 7 first and include the code that will be placed into the PlayerContol script.
 */


public class ItemPickup : MonoBehaviour {

	public int itemAdd; // creates counter that can be passed to player control; add amount in inspector

	void OnTriggerEnter2D (Collider2D other) {

		//this "if" statement confirms that only the player will be allowed to pick the object up (this is assuming the player script is called "PlayerControl")
		//this is needed in case monsters who also may ahve the box collider2d component, will not pick up the item
		if(other.GetComponent<PlayerControl>() == null)
			return;

		PlayerControl.AddPoints(itemAdd); // on collison, will add the amount in player script
		Destroy(gameObject); //destroys the object

	}
}	