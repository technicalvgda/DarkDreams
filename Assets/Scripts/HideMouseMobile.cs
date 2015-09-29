using UnityEngine;
using System.Collections;

public class HideMouseMobile : MonoBehaviour
{
	const int hidingOrder = 0;
	const int sortingOrder = 2;

	bool isHiding = false;

	// For click/tap detection
	Vector2 clickPosition;

	// Reference to player
	SpriteRenderer playerSprite;

	// Use this for initialization
	void Awake ()
	{
		clickPosition = new Vector2 (0f, 0f);
		playerSprite = GetComponent<SpriteRenderer> ();
	}

	// For collision detection
	void OnTriggerStay2D(Collider2D col)
	{
		// OverlapPoint refers to world space instead of screen space, adjusting accordingly
		clickPosition.x = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
		clickPosition.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		// Check to see if mouse position is overlapping cover, perform appropriate task
		if (Input.GetMouseButtonDown (0))
		{
			if (col.gameObject.tag == "Cover" && !isHiding && col.OverlapPoint(clickPosition))
			{
				playerSprite.sortingOrder = hidingOrder;
				isHiding = true;
			}
			else if (isHiding)
			{
				playerSprite.sortingOrder = sortingOrder;
				isHiding = false;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}