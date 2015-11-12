using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*This script is to be placed inside "item" objects. It's purpose is to add the score into the player script.
 * 
 * 
 * Instructions: 
 1)  Drag this script in your Scripts folder in Unity.
 2)  Create an object. This will be an "item" the player can "pick up."
 3)  In the inspector tab, add a "Box Collider 2D" component and check "Is Trigger" option.
 4)  Drag this script to your item object.
 5)  In the inspector tab, locate your "Item Pickup(Script)" script and find the option "Item Add".
 6)  Insert the amount you wish to add (ideally this will be 1) and hit enter.
 7)  Open your PlayerControl script and add this code into it:

//to count item pickups
public static int itemCounter;

// adds points in  the Player Script
public static void AddPoints (int itemAdd){
	itemCounter += itemAdd; //adds amount to current score
	Debug.Log ("Score: " + itemCounter); //confirms the player has picked up the object (track amount). this is removeable.
}


 * 8) Save both your PlayerControl script and ItemPickup script. Run the game!
 * Note: In the event that you are not allowed or cannot see the "Item Add" option, try skipping to step 7 first and include the code that will be placed into the PlayerContol script.
 */


public class ItemPickup : MonoBehaviour {

	public int itemAdd; // creates counter that can be passed to player control; add amount in inspector
	//public GameObject canvas;
	public bool flash = false;
    public bool textActive = false;
	public FadingDarkness fadingDarkness;
	PlayerControl playerScript;
    PauseScript pause;
    //public GameObject flashingCanvas;
   
    private GameObject itemTextPanel;
    Vector2 clickPosition;
  
    float clickOffsetY = 1;
    float clickOffsetX = 1;
    
    void Start()
    {
        pause = Camera.main.GetComponent<PauseScript>();
        // player = GameObject.Find("Player").GetComponent<PlayerControl>();
        itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;//GameObject.Find("ItemTextPanel");
        itemTextPanel.SetActive(false);
        clickPosition = new Vector2(0f, 0f);
        //cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }
    void OnTriggerStay2D (Collider2D other) {

		playerScript = other.GetComponent<PlayerControl>();
		fadingDarkness = playerScript.fadingDarknessScript;

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
            if(textActive == true)
            {
                //Time.timeScale = 1;
                pause.busy = false;
                PlayerControl.AddPoints(itemAdd); //will add the amount in player script
                Destroy(gameObject); //destroys the object
            }
            itemTextPanel.SetActive(true);
            pause.busy = true;
            //Time.timeScale = 0;
            flash = true;
            textActive = true;
            //Destroy(gameObject); //destroys the object
            //canvas = GameObject.Find ("FadingDarknessCanvas");
            //fadingDarkness = GameObject.FindWithTag(FadingDarkness);
            if (fadingDarkness == null)
            {
                Debug.Log("not found");
            }
            else
            {
                fadingDarkness.flash = true;
            }
        }
        

		

       

		//flashingCanvas = GameObject.Find("FlashingLightCanvas")();
		//Instantiate(flashingCanvas);


	


	}
}	