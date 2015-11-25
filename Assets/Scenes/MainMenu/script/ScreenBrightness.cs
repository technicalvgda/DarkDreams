using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenBrightness : CustomScreen
{
    //public Transform preview; // already affected by the light
    public Slider slrBright;
    public Button btnBack;
    float gamma;
    WaitForSeconds wait = new WaitForSeconds(.1f);

    void Start()
    {
        gamma = 1 + slrBright.value * .2f;
        SaveSettings();
        gamma = PlayerPrefs.GetFloat("Brightness");
        RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1);
    }

    public void UpdateBrightness()
    {
        gamma = 1 + slrBright.value * .2f;
        RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1);
    }

    void SaveSettings()
    {
        if (!PlayerPrefs.HasKey("Brightness"))
            PlayerPrefs.SetFloat("Brightness", gamma);
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
        slrBright.interactable = arg;
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
