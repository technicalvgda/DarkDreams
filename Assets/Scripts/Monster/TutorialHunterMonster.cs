using UnityEngine;
using System.Collections;

public class TutorialHunterMonster : MonoBehaviour {
	
	
	
	
	
	Transform player;
	public float moveSpeed;
	
	private Transform myTransform;
	
	
	
	// Use this for initialization
	void Start()
	{
        myTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void FixedUpdate()
	{
		Debug.DrawLine(player.position, myTransform.position, Color.blue);
	}
	
	// Update is called once per frame
	void Update()
	{
		if (player.position.y > myTransform.position.y) 
		{
			if (player.position.x < myTransform.position.x) 
			{
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
			} 
			else if (player.position.x > myTransform.position.x) 
			{
				myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
			}
		}
		
	}
}


