using UnityEngine;
using System.Collections;

// looks barren
public class CustomButton : MonoBehaviour {

	public bool clicked;
	public bool selected;
	int time = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Check if Box Collider is clicked
	void OnMouseDrag() {
		if (++time == 1)
			clicked = true;
		else
			clicked = false;
	}

	void OnMouseUp() {
		time = 0;
		clicked = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// raycast for click detection, unused
	/*bool Clicked() {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			if(hitInfo)
			{
				Debug.Log( hitInfo.transform.gameObject.name );
				return hitInfo.transform.gameObject == this;
			}
		} return false;
	}*/
}
