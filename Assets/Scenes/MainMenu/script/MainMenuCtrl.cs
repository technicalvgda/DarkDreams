using UnityEngine;
using System.Collections;

public class MainMenuCtrl : MonoBehaviour
{
    // Manages the screens, contains functions to be used on button clicks.

    public ScreenLogo scrnLogo;
    public ScreenSelectables scrnMain;
    public ScreenSelectables scrnSettings;
    public ScreenSelectables scrnBrightness;
    public ScreenSelectables scrnAudio;
    public ScreenDialog quitDialog;

    ScreenSelectables scrnLast;
    Coroutine routine;
    WaitForSeconds waitTransition = new WaitForSeconds(.3f);

    //audio control
    AudioHandlerScript audioHandler;

    void Start ()
    {
        // Makes sure the sprites appear correct
        const int scrnHeight = 720;
        const int ppu = 100;
        FindObjectOfType<Camera>().orthographicSize = (scrnHeight / 2.0f) / ppu;
        //find audio handler
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();

        // Unfreeze time in case somebody forgets to do it (thanks pause screen)
        Time.timeScale = 1f;

        scrnLogo.Activate();
        //play title music
        audioHandler.LoopMusic(true);
        //music 0 is Lullaby Waltz (title music)
        audioHandler.PlayMusic(0);
    }

    // Not used for now
    void SetRoutine(IEnumerator arg)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(arg);
    }

    // redundant
    IEnumerator _TransitionFromLogo()
    {
        scrnLogo.Deactivate();
        // Requires manual syncing, not as flexible as while(lastscreen=active)yield;
        yield return waitTransition;
        scrnMain.Activate();
        scrnLast = scrnMain;
    }
    
    IEnumerator _TransitionTo(ScreenSelectables arg)
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
        scrnLast.SetButtonsInteractable(false);
        while (quitDialog.gameObject.activeSelf)
            yield return null;
        scrnLast.SetButtonsInteractable(true);
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
    public void TransitionFromLogo() { StartCoroutine(_TransitionFromLogo()); }
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
