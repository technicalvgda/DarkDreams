using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("MusicV");
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    





}
