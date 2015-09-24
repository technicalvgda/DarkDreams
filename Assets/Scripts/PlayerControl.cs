using UnityEngine;
using System.Collections;


public class PlayerControl : MonoBehaviour 
{
    public const float EDGEBUFFER = 0.05f; // Percentage of screen to validate mouse click/mobile tap
    // for edge detection
    Vector2 edgeLeft;
    Vector2 edgeRight;
    Vector2 screenWidth;

    // for references to player
 
    private SpriteRenderer sprite;
    // for player movement
    Vector2 movement;

    //speed variables
    public float playerSpeed;               // final magnitude of speed, the player's speed
    public bool slowMo;                     //boolean that toggles slow motion
    public float normalSpeed = 10.0f;       //normal speed magnitude
    public float slowMoSpeed = 5.0f;        //speed magnitude when slowMo is activaed

    //hiding variables
    public bool hide = false;
    int hidingOrder = 0;//sorting layer when hidden
    int sortingOrder = 2;//sorting layer normally

    //panels
    public GameObject gameOverPanel;




    // Use this for initialization
    void Awake()
    {
        screenWidth = new Vector2((float)Screen.width, 0f);
        edgeLeft = new Vector2(screenWidth.x * EDGEBUFFER, 0f);
        edgeRight = new Vector2(screenWidth.x - edgeLeft.x, 0f);
        sprite = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start() //what happens as soon as player is created
    {
        gameOverPanel.SetActive(false);
        slowMo = false;  //slowMo starts out as false since the player hasn't hit the button yet
        

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

        else  //when slowMo is false, the player will move normaly
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
        }
        
    }
    //handle collisions with level objects
    void OnTriggerEnter2D(Collider2D col)
    {
        //if player colliders with an enemy and is not hidden
        if (col.gameObject.tag == "Enemy" && hide == false)
        {
            //activate game over panel
            gameOverPanel.SetActive(true);
            //prevent player from moving
            normalSpeed = 0f;
        }
    }
    //allows actions when staying within collision area
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cover")
        {
            //Toggle Hide/Unhide
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hide = !hide;
                //if player is hidden
                if (hide)
                {
                    sprite.sortingOrder = hidingOrder;
                }
                else
                {
                    sprite.sortingOrder = sortingOrder;
                }
                Debug.Log("Hide: " + hide);
            }     
        }
        //if player colliders with an enemy and is not hidden
        if (col.gameObject.tag == "PatrolEnemy" && hide == false)
        {
            //activate game over panel
            gameOverPanel.SetActive(true);
            //prevent player from moving
            normalSpeed = 0f;
        }
       ///gameover for stationary enemies handled in their own code

    }
}