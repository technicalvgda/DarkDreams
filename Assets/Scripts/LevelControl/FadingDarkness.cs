/* Fades the alpha value of a Canvas Group.
 * The Alpha range is between 0 and 1.
 */

using System.Collections;
using UnityEngine;

public class FadingDarkness : MonoBehaviour
{
    CanvasGroup canvasGroup;
    CanvasGroup canvas1;
    CanvasGroup canvas2;
    CanvasGroup canvas3;
    CanvasGroup canvas4;
    CanvasGroup canvas5;

    float fadeSpeed;
    private float hiddenFadeSpeed; //fade speed when hidden
    public bool playerHidden = false; // bool that finds if player is hidden or not, also calls script from playerControl

    // calls stuff from other scripts [declaring]
    PlayerControl playerScript;
    PlayerControl fadingScript;


    void Awake()
    {
        // a lot of information gained from other scripts mainly the CanvasGroup and PlayerControl
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        fadingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvas1 = GameObject.FindGameObjectWithTag("Canvas1").GetComponent<CanvasGroup>();
        canvas2 = GameObject.FindGameObjectWithTag("Canvas2").GetComponent<CanvasGroup>();
        canvas3 = GameObject.FindGameObjectWithTag("Canvas3").GetComponent<CanvasGroup>();
        canvas4 = GameObject.FindGameObjectWithTag("Canvas4").GetComponent<CanvasGroup>();
        canvas5 = GameObject.FindGameObjectWithTag("Canvas5").GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // sets the CanvasGroup alpha to 0
        canvasGroup.alpha = 0;
        // sets the fadeSpeed to 10f
        fadeSpeed = 0.05f;
        hiddenFadeSpeed = fadeSpeed * 2; //declares the hiddenFadeSpeed
    }

    void Update()
    {
        // this is where the magic happens
        if(fadingScript.canvas1)
        {
            if (canvas1.alpha <= 0.85) //stops the fade at 0.85
            {

                if (playerScript.hide) //when player is hidden...
                {
                    canvas1.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvas1.alpha += Time.deltaTime * fadeSpeed; //default fade
                }
            }
        }
        if (fadingScript.canvas2)
        {
            canvas1.alpha = 0;  // after leaving canvas 1, canvas1 alpha goes to zero
            if (canvas2.alpha <= 0.85) //stops the fade at 0.85
            {

                if (playerScript.hide) //when player is hidden...
                {
                    canvas2.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvas2.alpha += Time.deltaTime * fadeSpeed; //default fade
                }
            }
        }
        if (fadingScript.canvas3)
        {
            canvas2.alpha = 0; // leaving canvas 2 will reset the canvas2 alpha to 0
            if (canvas3.alpha <= 0.85) //stops the fade at 0.85
            {

                if (playerScript.hide) //when player is hidden...
                {
                    canvas3.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvas3.alpha += Time.deltaTime * fadeSpeed; //default fade
                }
            }
        }
        if (fadingScript.flashingRoom)
        {
            canvas3.alpha = 0; // different than the other 2 since this doesn't have a canvas, its a flickering room, but anyway
            // by leaving canvas3 the alpha resets.
        }
        if (fadingScript.canvas4)
        {
            if (canvas4.alpha <= 0.85) //stops the fade at 0.85
            {
                //doesnt have the reset because you were leaving the flickering room
                if (playerScript.hide) //when player is hidden...
                {
                    canvas4.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvas4.alpha += Time.deltaTime * fadeSpeed; //default fade
                }
            }
        }
        if (fadingScript.canvas5)
        {
            if (canvas5.alpha <= 0.85) //stops the fade at 0.85
            {
                canvas4.alpha = 0; //same as the top 4
                if (playerScript.hide) //when player is hidden...
                {
                    canvas5.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
                }
                else
                {
                    canvas5.alpha += Time.deltaTime * fadeSpeed; //default fade
                }
            }
        }


    }
}