using UnityEngine;
using System.Collections;

public class TutorialHide2 : MonoBehaviour {
	GameObject fake1;
	PlayerControl player;
	PatrollingMonster monster;
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		fake1 = GameObject.FindWithTag ("Fake1");
	}
	
	// Update is called once per frame
	
	void Update () {
		
	}
	
	
	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "Player" && player.hide == true){
			
			Destroy (fake1);
			
			
		}
	}
}
