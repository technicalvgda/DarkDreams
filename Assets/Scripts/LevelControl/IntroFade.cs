using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Linq;

public class IntroFade : MonoBehaviour
{
    public Color endColor;
    //displays on intro panel (may change from level to level)
    public string progressMessage = "Goodnight";
    public float timeToWait = 1.0f;
    public float timeToFade = 1.0f;
    public float timeToUpdatePeriods = 0.5f;
    public int periodCount = 3;
    public bool updateText = true;

    private Image[] imagesToFade;
    private SpriteRenderer[] spriteRenderersToFade;
    private Text[] textsToFade;
    private Color[] imageStartColors;
    private Color[] spriteRendererStartColors;
    private Color[] textStartColors;
    private float waitProgress;
    private float fadeProgress;

    void Start()
    {
        imagesToFade = this.GetComponentsInChildren<Image>();
        imageStartColors = new Color[imagesToFade.Length];
        for (int i = 0; i < imagesToFade.Length; i++)
        {
            imageStartColors[i] = imagesToFade[i].color;
        }
        spriteRenderersToFade = this.GetComponentsInChildren<SpriteRenderer>();
        spriteRendererStartColors = new Color[spriteRenderersToFade.Length];
        for (int i = 0; i < spriteRenderersToFade.Length; i++)
        {
            spriteRendererStartColors[i] = spriteRenderersToFade[i].color;
        }
        textsToFade = this.GetComponentsInChildren<Text>();
        textStartColors = new Color[textsToFade.Length];
        for (int i = 0; i < textsToFade.Length; i++)
        {
            textStartColors[i] = textsToFade[i].color;
        }
    }

    void Update()
    {
        waitProgress += Time.deltaTime;
        if (updateText && textsToFade.Length > 0)
        {
            int currentPeriodCount = (int)((waitProgress / timeToUpdatePeriods) % (periodCount + 1));
            textsToFade[0].text = progressMessage + String.Concat(Enumerable.Repeat(".", currentPeriodCount).ToArray()) + String.Concat(Enumerable.Repeat(" ", periodCount + 1 - currentPeriodCount).ToArray());
        }
        if (waitProgress >= timeToWait)
        {
            fadeProgress += Time.deltaTime / timeToFade;
            for (int i = 0; i < imagesToFade.Length; i++)
            {
                imagesToFade[i].color = Color.Lerp(imageStartColors[i], endColor, fadeProgress);
            }
            for (int i = 0; i < spriteRenderersToFade.Length; i++)
            {
                spriteRenderersToFade[i].color = Color.Lerp(spriteRendererStartColors[i], endColor, fadeProgress);
            }
            for (int i = 0; i < textsToFade.Length; i++)
            {
                textsToFade[i].color = Color.Lerp(textStartColors[i], endColor, fadeProgress);
            }
        }
    }
}