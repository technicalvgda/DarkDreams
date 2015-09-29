using UnityEngine;
using System.Collections;

/* The purpose of this script is to create a flickering effect that will be layered over the level.
 * This script is to be inserted into a Canvas UI.

 * Instructions:
 * 1) In Unity, right click the a blank space in the Hierarchy section (where all the objects in the scene are placed) and select UI > Image
 * 2) Click the Image object. Resize the image so that it covers the desired scene where you would like the flashing darkness effect.
 * 3) In the inspector tab, locate "Image(Script)" and change the color option from white to black.
 * 4) In the same inspector tab, click "Add Component" and add "Canvas Group". 
 * 5) Select the "Alpha" option in Canvas Group and change the value from 1 to 0. This will make the image "transparent" at the beginning.
 * 6) Now, select "Canvas" on the Hierarchy section (NOTE: Do not select the Image object!) and look right at its Inspector tab.
 * 7) The Inspector Tab will have a "Canvas" drop down; locate "Render Mode".
 * 8) In render mode, change from "Screen Space - Overlay" to "World Space."
 * 9) Drag this script into the the Image object. Run the game!
 */

public class FlashingDarkness : MonoBehaviour {
    CanvasGroup cGroup;
    public float waitTime = 0.0001f;
    
    void Start()
    {
        //create a Canvas group object, needed to change the alpha value
        cGroup = gameObject.GetComponent<CanvasGroup>();
     
        InvokeRepeating("Flash", 0f, waitTime);
    }

	

    void Flash()
    {
        //generate a random float number from 0.0 to 1.0
        float alphaValue = Random.Range(0.0f, 1.0f);

        //set the random generated number to the alpha value of the Canvas Group
        cGroup.alpha = alphaValue;

        //Debug.Log ("Random #: " + alphaValue); //confirms and prints value of the random generated number. WARNING! This will print A LOT of values. This is removable.
    }
}