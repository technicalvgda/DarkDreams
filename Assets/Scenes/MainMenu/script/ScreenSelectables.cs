using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenSelectables : MonoBehaviour
{
    public Selectable[] selectables;
    Coroutine routine;
    WaitForSeconds wait = new WaitForSeconds(.1f);

    void SetRoutine(IEnumerator arg)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(arg);
    }

    void SetButtonsActive(bool arg)
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].gameObject.SetActive(arg);
    }

    // This is also called whenever a screen needs to unfocus
    public void SetButtonsInteractable(bool arg)
    {
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].interactable = arg;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        SetButtonsActive(false);
        SetButtonsInteractable(false);
        SetRoutine(_FadeIn());
    }

    public void Deactivate()
    {
        SetButtonsActive(true);
        SetButtonsInteractable(false);
        SetRoutine(_FadeOut());
    }

    IEnumerator _FadeIn()
    {
        // Flash the UI elements
        for (int i = 0; i < 5; i++)
        {
            SetButtonsActive(!selectables[0].gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetButtonsActive(true);

        yield return wait;
        SetButtonsInteractable(true);
    }

    IEnumerator _FadeOut()
    {
        for (int i = 0; i < 5; i++)
        {
            SetButtonsActive(!selectables[0].gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetButtonsActive(false);

        this.gameObject.SetActive(false);
    }
}
