using UnityEngine;
using System.Collections;

// Manages the screens, contains functions to be called on button clicks.
public class MainMenuCtrl : MonoBehaviour
{
    public ScreenLogo scrnLogo;
    public CustomScreen scrnMain;
    public CustomScreen scrnSettings;
    public CustomScreen scrnBrightness;
    public CustomScreen scrnAudio;
    public ScreenDialog quitDialog;

    CustomScreen scrnLast;
    WaitForSeconds waitTransition = new WaitForSeconds(.3f);

    //audio control
    AudioHandlerScript audioHandler;

    void Start ()
    {
        // Unfreezes time in case somebody forgets to do it (thanks pause screen)
        Time.timeScale = 1f;

        // Makes sure the sprites appear correct
        const int scrnHeight = 720;
        const int ppu = 100;
        FindObjectOfType<Camera>().orthographicSize = (scrnHeight / 2.0f) / ppu;

        //find audio handler
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        //play title music
        audioHandler.LoopMusic(true);
        //music 0 is Lullaby Waltz (title music)
        audioHandler.PlayMusic(0);

        scrnLogo.Activate();
        scrnLast = scrnLogo;
    }

    // Not used for now
    Coroutine routine;
    void SetRoutine(IEnumerator arg)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(arg);
    }

    IEnumerator _TransitionTo(CustomScreen arg)
    {
        scrnLast.Deactivate();
        yield return waitTransition;
        arg.Activate();
        scrnLast = arg;
    }

    // Unfocuses a screen until the dialog is gone.
    // This HAS to be called after the dialog activation, or the while loop ends immediately.
    IEnumerator _DialogStandby()
    {
        scrnLast.SetAllInteractable(false);
        while (quitDialog.gameObject.activeSelf)
            yield return null;
        scrnLast.SetAllInteractable(true);
    }

    IEnumerator _FadeIn()
    {
        //put things here
        yield return new WaitForSeconds(1);
    }

    IEnumerator _FadeToGame()
    {
        // put things here
        //yield return new WaitForSeconds(1);
        yield return null;
        Application.LoadLevel(1);
    }

    // These allow the coroutines to be called in the buttons' UnityEvent
    public void TransitionToMain() { StartCoroutine(_TransitionTo(scrnMain)); }
    public void TransitionToSettings() { StartCoroutine(_TransitionTo(scrnSettings)); }
    public void TransitionToBrightness() { StartCoroutine(_TransitionTo(scrnBrightness)); }
    public void TransitionToAudio() { StartCoroutine(_TransitionTo(scrnAudio)); }
    public void DialogStandby() { StartCoroutine(_DialogStandby()); }
    public void NewGame() { StartCoroutine(_FadeToGame()); }
    public void Quit() { Application.Quit(); }

    // Note:
    // Clicked "Back" buttons should really tell the control to go back instead of actually bringing up a specific screen
}
