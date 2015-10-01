using UnityEngine;
using System.Collections;

public class FxSlide : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		// hide off-screen
		transform.position = new Vector3 (9999, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Slide(float x, float y, int direction) {
		StartCoroutine (_Slide (x, y, direction));
	}

	// fixed values for now
	IEnumerator _Slide(float x, float y, int direction) {
		transform.position = new Vector3 (x, y, 0);

		// 0=left-to-right, 1=right-to-left
		if (direction == 0) {
			for (int i = 0; i < 5; i++) {
				transform.position += new Vector3 (0.5f, 0, 0);
				yield return null;
			}
		} else {
			for (int i = 0; i < 5; i++) {
				transform.position -= new Vector3 (0.5f, 0, 0);
				yield return null;
			}
		}

		// hide again
		transform.position = new Vector3(9999, 0, 0);
	}
}
