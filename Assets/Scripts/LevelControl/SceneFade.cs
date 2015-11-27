using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{

    [Header("Must be set!!!")]
    public Image blackTexture;

    [Header("Increase/Decrease fade speed")]
    public float fadeSpeed = 1.5f;

    [Header("These are the background images")]
    public SpriteRenderer BG1;
    public SpriteRenderer BG2;
    /* 
     * Add additional SpriteRenderers for additional images to show.
     * Each one should have the renderer itself disabled.
     * DO NOT DISABLE THE PARENT OBJECT!!!
     */

    //Used for control => Hidden
    private bool sceneStarting = true;
    private bool sceneEnding = false;

    [Header("The name of the scene to follow this")]
    public string levelToLoad = "";
    void Start()
    {
        Debug.Log("Start()");
        blackTexture.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
        if (sceneEnding)
        {
            EndScene();
        }
    }

    void FadeToClear()
    {
        blackTexture.color = Color.Lerp(blackTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        blackTexture.color = Color.Lerp(blackTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (blackTexture.color.a <= 0.05f)
        {
            blackTexture.color = Color.clear;
            blackTexture.enabled = false;

            sceneStarting = false;
        }
    }

    void EndScene()
    {
        blackTexture.enabled = true;

        FadeToBlack();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        sceneEnding = true;
        yield return new WaitForSeconds(5);
        //**** COPY FROM HERE ****//
        BG1.enabled = false;
        BG2.enabled = true;
        sceneStarting = true;
        sceneEnding = false;

        yield return new WaitForSeconds(5);
        sceneEnding = true;
        yield return new WaitForSeconds(5);
        //**** COPY TO HERE ****//

        //**** PASTE HERE ****//

        Application.LoadLevel(levelToLoad);
    }
}

