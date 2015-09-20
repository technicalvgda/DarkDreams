using UnityEngine;
using System.Collections;

public class MovementMouseMobile : MonoBehaviour
{
	public const float EDGEBUFFER = 0.05f; // Percentage of screen to validate mouse click/mobile tap
	const float PLAYERSPEED = 5; // Just a placeholder until we get something more finalized
	
	// for edge detection
	Vector2 edgeLeft;
	Vector2 edgeRight;
	Vector2 screenWidth;

	// for player movement
	Vector2 movement;
	
	// for references to player
	Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Awake()
	{
		screenWidth = new Vector2((float)Screen.width, 0f);
		edgeLeft = new Vector2(screenWidth.x * EDGEBUFFER, 0f);
		edgeRight = new Vector2(screenWidth.x - edgeLeft.x, 0f);

		playerRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Using FixedUpdate instead of Update, meaning this is done before rendering
	void FixedUpdate ()
	{
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0))
		{
			if (Input.mousePosition.x < edgeLeft.x)
				Move (-PLAYERSPEED);
			if (Input.mousePosition.x > edgeRight.x)
				Move (PLAYERSPEED);
		} 
		else
		{
			Move (0);
		}
	}

	void Move (float h)
	{
		// Set movement and normalize in terms of time passed from previous frame
		// (Assuming we will be frame rate dependent)
		movement.Set (h, 0f);
		movement *= Time.deltaTime;

		// Transform is a Vector3, adjusting data so RigidBody2D can accept it
		movement.x += transform.position.x;
		movement.y += transform.position.y;

		// apply movement to player
		playerRigidbody.MovePosition (movement);
	}
}
