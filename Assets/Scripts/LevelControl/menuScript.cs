using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {
	
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
    float brightnessLevel;
    float musicVolume;
    float sfxVolume;
    // Use this for initialization
    void Start ()
	{
        //Initialize variables for playerprefs (if they dont exist)
        PlayerPrefs.SetFloat("Brightness", 0);
        PlayerPrefs.SetFloat("Music", 0);
        PlayerPrefs.SetFloat("SFX", 0);
        //set variables to proper level stored in PlayerPref variables
        brightnessLevel = PlayerPrefs.GetFloat("Brightness");
        musicVolume = PlayerPrefs.GetFloat("Music");
        sfxVolume = PlayerPrefs.GetFloat("SFX");

        quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
		quitMenu.enabled = false;
		
		
	}
	
	
	public void ExitPress()
		
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}
	
	public void NoPress()
		
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
		
	}
	
	public void StartLevel()
		
	{
		Application.LoadLevel(1);
	}
	
	public void ExitGame()
		
	{
		Application.Quit();
	}
	
	
}
