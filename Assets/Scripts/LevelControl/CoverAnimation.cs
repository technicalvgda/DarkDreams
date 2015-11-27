using UnityEngine;
using System.Collections;

public class CoverAnimation : MonoBehaviour {

    Animator anim;
    PlayerControl player;
	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (player.hide)
            {
                anim.SetBool("Hidden", true);
            }
            else
            {
                anim.SetBool("Hidden", false);
            }
        }

    }
}
