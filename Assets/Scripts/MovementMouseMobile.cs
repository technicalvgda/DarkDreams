using UnityEngine;
using System.Collections;

public class MovementMouseMobile : MonoBehaviour
{
	const float EDGEBUFFER = 0.10f; // Percentage of screen to validate mouse click/mobile tap
	const float PLAYERSPEED = 1; // Just a placeholder until we get something more finalized
	
	// for edge detection
	Vector3 edgeLeft;
	Vector3 edgeRight;
	Vector3 screenWidth;

	// for player movement
	Vector3 movement;
	
	// for references to player
	Animator animator;
	Rigidbody playerRigidbody;

	// Use this for initialization
	void Awake()
	{
		screenWidth.Set((float)(Screen.width), 0f, 0f);
		edgeLeft.Set(screenWidth.x * EDGEBUFFER, 0f, 0f);
		edgeRight.Set(screenWidth.x - edgeLeft.x, 0f, 0f);

		animator = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Using FixedUpdate instead of Update, meaning this is done before rendering
	void FixedUpdate ()
	{
		if (Input.GetMouseButtonDown (0))
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
		movement.Set (h, 0f, 0f);
		movement *= Time.deltaTime;

		// apply movement to player
		playerRigidbody.MovePosition (transform.position + movement);
	}
}
