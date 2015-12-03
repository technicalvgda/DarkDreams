/* Fades the alpha value of a Canvas Group.
 * The Alpha range is between 0 and 1.
 */

using System.Collections;
using UnityEngine;

public class FadingDarkness : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float fadeSpeed;
    private float hiddenFadeSpeed; //fade speed when hidden
    public bool playerHidden = false; // bool that finds if player is hidden or not, also calls script from playerControl
    
    GameObject player;
    Transform playerPos;
    PlayerControl playerScript;
    bool fade = false;
    public bool flash = false;
    bool flashActive = false;



    float waitTime = 0.1f;///0.0001f

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.GetComponent<Transform>();
        playerScript = player.GetComponent<PlayerControl>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        
        // sets the CanvasGroup alpha to 0
        canvasGroup.alpha = 0;
        // sets the fadeSpeed to 10f
        fadeSpeed = 0.01f;
        hiddenFadeSpeed = fadeSpeed * 2; //declares the hiddenFadeSpeed
    }

    void Update()
    {

        if (fade)
        {
            //Debug.Log("fading");
            if (canvasGroup.alpha <= 0.85) //stops the fade at 0.85
            {

                if (playerScript.hide) //when player is hidden...
                {
                    canvasGroup.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvasGroup.alpha += Time.deltaTime * fadeSpeed; //default fade
                }

            }
        }
        if(flash == true)
        {
           fade = false;
           canvasGroup.alpha = 0;
            if(flashActive == false)
            {
                Debug.Log("flash");
                flashActive = true;
                InvokeRepeating("Flash", 0f, waitTime);

            }
           
           flash = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //turn on fading
            fade = true;
			playerScript.fadingDarknessScript = this;

            
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //turn off fading
            fade = false;
            //reset darkness
            //canvasGroup.alpha = 0;
           
        }

    }
    void Flash()
    {
        // If this object is not on the same elevation as the player, do nothing
        if (this.transform.position.y < playerPos.position.y - 10 ||
            this.transform.position.y > playerPos.position.y + 40)
        {
            //generate a random float number from 0.0 to 1.0
            float alphaValue = Random.Range(0.0f, 0.4f);

            //set the random generated number to the alpha value of the Canvas Group
            canvasGroup.alpha = alphaValue;
        }
        //Debug.Log ("Random #: " + alphaValue); //confirms and prints value of the random generated number. WARNING! This will print A LOT of values. This is removable.
    }
}