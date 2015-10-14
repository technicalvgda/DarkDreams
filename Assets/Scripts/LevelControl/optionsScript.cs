using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class optionsScript : MonoBehaviour 
{
	public Slider brightness;
	public Slider bgm;
	public Slider se;

	public void ChangeScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

	// Changes the ambient light according to slider position.
	// A sprite needs its Material set to Diffuse to be affected by ambient light (see "BG" object).
	// Not sure if this is the optimal method since it requires changing the material of a lot of sprites.

	// Only changes the lighting within the Options menu.
	// This value probably needs to be carried into the main scene somehow.
	
	public void ChangeBrightness()
	{
		// Take the value of the slider
		float gamma = brightness.value;

		// Feed it to the ambient light
		RenderSettings.ambientLight = new Color (gamma, gamma, gamma, 1);
	}

	// TODO: No idea how audio is handled in this game at the moment.

	public void ChangeBGM()
	{
	}

	public void ChangeSE()
	{
	}

	public void ToggleSound()
	{
		bgm.interactable = !bgm.interactable;
		se.interactable = !se.interactable;
	}
}
