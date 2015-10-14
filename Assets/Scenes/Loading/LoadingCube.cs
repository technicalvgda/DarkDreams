using UnityEngine;
using System.Collections;

public class LoadingCube : MonoBehaviour {
	public GameObject cube;
	public GameObject cube1;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("setCube", 1,3f);
		InvokeRepeating ("setCube2",2 ,3f);
		InvokeRepeating ("notSetCube", 3, 3f);
	}
	
	// Update is called once per frame
	void setCube () {
		cube.SetActive (true);

	}
	void setCube2() {
		cube1.SetActive (true);
	}
	void notSetCube() {
		cube.SetActive (false);
		cube1.SetActive (false); 
	}
}
