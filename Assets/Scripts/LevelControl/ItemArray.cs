using UnityEngine;
using System.Collections;

public class ItemArray : MonoBehaviour {

    public GameObject[] Items = new GameObject[15];

	// Use this for initialization
	void Start () {
        Items = GameObject.FindGameObjectsWithTag("Item");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
