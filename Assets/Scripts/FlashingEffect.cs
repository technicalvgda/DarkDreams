using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlashingEffect : MonoBehaviour {
	//Affects the UI elemnents of the canvas
	CanvasGroup flash;
	//Sets the speed of the blinking
	public float flashSpeed=0f;
	void Start()
	{
		
	}
	void Update()
	{
		//Creating the actual canvas that allows it to be manipulated through the CanvasGroup
		//Added a CanvasGroup to the Canvas UI element in UI
		CanvasGroup canvas=GetComponent<CanvasGroup>();
		//If the alpha is less than 1, then increase the darkness of the flash
		if (canvas.alpha < 1)
		{
			canvas.alpha=canvas.alpha + (Time.deltaTime * flashSpeed);
			Debug.Log ("Flashing Up");
		}
		//If the alpha is greater than or equal to 1, then decrease the darkness of flash
		if(canvas.alpha >= 1)
		{
			canvas.alpha=canvas.alpha - (Time.deltaTime * flashSpeed * 5);
			Debug.Log ("Flashing Down");             
		}     
	}
}