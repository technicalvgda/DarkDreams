using UnityEngine;
using System.Collections;

public class FxSpin : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		// hide
		transform.position = new Vector3(9999, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spin(float x, float y) {
		StartCoroutine (_Spin (x, y));
	}

	IEnumerator _Spin(float x, float y) {
		transform.position = new Vector3 (x, y, 0);
		transform.localScale = new Vector3 (3, 3, 0);

		for (int i = 0; i < 12; i++) {
			transform.eulerAngles += new Vector3(0, 0, 15);
			transform.localScale -= new Vector3(0.25f, 0.25f, 0);
			yield return null;
		}

		// hide
		transform.position = new Vector3(9999, 0, 0);
	}
}
