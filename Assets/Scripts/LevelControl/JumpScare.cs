using UnityEngine;
using System.Collections;

public class JumpScare : MonoBehaviour {
    AudioHandlerScript audioHandler;
    Animator anim;
    public int soundNum;
	// Use this for initialization
	void Start ()
    {
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
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
    void JumpSound()
    {
        audioHandler.PlaySound(soundNum);
    }
}
