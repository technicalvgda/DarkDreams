using UnityEngine;
using System.Collections;

public class BeatGameScript : MonoBehaviour 
{
	public GameObject player;
	Vector2 clickPosition;
	//CameraFollowScript cameraScript;
	float clickOffsetY = 1;
	float clickOffsetX = 1;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		clickPosition = new Vector2(0f, 0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerStay2D(Collider2D other)
	{
		
		float xNegPosition = transform.position.x - clickOffsetX;
		float xPosPosition = transform.position.x + clickOffsetX;
		float yPosPosition = transform.position.y + clickOffsetY;
		float yNegPosition = transform.position.y - clickOffsetY;
		
		///get position of click
		clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		
		if (Input.GetKeyDown(KeyCode.Space) || ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
		                                        (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) && Input.GetMouseButtonDown(0)))
		{
			if (other.GetComponent<PlayerControl>() == null)
			{
				return;
			}
			StartCoroutine(_BeatGame ());
		}
	}
	//The cutscene
	IEnumerator _BeatGame()
	{
		Debug.Log ("You beat the game!");
		//Make it so player can't move
		player.GetComponent<PlayerControl> ().normalSpeed = 0;
		//Wait a few seconds
		yield return new WaitForSeconds (3f);

		//Load the credits level
		Application.LoadLevel ("EndCreditsScene");
		yield return null;
	}
}
