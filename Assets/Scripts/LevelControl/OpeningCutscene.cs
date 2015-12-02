using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpeningCutscene : MonoBehaviour
{
	public GameObject hunterEnemy;
	public GameObject wall;
	float wallMargin;
	const float wallOffset = 20;

	GameObject player;
    PlayerControl playerScript;
	GameObject cam;
    PauseScript pause;

    // Use this for initialization
    void Start ()
	{
        
            Setup();
            StartCoroutine("_Cutscene");
        
       
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void Setup()
	{
        
        player = GameObject.FindGameObjectWithTag ("Player");
        cam = Camera.main.gameObject;//GameObject.FindGameObjectWithTag ("MainCamera");
        pause = Camera.main.GetComponent<PauseScript>();
        pause.busy = true;
        // Move the player to the center
        if (Application.loadedLevelName != "Tutorial Stage")
        {
            player.transform.position = new Vector3(143, player.transform.position.y, 0);
        }
        //prevent player from moving until end of cutscene
        playerScript = player.GetComponent<PlayerControl>();
        playerScript.normalSpeed = 0f;
        // Figure out where the left wall is so the camera's panning can stop there
        wallMargin = wall.transform.position.x + wallOffset;
	}
	public void EndCutscene()
    {
        StopCoroutine("_Cutscene");
    }
	IEnumerator _Cutscene()
	{
        //how long to wait until cutscene begins
		yield return new WaitForSeconds (9.5f);

		// Lock the camera once it finishes positioning itself
		cam.GetComponent<CameraFollowScript> ().enabled = false;

		yield return new WaitForSeconds (1);

		// Pan camera to left until it hits the wall
		while (cam.transform.position.x > wallMargin)
		{
			cam.transform.position += new Vector3(-0.2f, 0, 0);
			yield return null;
		}

		yield return new WaitForSeconds (1);

		// move the hunter
		hunterEnemy.SetActive (true);

		yield return new WaitForSeconds (1.5f);

		// pan back to player
		while (cam.transform.position.x < player.transform.position.x)
		{
			cam.transform.position += new Vector3(0.4f, 0, 0);
			yield return null;
		}
		cam.GetComponent<CameraFollowScript> ().enabled = true;
        //prevent player from moving until end of cutscene
        playerScript.normalSpeed = playerScript.defaultSpeed;
        pause.busy = false;
        yield return new WaitForSeconds(10f);
        hunterEnemy.SetActive(false);
        yield return null;
	}
}
