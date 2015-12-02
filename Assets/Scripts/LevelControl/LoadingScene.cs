using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    private AsyncOperation async = null;
    public Image blackTexture;
    public float fadeSpeed = 1.5f;
    private int waitTime = 10;
    bool fadeOut = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadLevel("Tutorial Stage"));
    }
    void Update()
    {

        if (fadeOut)
        {

            blackTexture.color = Color.Lerp(blackTexture.color, Color.black, fadeSpeed * Time.deltaTime);
            if (blackTexture.color.a > 254f)
            {
                async.allowSceneActivation = true;
            }
        }
        else
        {
            blackTexture.color = Color.Lerp(blackTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
        }
       
    }


    private IEnumerator LoadLevel(string Level)
    {
        yield return new WaitForSeconds(waitTime);
        async=Application.LoadLevelAsync(Level);
        async.allowSceneActivation = false;
        fadeOut = true;
        
       
        yield return 0;
    }
}
