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
    float voiceVolume;
    // Use this for initialization
    void Start ()
	{
        //Initialize variables for playerprefs (if they dont exist)
        if (!PlayerPrefs.HasKey("Brightness"))
        {
            PlayerPrefs.SetFloat("Brightness", 0);
        }
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 1);
        }
        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 1);
        }
        if (!PlayerPrefs.HasKey("Voice"))
        {
            PlayerPrefs.SetFloat("Voice", 1);
        }
        //set variables to proper level stored in PlayerPref variables
        brightnessLevel = PlayerPrefs.GetFloat("Brightness");
        musicVolume = PlayerPrefs.GetFloat("Music");
        sfxVolume = PlayerPrefs.GetFloat("SFX");
        voiceVolume = PlayerPrefs.GetFloat("Voice");

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
