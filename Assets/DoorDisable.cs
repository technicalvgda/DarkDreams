using UnityEngine;
using System.Collections;

public class DoorDisable : MonoBehaviour {

    Transform player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }
    
    void Update()
    {
        if (player.position.x == this.transform.position.x && player.position.y < this.transform.position.y+5 && player.position.y > this.transform.position.y-5)
        {
            this.GetComponent<TeleportDoors>().exit = null;
            this.GetComponent<Animator>().enabled = true;
        }
    }
	
}
