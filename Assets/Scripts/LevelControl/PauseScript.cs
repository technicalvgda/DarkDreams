
//
// WARNING: You can still pause during the Game Over screen.
//

using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class PauseScript : MonoBehaviour {

	public GameObject panel;
	public GameObject text;
    GameObject player;
    //public GameObject resumeButton;
    bool paused;
	bool busy;

	// Use this for initialization
	void Start () {
        //GameObject.Find("ResumeButton");
       panel.SetActive(false);
       player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update () {
		if (!busy && Input.GetKeyDown (KeyCode.Escape)&& (player.GetComponent<PlayerControl>().isAlive == true)){
            
            paused = !paused;

			// Lock keys until transitioning is over
			busy = true;

			// This will spook the garbage collector after a few thousand times
			if (paused)
				StartCoroutine (_PauseTransition ());
			else
				StartCoroutine (_UnpauseTransition ());
		}
	}
	
	IEnumerator _PauseTransition() {
        panel.SetActive(true);
		// Freeze the game
		Time.timeScale = 0;

		// The transition effect; just an example
		// Initialize the fade color
		panel.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
		// Fade in over 10 frames
		for (int i = 0; i < 10; i++) {
			panel.GetComponent<Image> ().color += new Color (0, 0, 0, 0.04f);
			yield return null;
		}

		// Show the PAUSED text
		text.GetComponent<Text> ().enabled = true;

		// Transition done, accept input again
		busy = false;
	}

    public void Resume()
    {
        StartCoroutine(_UnpauseTransition());
    }
    public void Quit()
    {
        //load first level in hierarchy
        Application.LoadLevel(0);
    }

	IEnumerator _UnpauseTransition() {
		Time.timeScale = 1;
		panel.GetComponent<Image> ().color = new Color (0, 0, 0, 0.4f);

		// Remove the PAUSED text
		text.GetComponent<Text> ().enabled = false;

		// Fade out over 10 frames
		for (int i = 0; i < 10; i++) {
			panel.GetComponent<Image> ().color += new Color (0, 0, 0, -0.04f);
			yield return null;
		}

		busy = false;
        panel.SetActive(false);
	}
    
}
