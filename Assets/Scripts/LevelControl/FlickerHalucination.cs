using UnityEngine;
using System.Collections;

public class FlickerHalucination : MonoBehaviour {
    CanvasGroup canvasGroup;
  
    public float seconds;
    public bool onTrigger;
    public bool onFlicker;

	// Use this for initialization
	void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.50f;
        seconds = Random.Range(3,6);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (onTrigger)
        {
            if (seconds <= 0)
            {
                seconds = Random.Range(3, 6);
            }
            else
            {
                seconds -= Time.deltaTime;

                if ((seconds <= 0.30))
                {
                    Flash();
                    onFlicker = true;
                    
                }
                else
                {
                    canvasGroup.alpha = 0.50f;
                }
            }
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrigger = true;
        }
    }

    void Flash()
    {
        float alphaValue = Random.Range(0f, 0.65f);
        canvasGroup.alpha = alphaValue;

        

    }
}
