using UnityEngine;
using System.Collections;

public class patrola2 : MonoBehaviour 
{
	public Transform point1;
	Vector3 start;
	Vector3 end;
	private float speed = .05f;
	bool x = true;
	int counter = 0;

	// Use this for initialization
	void Start () 
	{	 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counter == 0) 
		{
			start = new Vector3(point1.position.x - 5.0f, point1.position.y, point1.position.z);
			end = new Vector3 ( point1.position.x + 5.0f, point1.position.y, point1.position.z);
		}

		if (point1.position == end) 
		{
			x = false;
		} 
		else if (point1.position == start) 
		{
			x = true;
		}
		if (point1.position == end || x == false) 
		{
			transform.position = Vector3.MoveTowards (point1.position, start, speed);
			counter = 1;
		} 
		else if (point1.position != end) 
		{
			transform.position = Vector3.MoveTowards (point1.position, end, speed);
			counter = 1;
		}

	}
}
