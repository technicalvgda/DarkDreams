using UnityEngine;
using System.Collections;

public class ItemPickUp_Narith : MonoBehaviour {
    private float points = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //picks up item upon contact, item is destroyed and consumed
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Item")
        {
            Debug.Log("Item picked up");
            Destroy(col.gameObject);
            points++;
        }
    }
}
