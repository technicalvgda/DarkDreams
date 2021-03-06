﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    private AsyncOperation async;
    public Image blackTexture;
    public float fadeSpeed = 1.5f;
    private int waitTime = 10;
    bool fadeOut = false;
    // Use this for initialization
    void Start()
    {
        //unload old resources
        Resources.UnloadUnusedAssets();
        //force heap collection
        System.GC.Collect();
        //load new scene
        StartCoroutine(LoadLevel("Tutorial Stage"));
        Invoke("ActivateScene", 14f);
    }
    void Update()
    {

        if (fadeOut)
        {

            blackTexture.color = Color.Lerp(blackTexture.color, Color.black, fadeSpeed * Time.deltaTime);
           
        }
        else
        {
            blackTexture.color = Color.Lerp(blackTexture.color, Color.clear, fadeSpeed * Time.deltaTime/5);
            if (blackTexture.color.a > 254f)
            {
                blackTexture.color = Color.black;

            }
        }
       
    }


    private IEnumerator LoadLevel(string Level)
    {
        //yield return new WaitForSeconds(waitTime);
        //fadeOut = true;
        //yield return new WaitForSeconds(3);
        async =Application.LoadLevelAsync(Level);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(waitTime);
        fadeOut = true;
        yield return async;
    }
    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }
}
