using UnityEngine;
using System.Collections;

public class PlayerPickMechanics : MonoBehaviour {
    private float points = 0;
	// Use this for initialization
	void Start () {
	
	}
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Item") {
            Destroy(col.gameObject);
            points++;
            print("Points:" + points);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
