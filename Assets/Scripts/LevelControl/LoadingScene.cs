using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    private AsyncOperation async = null;
    private int waitTime = 10;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(LoadLevel("Tutorial Stage"));
    }


    private IEnumerator LoadLevel(string Level)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevelAsync(Level);
        yield return 0;
    }
}
