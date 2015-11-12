using UnityEngine;
using System.Collections;

public class FogParticleScript : MonoBehaviour
{

    public bool onTrigger;
    ParticleSystem fog;
    GameObject player;
    float offset= 10f;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        fog = this.GetComponent<ParticleSystem>();
        fog.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if((player.transform.position.y > transform.position.y + offset )|| (player.transform.position.y < transform.position.y - offset))
        {
           
            onTrigger = false;
        }
        else
        {
            fog.enableEmission = true;
            onTrigger = true;
        }
        if (onTrigger)
        {
            fog.enableEmission = true;
        }

    }
    /*
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrigger = false;
        }
    }
    */
}
