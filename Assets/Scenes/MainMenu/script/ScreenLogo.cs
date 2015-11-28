using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScreenLogo : MenuScreen
{
    public Button start;
    Image img;

    public override void Activate()
    {
        this.gameObject.SetActive(true);

        // Can be put in Awake() too
        start.interactable = false;
        img = start.GetComponent<Image>();
        img.color = new Color(0, 0, 0, 0);

        StartCoroutine(_FadeIn());
    }

    public override void Deactivate()
    {
        start.interactable = false;
        StartCoroutine(_FadeOut());
    }

    IEnumerator _FadeIn()
    {
        while (img.color.a < 1)
        {
            img.color += new Color(.03f, .03f, .03f, .03f);
            yield return null;
        }
        img.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.2f);
        start.interactable = true;
    }

    IEnumerator _FadeOut()
    {
        img.color = new Color(1, 1, 1, 1);
        while (img.color.a > 0)
        {
            img.color -= new Color(0, 0, 0, .08f);
            yield return null;
        }
        img.color = new Color(1, 1, 1, 0);

        this.gameObject.SetActive(false);
    }
}
