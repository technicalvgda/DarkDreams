using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightBulbItemPickup : MonoBehaviour
{

    AudioHandlerScript audioHandler;
    public int dialogueToPlay;
    public int itemAdd; // creates counter that can be passed to player control; add amount in inspector
                        //public GameObject canvas;
    public bool flash = false;
    public bool textActive = false;
    private bool playerTouching = false;
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
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        pause = Camera.main.GetComponent<PauseScript>();
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        // player = GameObject.Find("Player").GetComponent<PlayerControl>();
        //itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;//GameObject.Find("ItemTextPanel");
        //itemTextPanel.SetActive(false);
        clickPosition = new Vector2(0f, 0f);
        //cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }
    void Update()
    {
        //if player is overlapping item
        if (playerTouching == true)
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

                fadingDarkness = playerScript.fadingDarknessScript;
                
                    playerScript.AddPoints(itemAdd); //will add the amount in player script
                    Destroy(gameObject); //destroys the object
                    flash = true;
                    if (dialogueToPlay != -1)
                    {
                        audioHandler.PlayVoice(dialogueToPlay);
                    }
                   

                }
                if (fadingDarkness == null)
                {
                    Debug.Log("not found");
                }
                else
                {
                    fadingDarkness.flash = true;
                }



            
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerTouching = true;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerTouching = false;
        }
    }
    /*
    void OnTriggerStay2D (Collider2D other)
    {


		//playerScript = other.GetComponent<PlayerControl>();
       
		

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
            fadingDarkness = playerScript.fadingDarknessScript;
            if (textActive == true)
            {
                //Time.timeScale = 1;
                pause.busy = false;
                playerScript.AddPoints(itemAdd); //will add the amount in player script
                Destroy(gameObject); //destroys the object
            }
            else
            {


                itemTextPanel.SetActive(true);
                pause.busy = true;
                flash = true;
                textActive = true;
                audioHandler.PlayVoice(dialogueToPlay);
                textPause = true;
                Time.timeScale = 0;
                
            }
            
            
            
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
        
	}
    */


}
