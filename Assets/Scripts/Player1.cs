using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

	public float speed = 6f;
	private float moveX = 0f;
	public GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveX = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * moveX, GetComponent <Rigidbody2D> ().velocity.y);

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Enemy") {

			gameOverPanel.SetActive(true);
			speed = 0f;
		}
	}
}
