using UnityEngine;
using System.Collections;

public class CoverAnimation : MonoBehaviour {

    Animator anim;
    PlayerControl player;
    public bool mouseOver = false;
    public bool playerContact = false;
	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
   
    //detect player clicks
    /*
    void OnMouseEnter()
    {
        mouseOver = true;
    }
    void OnMouseExit()
    {
        mouseOver = false;
    }
    
    void OnMouseDown()
    {
        Debug.Log("Clicked");
    }
    */
    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.tag == "Player")
        {
            playerContact = true;
            //Debug.Log("Player touching");
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
   
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("Player not touching");
            playerContact = false;
           
        }
    }
}
