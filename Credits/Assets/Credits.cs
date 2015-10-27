using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject camera;
	public int speed = 1;
	public string level;
	
	// Update is called once per frame
	void Update () {
	
		camera.transform.Translate (Vector3.down * Time.deltaTime * speed);
	
	}

	IEnumerator waitFor(){

		yield return new WaitForSeconds (20);
		Application.LoadLevel (level);

	}

}


