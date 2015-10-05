using UnityEngine;
using System.Collections;

public class MainTitle : MonoBehaviour {

	//TODO: clean this up

	public GameObject bgPatternL;
	public GameObject bgPatternR;

	public GameObject textPress;
	public GameObject btnTest;
	public GameObject btnQuit;

	// particle effects
	public GameObject particleBeam;
	public GameObject particleCross;
	public GameObject particleFade;

	// sfx
	public GameObject sfxChange;
	public GameObject sfxConfirm;
	
	// Use this for initialization
	void Start () {
		// hide both buttons
		btnTest.SetActive (false);
		btnQuit.SetActive (false);

		// start
		StartCoroutine ("_ScrollBG");
		StartCoroutine ("_Main");
	}
	
	// Update is called once per frame
	void Update () {
	}

	// seamlessly scrolls the background
	IEnumerator _ScrollBG() {
		float x = 0;
		while (true) {
			x += 0.01f;
			if (x > 12.8f) x -= 12.8f;
			bgPatternL.transform.position = new Vector3 (x, 0, 0);
			bgPatternR.transform.position = new Vector3 (x - 12.8f, 0, 0);
			yield return null;
		}
	}

	WaitForSeconds secWait = new WaitForSeconds (1);	// 60 frames
	WaitForSeconds halfSecWait = new WaitForSeconds (0.5f);	// 30 frames

	// TODO: break this thing into smaller coroutines
	IEnumerator _Main() {
		// transition in
		//TODO

		// wait for everything to set in
		yield return secWait;

		// flash the "press button" text, if this is placed any higher up, it briefly causes a
		// NullReferenceException in the flash script, probably due to things not setting in yet
		textPress.GetComponent<FxFlash> ().Set (0.05f, 2);

		// if any key is pressed, flash the text faster and play a sound
		while (true) {
			if (Input.anyKey) {
				sfxConfirm.GetComponent<AudioSource> ().Play ();
				textPress.GetComponent<FxFlash> ().Set (0.3f, 1);
				break;
			}
			yield return null;
		}
		yield return secWait;
		yield return halfSecWait;

		// hide the text and slide the buttons in
		textPress.SetActive (false);
		btnTest.SetActive (true);
		btnQuit.SetActive (true);
		for (int i = 0; i < 10; i++) {
			btnTest.transform.position -= new Vector3 (0, 0.03f, 0);
			btnQuit.transform.position -= new Vector3 (0, 0.03f, 0);
			yield return null;
		}

		// input check
		int selection;
		while ((selection = MouseCheck ()) == -1)
		//while ((selection = KeyCheck ()) == -1)
			yield return null;

		// the player confirmed something; play a sound
		sfxConfirm.GetComponent<AudioSource> ().Play ();
		yield return secWait;

		// transition out
		SpriteRenderer fadeSprite = particleFade.GetComponent<SpriteRenderer> ();
		while (fadeSprite.color.a < 1) {
			fadeSprite.color += new Color(0, 0, 0, 0.1f);
			yield return null;
		}

		yield return halfSecWait;

		// see which button the player pressed, flash it and call the corressponding function
		switch (selection) {
		case 0:
			StartGame ();	// PUT SOMETHING IN THIS FUNCTION
			break;
		case 1:
			Quit ();		// SAME FOR THIS ONE
			break;
		default:
			break;
		}
	}

	// check each button; first click focuses the button,
	// second click confirms and notifies by the user by flashing it
	int MouseCheck() {
		if (btnTest.GetComponent<CustomButton>().clicked) {
			if (!btnTest.GetComponent<CustomButton>().selected)
				SelectBtnTest ();
			else {
				btnTest.GetComponent<FxFlash> ().Set (0.3f, 1);
				return 0;
			}
		}
		else if (btnQuit.GetComponent<CustomButton>().clicked) {
			if (!btnQuit.GetComponent<CustomButton>().selected)
				SelectBtnQuit ();
			else {
				btnQuit.GetComponent<FxFlash> ().Set (0.3f, 1);
				return 1;
			}
		}
		return -1;
	}

	// Left/right selects the corresponding option. If user hits Enter, confirms the highlighted option
	int KeyCheck() {
		if (Input.GetKey ("left") && !btnTest.GetComponent<CustomButton> ().selected)
			SelectBtnTest ();
		else if (Input.GetKey ("right") && !btnQuit.GetComponent<CustomButton> ().selected)
			SelectBtnQuit ();
		else if (Input.GetKey ("enter")) {
			if (btnTest.GetComponent<CustomButton>().selected) {
				btnTest.GetComponent<FxFlash> ().Set (0.3f, 1);
				return 0;
			}
			else if (btnQuit.GetComponent<CustomButton>().selected) {
				btnQuit.GetComponent<FxFlash> ().Set (0.3f, 1);
				return 1;
			}
		}
		return -1;
	}

	
	// this happens when the player confirms "Test"
	void StartGame() {
		Application.LoadLevel ("TestScene");
	}
	
	// confirms "Quit"
	void Quit() {
		Application.Quit ();
	}


	void SelectBtnTest() {
		FxBtnTest (); // plays effect, set flags
		btnTest.GetComponent<CustomButton>().selected = true;
		btnQuit.GetComponent<CustomButton>().selected = false;
	}

	void SelectBtnQuit() {
		FxBtnQuit ();
		btnTest.GetComponent<CustomButton>().selected = false;
		btnQuit.GetComponent<CustomButton>().selected = true;
	}


	// do flashy things; ignore all this
	void FxBtnTest() {
		// play sfx
		sfxChange.GetComponent<AudioSource> ().Play ();

		// particle effects
		particleBeam.GetComponent<FxSlide>().Slide
			(btnQuit.transform.position.x, btnQuit.transform.position.y, 1);
		particleCross.GetComponent<FxSpin>().Spin
			(btnTest.transform.position.x, btnTest.transform.position.y);

		// change the sprite
		btnTest.GetComponent<Animator>().SetBool("Selected", true);
		btnQuit.GetComponent<Animator>().SetBool("Selected", false);
	}

	void FxBtnQuit() {
		sfxChange.GetComponent<AudioSource> ().Play ();
		particleBeam.GetComponent<FxSlide>().Slide
			(btnTest.transform.position.x, btnTest.transform.position.y, 0);
		particleCross.GetComponent<FxSpin>().Spin
			(btnQuit.transform.position.x, btnQuit.transform.position.y);
		btnTest.GetComponent<Animator>().SetBool("Selected", false);
		btnQuit.GetComponent<Animator>().SetBool("Selected", true);
	}
}
