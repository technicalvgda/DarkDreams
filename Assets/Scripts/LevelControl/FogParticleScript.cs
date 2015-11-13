using UnityEngine;
using System.Collections;

public class FogParticleScript : MonoBehaviour
{

    public bool onTrigger;
    ParticleSystem fog;
    GameObject player;
    Transform playerPos;
    float offset= 10f;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = player.GetComponent<Transform>();
        fog = this.GetComponent<ParticleSystem>();
        //fog.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        // If this object is not on the same elevation as the player, do nothing
        if (this.transform.position.y < playerPos.position.y - 10 ||
            this.transform.position.y > playerPos.position.y + 10)
        {
            fog.Stop();
            fog.Clear();
            //fog.enableEmission = false;
        }
        else
        {
            fog.Play();
            //fog.enableEmission = true;
        }
        /*
        if ((player.transform.position.y > transform.position.y + offset )|| (player.transform.position.y < transform.position.y - offset))
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
        */

    }
    
}
