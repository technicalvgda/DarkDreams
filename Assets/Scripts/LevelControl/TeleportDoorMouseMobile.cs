using UnityEngine;
using System.Collections;

public class TeleportDoorMouseMobile : MonoBehaviour
{
	public Transform exit;

	Vector2 clickPosition;

	// Use this for initialization
	void Awake ()
	{
		clickPosition = new Vector2 (0f, 0f);
	}
	
	// Update is called once per frame
	void OnTriggerStay2D (Collider2D col)
	{
		clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		if (col.tag == "Player" && col.OverlapPoint(clickPosition) && Input.GetMouseButtonDown (0))
			col.transform.position = exit.transform.position;
	}
}
