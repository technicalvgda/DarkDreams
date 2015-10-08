using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;



public class menuScript : MonoBehaviour {



    //private Settings m_Settings = new Settings();
    
    public float MusicVolume;
    public float SoundVolume;
    public float BrightnessS;

    public Transform Blackness;

    public bool SoundMute = true;



    public bool IsOptionOpen { get; set; }


    public Canvas optionMenu;
    public Canvas quitMenu;
	public Button startText;
    public Button optionText;
	public Button exitText;
	
	// Use this for initialization
	void Start ()
		
	{
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
        optionMenu.enabled = false;


    }



    public void ExitPress()//exit menu
		
	{
        optionMenu.enabled = false;
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}
	
	public void NoPress()
		
	{
        optionMenu.enabled = false;
        quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
		
	}



    public void BackOption()

    {
        optionMenu.enabled = false;
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
        IsOptionOpen = false;

    }


    public void StartLevel()//start the game
		
	{
		Application.LoadLevel(1);
	}



    public void Option()//mine

    {
        optionMenu.enabled = true;
        IsOptionOpen = true;
        quitMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void MuteSound()//exit the game

    {
        SoundMute = !SoundMute;
        AudioListener.pause = SoundMute;
    }

    public void ExitGame()//exit the game
		
	{
        quitMenu.enabled = true;
        Application.Quit();
    }

    

    //------------
    void OnGUI()
    {
        if (IsOptionOpen)
        {//public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue); 
            GUILayout.BeginHorizontal();
            //GUILayout.Label("Music volume: ", GUILayout.Width(90));
            MusicVolume = GUI.HorizontalSlider(new Rect(500, 170, 100, 10), MusicVolume, 0.0f, 10.0f);
            AudioListener.volume = MusicVolume;
            PlayerPrefs.SetFloat("MusicV", MusicVolume);
            GUILayout.EndHorizontal();


            //NOT YET IMPLEMENTED---------------------------------------------------------
            GUILayout.BeginHorizontal();
            //GUILayout.Label("SoundVolume: ", GUILayout.Width(90));
            SoundVolume = GUI.HorizontalSlider(new Rect(500, 190, 100, 30), SoundVolume, 0.0f, 10.0f);
            GUILayout.EndHorizontal();
            //NOT YET IMPLEMENTED---------------------------------------------------------


            GUILayout.BeginHorizontal();
            //GUILayout.Label("Brightness: ", GUILayout.Width(90));
            BrightnessS = GUI.HorizontalSlider(new Rect(500, 350, 100, 30), BrightnessS, 10.0f, 0.0f);



            SpriteRenderer spRend = Blackness.transform.GetComponent<SpriteRenderer>();
            spRend.color = new Color(1f, 1f, 1f, (BrightnessS/8));



            GUILayout.EndHorizontal();

            //MusicVolume = GUILayout.HorizontalSlider(new Rect(480, 200, 100, 20), 0.0f, 300.0f, "VOLUME");


        }

    }
    //------------



    

}
