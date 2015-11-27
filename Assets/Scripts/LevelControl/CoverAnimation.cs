using UnityEngine;
using System.Collections;

public class CoverAnimation : MonoBehaviour {

    Animator anim;
    PlayerControl player;
    public bool mouseOver = false;
	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
    //detect player clicks
    void OnMouseEnter()
    {
        mouseOver = true;
    }
    void OnMouseExit()
    {
        mouseOver = false;
    }
    //
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
