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
    Vector3 offset = new Vector3(0, -2, 0);
    //reference to camera
    CameraFollowScript cameraScript;
    float clickOffsetY = 5;
    float clickOffsetX = 5;
    public Sprite up, down;
    public GameObject boardedDoor;
    PlayerControl player;
    GameObject playerObj;
    bool playerContact = false;

    //Check if the player goes through the door for nightmare tower generation
    public bool used = false;

    Animator anim;
    // Use this for initialization

    void Start()
    {
        clickPosition = new Vector2(0f, 0f);
        cameraScript = Camera.main.GetComponent<CameraFollowScript>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerControl>();

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

        if (exit.gameObject.name.Equals("AtticDoor"))
        {
            boardedDoor = GameObject.FindGameObjectWithTag("BoardedDoor");
            boardedDoor.transform.position = this.transform.position;
        }




        /*
                //if this door leads up
                if (exitlock.position.y > transform.position.y)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = up;
                }
                //if door leads down
                else if (exitlock.position.y < transform.position.y)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = down;
                }
        */



    }
    void Update()
    {

        if ((playerContact == true && ((Input.GetKeyDown(KeyCode.Space)) && player.canHide)))
        {
            /*			if(TutorialDoorCheck.islocked == false){
                            cameraScript.follow = false;
                            cameraScript.target = exitlock.transform;

                        }
                   */
            if (exit != null)
            {
                if (exit.gameObject.name.Equals("AtticDoor"))
                {
                    //if player has collected all 5 items
                    if (player.itemCounter == 5)
                    {
                        cameraScript.follow = false;
                        cameraScript.target = exit.transform;
                    }
                }
                else
                {
                    cameraScript.follow = false;
                    cameraScript.target = exit.transform;
                }


            }
            else
            {
                Debug.Log("no exit assigned");
            }

            Debug.Log("Teleport Complete!"); // confirm that teleport is complete; this can be taken out
            TeleportToExit2D(playerObj);
        }
        ///handles click to hide
        else if (playerContact == true && Input.GetMouseButtonDown(0) && player.canHide)
        {

            RaycastHit2D[] hits;
            hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100);
            for (int i = 0; i < hits.Length; i++)
            {

                RaycastHit2D hit = hits[i];
                if (hit.collider.tag == "Door" || hit.collider.tag == "Lock")
                {
                    if (exit != null)
                    {
                        if (exit.gameObject.name.Equals("AtticDoor"))
                        {
                            //if player has collected all 5 items
                            if (player.itemCounter == 5)
                            {
                                cameraScript.follow = false;
                                cameraScript.target = exit.transform;
                            }
                        }
                        else
                        {
                            cameraScript.follow = false;
                            cameraScript.target = exit.transform;
                        }


                    }
                    else
                    {
                        Debug.Log("no exit assigned");
                    }
                    Debug.Log("Teleport Complete!"); // confirm that teleport is complete; this can be taken out
                    TeleportToExit2D(playerObj);
                }

            }
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            playerContact = true;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("Player not touching");
            playerContact = false;

        }
    }
    /*
        void OnTriggerStay2D(Collider2D col)
        {

            //used to make an offset that creates an area to click on, which can be increased/decreased by changing the constant.
            float xNegPosition = transform.position.x - clickOffsetX;
            float xPosPosition = transform.position.x + clickOffsetX;
            float yPosPosition = transform.position.y + clickOffsetY;
            float yNegPosition = transform.position.y - clickOffsetY;

            ///get position of click
            clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;  
            ///click can is on door
            if ((col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) || 
               ((yNegPosition<clickPosition.y && clickPosition.y<yPosPosition)&& 
                (xNegPosition<clickPosition.x && clickPosition.x<xPosPosition)&&
                Input.GetMouseButtonDown(0))))&&col.GetComponent<PlayerControl>().canHide)
            {    

               if (exit != null)
                {
                    if (exit.gameObject.name.Equals("AtticDoor"))
                    {
                        //if player has collected all 5 items
                        if (player.itemCounter == 5)
                        {
                            cameraScript.follow = false;
                            cameraScript.target = exit.transform;
                        }
                    }
                    else
                    {
                        cameraScript.follow = false;
                        cameraScript.target = exit.transform;
                    }


                }
                else
                {
                    Debug.Log("no exit assigned");
                }

                Debug.Log("Teleport Complete!"); // confirm that teleport is complete; this can be taken out
                TeleportToExit2D(col);
            }

        }
    */
    void TeleportToExit2D(GameObject col)
    {


        /*	if(TutorialDoorCheck.islocked == false){
                col.transform.position = exitlock.transform.position;

            }
          */

        if (exit != null)
        {
            if (exit.gameObject.name.Equals("AtticDoor"))
            {
                //if player has collected all 5 items
                if (player.itemCounter == 5)
                {
                    player.hide = true;
                    if (col.transform.position == exit.transform.position)
                    {
                        used = false;
                    }
                    else
                    {
                        used = true;
                    }
                    col.transform.position = exit.transform.position + offset; //line that teleports player
                    player.hide = false;
                }
            }
            else
            {
                player.hide = true;
                if (col.transform.position == exit.transform.position)
                {
                    used = false;
                }
                else
                {
                    used = true;
                }
                col.transform.position = exit.transform.position + offset; //line that teleports player
                player.hide = false;
            }
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