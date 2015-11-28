using UnityEngine;
using System.Collections;

// work on your naming scheme
public class FxLogo : MonoBehaviour
{
    SpriteRenderer sr;

    void Start ()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0);
        StartCoroutine(_FadeIn());
	}

    IEnumerator _FadeIn()
    {
        // Needs to sync with the bulb's fade in
        yield return new WaitForSeconds(1);
        while (sr.color.a < 1)
        {
            sr.color += new Color(0, 0, 0, .02f);
            yield return null;
        }
        sr.color = Color.white;
    }

    IEnumerator _FadeOut()
    {
        sr.color = Color.white;
        while (sr.color.a > 0)
        {
            sr.color -= new Color(0, 0, 0, .08f);
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 0);

        this.gameObject.SetActive(false);
    }

    public void FadeOut() { StartCoroutine(_FadeOut()); }
}
