using UnityEngine;
using System.Collections;

public class TutorialHide : MonoBehaviour {
		// Use this for initialization
	GameObject fake;
	GameObject darkness;
	FadingDarkness handler;
	PlayerControl player;
	PatrollingMonster monster;
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		fake = GameObject.FindWithTag ("Fake");
		darkness = GameObject.Find("FadingDarknessCanvasTut");
		handler = darkness.GetComponent<FadingDarkness>();
		//handler.enabled = false;
	}
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col){
	if(col.gameObject.tag == "Player" && player.hide == true){
				Destroy (fake);
			//handler.enabled = true;
			
	}
}
}
