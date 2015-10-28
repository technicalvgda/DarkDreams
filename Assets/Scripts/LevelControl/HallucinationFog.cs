using UnityEngine;
using System.Collections;

public class HallucinationFog : MonoBehaviour {

    public bool onTrigger;
    public ParticleSystem fog;

	// Use this for initialization
	void Start () {
        fog.GetComponent<ParticleSystem>();
        fog.enableEmission = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(onTrigger)
        {
            fog.enableEmission = true;
        }

	}
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrigger = true;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrigger = false;
        }
    }
}
