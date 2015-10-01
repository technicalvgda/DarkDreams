using UnityEngine;
using System.Collections;

public class FxFlash : MonoBehaviour {

	SpriteRenderer sr;
	IEnumerator routine;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	// change the parameters of the flash being played;
	// remove the old coroutine and replace it with a new one
	public void Set(float fade, int wait) {
		if (routine != null)
			StopCoroutine (routine);
		routine = _Flash (fade, wait);
		if (routine != null)	// double check; the console tends to yell if this isn't here
			StartCoroutine (routine);
	}

	IEnumerator _Flash(float fade, int wait) {
		WaitForSeconds w = new WaitForSeconds(wait/60.0f);
		while (true) {
			// fade out
			while (sr.color.a > 0) {
				sr.color += new Color (1, 1, 1, -fade);
				yield return null;
			} yield return w;
			// fade in
			while (sr.color.a < 1) {
				sr.color += new Color (1, 1, 1, fade);
				yield return null;
			} yield return w;
		}
	}
}
