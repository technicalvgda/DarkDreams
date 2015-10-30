using UnityEngine;
using System.Collections;

public class EarthquakeHallucination : MonoBehaviour {
	private Vector3 roomPos;
	private Sprite roomSprite;
	private float roomLeftEdge, roomRightEdge, roomTop,roomBot;
	private GameObject player;
	private GameObject cam;
	private Vector3 camOriginalPos;
	private Transform camTransform;
	private Vector3 newCamPosition;
	public float shakeIntensity = 0.5f;
	// Use this for initialization
	void Start () 
	{
		//Find the player
		player = GameObject.FindGameObjectWithTag ("Player");
		//Find the main camera
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		//Get the transform of the camera
		camTransform = cam.GetComponent<Transform> ();
		//Get world position of room
		roomPos = gameObject.transform.position;
		//Get the sprite of the room
		roomSprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
		//Calculate the left edge of the room
		roomLeftEdge = roomPos.x + (roomSprite.bounds.min.x * gameObject.transform.localScale.x);
		//Calculate the right edge of the room
		roomRightEdge = roomPos.x + (roomSprite.bounds.max.x * gameObject.transform.localScale.x);
		//Calculate the top edge of the room
		roomTop = roomPos.y + (roomSprite.bounds.max.y * gameObject.transform.localScale.y);
		//Calculate the bottom edge of the room
		roomBot = roomPos.y + (roomSprite.bounds.min.y * gameObject.transform.localScale.y);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//Get the original position of the camera
		camOriginalPos = cam.transform.localPosition;
		//Set the new camera position to the original
		newCamPosition = camOriginalPos;
		//Keep the camera facing the game
		newCamPosition.z = -10f;
		//If the player is within the bounds of the room...
		if (player.transform.position.x > roomLeftEdge && player.transform.position.x < roomRightEdge && 
		    player.transform.position.y > roomBot && player.transform.position.y < roomTop) 
		{
			//Calculate the X position of the next camera position
			newCamPosition.x = camOriginalPos.x + (Random.insideUnitSphere.x*shakeIntensity);
			//Calculate the Y position of the next camera position
			newCamPosition.y = camOriginalPos.y + (Random.insideUnitSphere.y*shakeIntensity);
			//Move the camera to the new location
			camTransform.localPosition = newCamPosition;
		}
	}
}