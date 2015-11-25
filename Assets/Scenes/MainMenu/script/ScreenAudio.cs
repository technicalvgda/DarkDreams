using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenAudio : CustomScreen
{
    public AudioHandlerScript audioHandler;
    public Slider slrMusic, slrSfx, slrVoice;
    public Button btnBack;
    float music, sfx, voice;

    WaitForSeconds wait = new WaitForSeconds(.1f);

    void Start()
    {
        music = slrMusic.value * .1f;
        sfx = slrSfx.value * .1f;
        voice = slrVoice.value * .1f;

        SaveSettings();

        music = PlayerPrefs.GetFloat("Music");
        sfx = PlayerPrefs.GetFloat("SFX");
        voice = PlayerPrefs.GetFloat("Voice");
    }

    public void UpdateMusic()
    {
        music = slrMusic.value * .1f;
        audioHandler.music.volume = music;
    }

    public void UpdateSfx()
    {
        sfx = slrSfx.value * .1f;
        audioHandler.sfx.volume = sfx;
    }

    public void UpdateVoice()
    {
        voice = slrVoice.value * .1f;
        audioHandler.voice.volume = voice;
    }

    void SaveSettings()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", music);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", sfx);
        if (!PlayerPrefs.HasKey("Voice"))
            PlayerPrefs.SetFloat("Voice", voice);
    }

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
