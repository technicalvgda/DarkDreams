using UnityEngine;
using System.Collections;

public class JumpScare : MonoBehaviour {
    AudioHandlerScript audioHandler;
    AudioSource sfx;
    Animator anim;
    public int soundNum;
    bool activated = false;
	// Use this for initialization
	void Start ()
    {
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        sfx = this.GetComponent<AudioSource>();
        //set volume to player's setting
        sfx.volume = audioHandler.sfxVolume;//PlayerPrefs.GetFloat("SFX");
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && activated == false)
        {
            activated = true;
            sfx.Play();
            anim.SetTrigger("Jump");
            

        }
    }
    void JumpSound()
    {
        sfx.Play();
    }
}
