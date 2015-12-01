using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenAudio : MenuScreen
{
    public AudioHandlerScript audioHandler;
    public Slider slrMusic, slrSfx, slrVoice;
    public Button btnBack;
    public FxStreak streak;

    RectTransform rtMusic, rtSfx, rtVoice;

    WaitForSeconds waitShort = new WaitForSeconds(.03f);
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

        rtMusic = slrMusic.GetComponent<RectTransform>();
        rtSfx = slrSfx.GetComponent<RectTransform>();
        rtVoice = slrVoice.GetComponent<RectTransform>();
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
        streak.Activate(-3.4f);

        rtMusic.position = new Vector3(-10, rtMusic.position.y, rtMusic.position.z);
        rtSfx.position = new Vector3(-10, rtSfx.position.y, rtSfx.position.z);
        rtVoice.position = new Vector3(-10, rtVoice.position.y, rtVoice.position.z);

        StartCoroutine(_EnterSlide3());
        StartCoroutine(_EnterFlash());
    }

    public override void Deactivate()
    {
        SetAllInteractable(false);
        SaveSettings();
        streak.Deactivate();
        StartCoroutine(_ExitFlash());
    }

    public override void SetAllInteractable(bool arg)
    {
        slrMusic.interactable = arg;
        slrSfx.interactable = arg;
        slrVoice.interactable = arg;
        btnBack.interactable = arg;
    }

    IEnumerator _EnterSlide(RectTransform rt)
    {
        while (1.41f - rt.position.x > 0.1f)
        {
            rt.position += new Vector3(.3f * (1.41f - rt.position.x), 0, 0);
            yield return null;
        }
        rt.position = new Vector3(1.41f, rt.position.y, rt.position.z);
    }

    IEnumerator _EnterSlide3()
    {
        StartCoroutine(_EnterSlide(rtMusic));
        yield return waitShort;
        StartCoroutine(_EnterSlide(rtSfx));
        yield return waitShort;
        StartCoroutine(_EnterSlide(rtVoice));
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
