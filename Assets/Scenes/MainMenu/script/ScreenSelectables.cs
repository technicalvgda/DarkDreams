using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenSelectables : CustomScreen
{
    public Selectable[] selectables;
    WaitForSeconds wait = new WaitForSeconds(.1f);

    void SetAllActive(bool arg)
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].gameObject.SetActive(arg);
    }

    // This is also called whenever a screen needs to unfocus
    public override void SetAllInteractable(bool arg)
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].interactable = arg;
    }

    public override void Activate()
    {
        this.gameObject.SetActive(true);
        SetAllActive(false);
        SetAllInteractable(false);

        StartCoroutine(_EnterFlash());
    }

    public override void Deactivate()
    {
        SetAllActive(true);
        SetAllInteractable(false);

        StartCoroutine(_ExitFlash());
    }

    IEnumerator _EnterFlash()
    {
        // Flash the UI elements
        for (int i = 0; i < 5; i++)
        {
            SetAllActive(!selectables[0].gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetAllActive(true);

        yield return wait;
        SetAllInteractable(true);
    }

    IEnumerator _ExitFlash()
    {
        for (int i = 0; i < 5; i++)
        {
            SetAllActive(!selectables[0].gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetAllActive(false);

        this.gameObject.SetActive(false);
    }
}
