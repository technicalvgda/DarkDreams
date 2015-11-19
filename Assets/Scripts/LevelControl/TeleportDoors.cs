using UnityEngine;
using System.Collections;

/* This code teleports the player to the upper level via another door. This is a two-way door, so you can go back to the lower level. 
 Instructions: 
 - Create 2 door objects and name them (ex. Door1, Door2)
 - Add Component and create "Box Collider 2D" for both objects.
 - Check "Is Trigger" for both objects.
 - Insert this code into both objects.
 - Click on Door1. In the inspector tab, locate the "TeleportDoors (Script)" and find "Exit."
 - (If you can't find "Exit", be sure to check to see if the "Teleporter (Script)" is expanded)
 - Right now, it should say "None(Transform)". Click the circle on the right.
 - Look left to find the "Select Transsform" window. Select Door2 object.
 - Now, select Door2, locate "TeleportDoors (Script)" and find Exit, click the circle on the right and select Door1 object.
 - Test program. Your player should be able to teleport when "Enter" or "Return" is pressed. */

public class TeleportDoors : MonoBehaviour
{
	public Transform exit; //creates the "teleport" aspect and the Exit option in Inspector Tab.
    Vector2 clickPosition;
    //reference to camera
    CameraFollowScript cameraScript;
    float clickOffsetY = 5;
    float clickOffsetX = 5;
    public Sprite up, down;

	//Check if the player goes through the door for nightmare tower generation
	public bool used = false;

    Animator anim;
    // Use this for initialization
    void Awake()
    {
        clickPosition = new Vector2(0f, 0f);
        cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        //if this door leads up
        if (exit.position.y > transform.position.y)
        {
            transform.GetComponent<SpriteRenderer>().sprite = up;
        }
        //if door leads down
        else if (exit.position.y < transform.position.y)
        {
            transform.GetComponent<SpriteRenderer>().sprite = down;
        }
       
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if(exit == null)
        {
            //anim.SetTrigger("Inactive");
            anim.enabled = true;
        }
        //used to make an offset that creates an area to click on, which can be increased/decreased by changing the constant.
        float xNegPosition = transform.position.x - clickOffsetX;
        float xPosPosition = transform.position.x + clickOffsetX;
        float yPosPosition = transform.position.y + clickOffsetY;
        float yNegPosition = transform.position.y - clickOffsetY;
        
        ///get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;  
        ///click can is on door
        if (col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) || 
           ((yNegPosition<clickPosition.y && clickPosition.y<yPosPosition)&& 
            (xNegPosition<clickPosition.x && clickPosition.x<xPosPosition)&&
            Input.GetMouseButtonDown(0))))
        {    
            if (exit != null)
            {
                cameraScript.follow = false;
                cameraScript.target = exit.transform;
            }
            else
            {
                Debug.Log("no exit assigned");
            }
           
            Debug.Log("Teleport Complete!"); // confirm that teleport is complete; this can be taken out
            TeleportToExit2D(col);
        }
        /*
        else
        {
            //cameraScript.follow = true;//allows smooth transition for each teleport
        }
        */
    }

    void TeleportToExit2D ( Collider2D col )
	{
        if (exit != null)
        {
			if(col.transform.position == exit.transform.position) {
				used = false;
			}
			else {
				used = true;
			}
			col.transform.position = exit.transform.position; //line that teleports player

        }
        else
        {
            Debug.Log("no exit assigned");
        }
	}
    void LoadNewLevel(/*Dummy Variable for next level*/)
    {
        //int x = blah; Level+"x"
        Application.LoadLevel(Application.loadedLevel); //change loadedLevel to next level when appropiriate
    }
}