using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Manages the screens, contains functions to be called on button clicks.
public class MainMenuCtrl : MonoBehaviour
{
    [Header("Graphics")]
    public SpriteRenderer fade;

    [Header("Audio")]
    public AudioHandlerScript audioHandler;

    [Header("Screens")]
    public MenuScreen scrnLogo;
    public MenuScreen scrnMain, scrnSettings, scrnBrightness, scrnAudio;
    public ScreenDialog quitDialog;
    MenuScreen scrnLast;

    WaitForSeconds waitTransition = new WaitForSeconds(.3f);

    void Start()
    {
        // Unfreezes time in case somebody forgets to do it (thanks pause screen)
        Time.timeScale = 1f;
        //create or reset the playerpref for level jumping
        PlayerPrefs.SetInt("NextLevel", 3);
        
        // Makes sure the sprites appear correct
        const int scrnHeight = 720;
        const int ppu = 100;
        FindObjectOfType<Camera>().orthographicSize = (scrnHeight / 2.0f) / ppu;

        //play title music
        audioHandler.LoopMusic(true);
        //music 0 is Lullaby Waltz (title music)
        audioHandler.PlayMusic(0);

        // The settings management code are in their respective screens.
        // These screens start out inactive; their initialization needs to be manually called.
        // Awake() and Start() won't be called if an object starts out inactive.
        scrnBrightness.InitSettings();
        scrnAudio.InitSettings();

        StartCoroutine(_FadeIn());
    }

    // Not used for now
    Coroutine routine;
    void SetRoutine(IEnumerator arg)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(arg);
    }

    IEnumerator _FadeIn()
    {
        yield return new WaitForSeconds(.5f);
        while (fade.color.a > 0)
        {
            fade.color += new Color(0, 0, 0, -.1f);
            yield return null;
        }
        fade.color = Color.clear;

        yield return new WaitForSeconds(2.5f);

        scrnLogo.Activate();
        scrnLast = scrnLogo;
    }

    IEnumerator _FadeToScene(int scene)
    {
        scrnLast.Deactivate();
        scrnLast = null;

        fade.color = Color.clear;
        while (fade.color.a < 0.1f)
        {
            fade.color += new Color(0, 0, 0, .0015f);
            yield return null;
        }
        while (fade.color.a < 1)
        {
            fade.color += new Color(0, 0, 0, .1f);
            yield return null;
        }
        fade.color = Color.black;

        yield return waitTransition;
        Application.LoadLevel(scene);
    }

    IEnumerator _TransitionTo(MenuScreen arg)
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

    // These allow the coroutines to be called in the buttons' UnityEvent
    public void TransitionToMain() { StartCoroutine(_TransitionTo(scrnMain)); }
    public void TransitionToSettings() { StartCoroutine(_TransitionTo(scrnSettings)); }
    public void TransitionToBrightness() { StartCoroutine(_TransitionTo(scrnBrightness)); }
    public void TransitionToAudio() { StartCoroutine(_TransitionTo(scrnAudio)); }
    public void DialogStandby() { StartCoroutine(_DialogStandby()); }
    public void NewGame() { StartCoroutine(_FadeToScene(9)); }//make this 2 for tutorial
    public void NightmareTower() { StartCoroutine(_FadeToScene(8)); }
    public void Quit() { Application.Quit(); }

    // Note:
    // Clicked "Back" buttons should really tell the control to go back on some kind
    // of screen stack instead of actually bringing up a specific screen
}
