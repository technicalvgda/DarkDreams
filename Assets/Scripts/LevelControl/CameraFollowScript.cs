using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{
	
	private Transform player;//our player character
	public float transitionDuration = 2.0f;
	public Transform target;
	public bool follow = true;
	public float yOffset = 7;
	void Awake ()//on start up
	{
		//finds the gameobject with the player tag and stores its transform component
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	void Update ()//for every frame:
		{
		if (follow == true) 
		{
			//stores a vector 2 which contains the player's x and y position
			Vector2 cameraPosition = new Vector2 (player.position.x, player.position.y + yOffset);//define cameraPosition as player position
			//sets the position of this transform (the camera) to the x and y position of the stored position, does not change z position
			transform.position = new Vector3 (cameraPosition.x, cameraPosition.y, transform.position.z);//this transform (camera game object)
		} 
		else
		{
			StartCoroutine (Transition ());
			StartCoroutine (Transition2());
		}
	}//position is camera position

	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 2.5f) 
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);
			Vector3 endPos = new Vector3 (target.position.x, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp (startingPos, endPos, t);
			yield return 0;
		}
	}
	IEnumerator Transition2()
	{
		float t = 0.0f;
		Vector3 endPos = transform.position;
		while (t<2.5f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);
			Vector3 playerPosition = new Vector3 (player.position.x, player.position.y + yOffset, transform.position.z);
			transform.position = Vector3.Lerp (endPos, playerPosition, t);
			yield return 0;
		}
		
	}
	
}
