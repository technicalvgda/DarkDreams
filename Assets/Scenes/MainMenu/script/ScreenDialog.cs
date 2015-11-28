using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenDialog : MonoBehaviour
{
    // The beam's sprites had pivots that are not at the center. Be careful when modifying them!
    public Transform beamHead, beamTail;
    public Button btnYes, btnNo;

    Coroutine routine;
    WaitForSeconds wait = new WaitForSeconds(.1f);

    void SetRoutine(IEnumerator arg)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(arg);
    }

    void SetButtonsActive(bool arg)
    {
        btnYes.gameObject.SetActive(arg);
        btnNo.gameObject.SetActive(arg);
    }

    void SetButtonsInteractable(bool arg)
    {
        btnYes.interactable = arg;
        btnNo.interactable = arg;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        SetButtonsActive(false);
        SetButtonsInteractable(false);
        SetRoutine(_BeamIn());
    }

    public void Deactivate()
    {
        SetButtonsActive(true);
        SetButtonsInteractable(false);
        SetRoutine(_FadeOut());
    }

    IEnumerator _BeamIn()
    {
        beamHead.position = new Vector3(-40, 0, 0);
        beamTail.position = new Vector3(-40, 0, 0);
        beamTail.localScale = new Vector3(100000, 1200, 1);

        while (beamTail.position.x < 6.2)
        {
            beamHead.position += new Vector3(2, 0, 0);
            beamTail.position += new Vector3(2, 0, 0);
            yield return null;
        }

        while (beamTail.localScale.y < 5000)
        {
            beamTail.localScale += new Vector3(0, 800, 0);
            yield return null;
        }

        for (int i = 0; i < 5; i++)
        {
            SetButtonsActive(!btnYes.gameObject.activeSelf);
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
            SetButtonsActive(!btnYes.gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetButtonsActive(false);

        this.gameObject.SetActive(false);
    }
}
