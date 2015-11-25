﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenLogo : MonoBehaviour
{
    SpriteRenderer sr;
    Button btn;

	// Use this for initialization
	void Start ()
    {
        sr = this.transform.Find("Logo").GetComponent<SpriteRenderer>();
        btn = this.transform.Find("Click").GetComponent<Button>();
	}

    public void Activate() { StartCoroutine(_FadeIn()); }
    public void Deactivate() { StartCoroutine(_FadeOut()); }

    IEnumerator _FadeIn()
    {
        sr.color = new Color(1, 1, 1, 0);
        btn.interactable = false;
        yield return new WaitForSeconds(.5f);

        while (sr.color.a < 1)
        {
            sr.color += new Color(0, 0, 0, .02f);
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.2f);
        btn.interactable = true;
    }

    IEnumerator _FadeOut()
    {
        sr.color = new Color(1, 1, 1, 1);
        btn.interactable = false;

        while (sr.color.a > 0)
        {
            sr.color -= new Color(0, 0, 0, .08f);
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 0);

        this.gameObject.SetActive(false);
    }
}