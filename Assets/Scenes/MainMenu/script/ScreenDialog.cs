using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenDialog : MonoBehaviour
{
    // The beam's sprites had pivots that are not at the center. Be careful when modifying them!
    public Transform beamHead, beamTail;
    public SpriteRenderer msg;
    public Button btnYes, btnNo;

    WaitForSeconds wait = new WaitForSeconds(.1f);

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

        StopAllCoroutines();
        StartCoroutine(_EnterBeam());
    }

    public void Deactivate()
    {
        SetButtonsActive(true);
        SetButtonsInteractable(false);

        StopAllCoroutines();
        StartCoroutine(_ExitFlash());
    }

    IEnumerator _EnterBeam()
    {
        // INIT
        beamHead.position = new Vector3(-40, 0, 0);
        beamTail.position = new Vector3(-40, 0, 0);
        beamTail.localScale = new Vector3(100000, 1200, 1);
        msg.color = new Color(1, 1, 1, 0);

        // EXEC
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

        StartCoroutine(_EnterMsg());
        StartCoroutine(_EnterFlash());
    }

    IEnumerator _EnterMsg()
    {
        while (msg.color.a < 1)
        {
            msg.color += new Color(0, 0, 0, .1f);
            yield return null;
        }
        msg.color = Color.white;
    }

    IEnumerator _EnterFlash()
    {
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

    IEnumerator _ExitFlash()
    {
        for (int i = 0; i < 5; i++)
        {
            SetButtonsActive(!btnYes.gameObject.activeSelf);
            yield return null;
            yield return null;
        }
        SetButtonsActive(false);
        msg.color = new Color(1, 1, 1, 0);

        this.gameObject.SetActive(false);
    }
}
