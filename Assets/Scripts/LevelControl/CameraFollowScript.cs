using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{

	private Transform player;//our player character
    public float yOffset = 7;
	void Awake ()//on start up
	{
        //finds the gameobject with the player tag and stores its transform component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	void Update ()//for every frame:
	{
        //stores a vector 2 which contains the player's x and y position
		Vector2 cameraPosition = new Vector2(player.position.x, player.position.y + yOffset);//define cameraPosition as player position
        //sets the position of this transform (the camera) to the x and y position of the stored position, does not change z position
		transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);//this transform (camera game object)
	}//position is camera position
}