using UnityEngine;
using System.Collections;

public class ParticleCollider : MonoBehaviour {

    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    
	// Use this for initialization
	void Start () {
		particle.emissionRate = 0;
		particle.startLifetime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            particle.emissionRate = 6;
            particle.startLifetime = 1;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            particle.emissionRate = 0;
            particle.startLifetime = 0;
        }
    }
}
