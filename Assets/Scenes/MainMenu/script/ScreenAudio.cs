using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenAudio : MenuScreen
{
    public AudioHandlerScript audioHandler;
    public Slider slrMusic, slrSfx, slrVoice;
    public Button btnBack;

    WaitForSeconds wait = new WaitForSeconds(.1f);

    public override void InitSettings()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", slrMusic.value);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", slrSfx.value);
        if (!PlayerPrefs.HasKey("Voice"))
            PlayerPrefs.SetFloat("Voice", slrVoice.value);

        // Set sliders to either their own values (in the editor) or the existing values in PlayerPrefs
        slrMusic.value = PlayerPrefs.GetFloat("Music");
        slrSfx.value = PlayerPrefs.GetFloat("SFX");
        slrVoice.value = PlayerPrefs.GetFloat("Voice");

        audioHandler.music.volume = slrMusic.value * .1f;
        audioHandler.sfx.volume = slrSfx.value * .1f;
        audioHandler.voice.volume = slrVoice.value * .1f;
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("Music", slrMusic.value);
        PlayerPrefs.SetFloat("SFX", slrSfx.value);
        PlayerPrefs.SetFloat("Voice", slrVoice.value);
    }

    public void UpdateMusic() { audioHandler.music.volume = slrMusic.value * .1f; }
    public void UpdateSfx() { audioHandler.sfx.volume = slrSfx.value * .1f; }
    public void UpdateVoice() { audioHandler.voice.volume = slrVoice.value * .1f; }

    public override void Activate()
    {
        this.gameObject.SetActive(true);
        SetAllInteractable(false);

        StartCoroutine(_EnterStretch());
        StartCoroutine(_EnterFlash());
    }

    public override void Deactivate()
    {
        SetAllInteractable(false);
        SaveSettings();
        StartCoroutine(_ExitFlash());
    }

    public override void SetAllInteractable(bool arg)
    {
        slrMusic.interactable = arg;
        slrSfx.interactable = arg;
        slrVoice.interactable = arg;
        btnBack.interactable = arg;
    }

    IEnumerator _EnterStretch()
    {
        yield return null;
    }

    IEnumerator _EnterFlash()
    {
        for (int i = 0; i < 5; i++)
        {
            btnBack.gameObject.SetActive(!btnBack.gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        btnBack.gameObject.SetActive(true);

        yield return wait;
        SetAllInteractable(true);
    }

    IEnumerator _ExitFlash()
    {
        for (int i = 0; i < 5; i++)
        {
            btnBack.gameObject.SetActive(!btnBack.gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        btnBack.gameObject.SetActive(false);

        this.gameObject.SetActive(false);
    }
}
