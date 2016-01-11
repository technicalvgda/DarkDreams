using UnityEngine;
using System.Collections;

public class NightmareBorder : MonoBehaviour {

    Vector3 wallPos;
    GameObject player;
    float yOffset = 10f;
    // Use this for initialization
    void Start ()
    {
        wallPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        wallPos.y = player.transform.position.y+ yOffset;
        transform.position = wallPos;
	}
}
