using UnityEngine;
using System.Collections;

public class ParticleGlow : MonoBehaviour {

    ParticleSystem particle;
    GameObject player;
    Transform playerPos;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = player.GetComponent<Transform>();
        particle = this.GetComponent<ParticleSystem>();
        //particle.emissionRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If this object is not on the same elevation as the player, do nothing
        if (this.transform.position.y < playerPos.position.y - 10 ||
            this.transform.position.y > playerPos.position.y + 10)
        {
           // particle.Stop();
            //particle.Clear();
            particle.emissionRate = 0;
            particle.startLifetime = 0;
        }
        else
        {
           // particle.Play();
			particle.emissionRate = 6;
			particle.startLifetime = 1;
		
        }
    }
/*
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            particle.emissionRate = 1;
            particle.startLifetime = 1;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            particle.emissionRate = 0;
            particle.startLifetime = 0;
        }
    }
    */
}
