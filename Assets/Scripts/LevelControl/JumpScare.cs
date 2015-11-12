using UnityEngine;
using System.Collections;

public class JumpScare : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("Jump");
        }
    }
}
