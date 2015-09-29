/* Fades the alpha value of a Canvas Group.
 * The Alpha range is between 0 and 1.
 */

using System.Collections;
using UnityEngine;

public class FadingDarkness : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float fadeSpeed;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // sets the CanvasGroup alpha to 0
        canvasGroup.alpha = 0;
        // sets the fadeSpeed to 10f
        fadeSpeed = 0.05f;
    }

    void Update()
    {
        // if the CanvasGroup alpha is less than 1, increase the alpha by fadeSpeed
        if (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
    }
}