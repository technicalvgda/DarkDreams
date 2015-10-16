

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



    // Use this for initialization
    void Awake()
    {
        clickPosition = new Vector2(0f, 0f);
        cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        ///get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        ///currently, click must be on player
        if (col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) || (col.OverlapPoint(clickPosition) && Input.GetMouseButtonDown(0))))
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
            col.transform.position = exit.transform.position; //line that teleports player
        }
        else
        {
            Debug.Log("no exit assigned");
        }

	}
}