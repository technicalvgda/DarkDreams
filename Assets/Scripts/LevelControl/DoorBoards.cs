using UnityEngine;
using System.Collections;

public class DoorBoards : MonoBehaviour {

    Animator anim;
    PlayerControl player;
    int items;
    // Use this for initialization
    void Start ()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        items =player.itemCounter;
        anim.SetInteger("Items", items);
        
	   
	}
}
