using UnityEngine;
using System.Collections;

public class FadingDarknessMechanic : MonoBehaviour {

    // code helped from Wilburn's

    CanvasGroup canvasGroup; //gets the canvas group from canvas
    public float fadeSpeed = 0f;
    private float hiddenFadeSpeed; //fade speed when hidden
    public bool playerHidden = false; // bool that finds if player is hidden or not, also calls script from playerControl

    

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>(); //initializes the CanvasGroup
        hiddenFadeSpeed = fadeSpeed * 2; //declares the hiddenFadeSpeed

    }

	// Use this for initialization
	void Start () {
        canvasGroup.alpha = 0; //starts alpha at zero
      
        
        

       	}
	
	// Update is called once per frame
	void Update () {
	    if(canvasGroup.alpha <= 0.75 ) //stops the fade at 0.75
        {
            
            if(playerHidden) //when player is hidden...
            {
                canvasGroup.alpha += Time.deltaTime * hiddenFadeSpeed; //fade speed is multiplied
            }
            else
            {
                canvasGroup.alpha += Time.deltaTime * fadeSpeed; //default fade
            }
            
        }        
	} 
}
