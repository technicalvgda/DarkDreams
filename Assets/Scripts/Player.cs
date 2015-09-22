using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 3f;
	private float moveX = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveX = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * moveX, GetComponent<Rigidbody2D> ().velocity.y);

	}
}
