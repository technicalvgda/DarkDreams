using UnityEngine;
using System.Collections;
//Worked with Jimmy C
public class PlayerHidePC_Narith : MonoBehaviour {
	private bool visible, hide = false;
	
	// Use this for initialization
	void Start () {
		
	}	
	// Update is called once per frame
	void Update () {
		
	}
	//player is under GameObject child
	//check isTrigger, player must be kinematic, only does upon entering
	void OnTriggerEnter2D(){
		Debug.Log ("Ready to Hide");
	}
	//allows actions when staying within collision area
	void OnTriggerStay2D()
	{
		//Toggle Hide/Unhide
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			hide = !hide;
			if (hide) 
			{
				Debug.Log("Hide: " + hide);
				visible = false;
			}
			if (!hide)
			{
				Debug.Log("Hide: " + hide);
				visible = true;
			}
		}
	}
}