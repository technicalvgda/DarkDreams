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
    PlayerControl playerScript;
    private object collision;

    void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        canvasGroup = GetComponent<CanvasGroup>();
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


        if (canvasGroup.alpha <= 0.85 && playerScript.gameObject.name == "Player") //stops the fade at 0.85
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

        else
        {
            canvasGroup.alpha = 0;
        }


    }

}