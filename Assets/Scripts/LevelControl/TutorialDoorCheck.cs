using UnityEngine;
using System.Collections;

public class TutorialDoorCheck : MonoBehaviour {
	public static bool islocked = true;
	 TeleportDoors teleport;

	GameObject door;
	// Use this for initialization
	void Start () {
		teleport = GameObject.FindGameObjectWithTag("Lock").GetComponent<TeleportDoors>();
		//teleport = GameObject.Find("Door2(1)").GetComponent<TeleportDoors>();
		door = GameObject.FindGameObjectWithTag ("Lock");
		teleport.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (PlayerControl.itemCounter == 1) {
			islocked = false;
			Destroy (GameObject.Find ("Lock"));
		//	TeleportDoors.exit.transform.position = null;
			teleport.enabled = true;
			GameObject.FindGameObjectWithTag("Lock").GetComponent<TeleportDoors>().enabled = true;
		} else {
			teleport.enabled = false;
			GameObject.FindGameObjectWithTag("Lock").GetComponent<TeleportDoors>();
			islocked = true;
			//TeleportDoors.exit.transform.position = null;
		}


	}
void OnTriggerEnter2D(Collider2D col){
		if (islocked == false) {
			Debug.Log ("You have enough to enter!");
		//	col.gameObject.GetComponent<TeleportDoors>().enabled = true;
			teleport.enabled = true;
			col.transform.position = col.transform.position;

			
		}
		else{
			//col.gameObject.GetComponent<TeleportDoors>().enabled = false;
			teleport.enabled = false;
			Debug.Log ("insufficient items");
		//	col.transform.position =TeleportDoors.exit.transform.position;
		
		
		}

	}



}
