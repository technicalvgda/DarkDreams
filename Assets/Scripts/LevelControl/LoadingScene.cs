using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    private AsyncOperation async = null;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(LoadLevel("Tutorial Stage"));
    }


    private IEnumerator LoadLevel(string Level)
    {
        Application.LoadLevelAsync(Level);
        yield return 0;
    }
}
