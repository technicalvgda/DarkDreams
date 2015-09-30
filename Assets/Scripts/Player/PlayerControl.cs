﻿using UnityEngine;
using System.Collections;


public class PlayerControl : MonoBehaviour 
{
    public const float EDGEBUFFER = 0.05f; // Percentage of screen to validate mouse click/mobile tap
    // for edge detection
    Vector2 edgeLeft;
    Vector2 edgeRight;
    Vector2 screenWidth;

    // For click/tap detection
    Vector2 clickPosition;

    // for references to player
    public bool isAlive = true;
    private SpriteRenderer sprite;
    // for player movement
    Vector2 movement;
    private bool facingRight = true;
    //speed variables
    public float playerSpeed;               // final magnitude of speed, the player's speed
    public bool slowMo;                     //boolean that toggles slow motion
    public float normalSpeed = 10.0f;       //normal speed magnitude
    public float slowMoSpeed = 5.0f;        //speed magnitude when slowMo is activaed

    //point variables
    public static int itemCounter;//to count item pickups

    //hiding variables
    public bool hide = false;
    int hidingOrder = 0;//sorting layer when hidden
    int sortingOrder = 2;//sorting layer normally

    




    // Use this for initialization
    void Awake()
    {
        clickPosition = new Vector2(0f, 0f);
        screenWidth = new Vector2((float)Screen.width, 0f);
        edgeLeft = new Vector2(screenWidth.x * EDGEBUFFER, 0f);
        edgeRight = new Vector2(screenWidth.x - edgeLeft.x, 0f);
        sprite = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start() //what happens as soon as player is created
    {
        
        slowMo = false;  //slowMo starts out as false since the player hasn't hit the button yet
        

	}
    void Update()
    {
        //code for sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //it shift key is hit, players speed is 2 times the speed
            playerSpeed = normalSpeed * 2;
            print("shift key was pressed");
        }/* 
        else
        {
            // if not then normal speed resumes
            playerSpeed = normalSpeed;
        }
            */
    }
    void LateUpdate()
    {
        //handles player movement based upon mouse clicks (or taps)
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < edgeLeft.x)
                Move(-playerSpeed);
            else if (Input.mousePosition.x > edgeRight.x)
                Move(playerSpeed);
        }
        //handles player movement based upon keyboard input
        else
        {
           //calls move, sends a value of the speed multiplied by the axis (which will either be -1, 0, or 1)
            Move(Input.GetAxis("Horizontal")*playerSpeed);
        }
        
        ///code for slow motion movement
        if (Input.GetKeyDown(KeyCode.E)) //when the player presses the "e" key, it toggles slowMo
        {
            slowMo = !slowMo;
            Debug.Log("toggle");  //so we can check how many times it toggles per keyhit, it is temporary
        }
        if (slowMo) //when slowMo is true, the player will move at half speed
        {
            playerSpeed = slowMoSpeed;
        }

        else  //when slowMo is false, the player will move normally
        {
            playerSpeed = normalSpeed;
        }

    }
   
    void Move(float h)
    {
        //prevent player from moving when hidden
        if (!hide)
        {
            // Set movement and normalize in terms of time passed from previous frame
            // (Assuming we will be frame rate dependent)
            movement.x = h;
            movement *= Time.deltaTime;

            // apply movement to player
            transform.Translate(movement);
            //this checks which direction the player is moving and flips the player based upon that
            if (movement.x > 0 && facingRight == false)
            {
                FlipPlayer();
            }
            if (movement.x < 0 && facingRight == true)
            {
                FlipPlayer();
            }

        }

    }
    //handle collisions with level objects
    void OnTriggerEnter2D(Collider2D col)
    {
        //if player colliders with an enemy and is not hidden
        if (col.gameObject.tag == "PatrolEnemy" && hide == false)
        {
            //player is dead
            isAlive = false;
            //prevent player from moving
            normalSpeed = 0f;
        }
    }
    //allows actions when staying within collision area
    void OnTriggerStay2D(Collider2D col)
    {
        // OverlapPoint refers to world space instead of screen space, adjusting accordingly
        clickPosition.x = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        clickPosition.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
       
       
        //Toggle Hide/Unhide
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)&& col.OverlapPoint(clickPosition))))
        {
            if (col.gameObject.tag == "Cover")
            {
                if (!hide)
                {
                    sprite.sortingOrder = hidingOrder;
                    hide = true;
                }
                else if (hide)
                {
                    sprite.sortingOrder = sortingOrder;
                    hide = false;

                    if (slowMo) //Disables slowmotion speed upon hiding
                    {
                        slowMo = false;
                    }
                }
            }
               
          }     
        
        //if player colliders with an enemy and is not hidden
        if (col.gameObject.tag == "PatrolEnemy" && hide == false)
        {
            //player is dead
            isAlive = false;
            //prevent player from moving
            normalSpeed = 0f;
        }
       ///gameover for stationary enemies handled in their own code
    }
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    // adds points in  the Player Script
    public static void AddPoints(int itemAdd)
    {
        itemCounter += itemAdd; //adds amount to current score
        Debug.Log("Score: " + itemCounter); //confirms the player has picked up the object (track amount). this is removeable.
    }
}