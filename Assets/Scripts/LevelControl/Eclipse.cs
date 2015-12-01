using UnityEngine;
using System.Collections;

public class Eclipse : MonoBehaviour {

    Vector3 startPos;
    Vector3 endPos;
    Vector3 movementDist = new Vector3(3f, 0,0);
    Vector3 levelOffset= new Vector3(0,0,0);
    public GameObject moon;
    SpriteRenderer eclipseSprite;
    Color eclipseColor;
    Color invisEclipse;
    SpriteRenderer moonSprite;
    Color moonColor;
    Color invisMoon;
    float duration = 1.0f;
    int waitTime = 2;//2 seconds
    bool transition = false;
    public float speed = 0.5F;
    private float startTime;
    private float journeyLength;
    // Use this for initialization
    void Start ()
    {
        eclipseSprite = this.GetComponent<SpriteRenderer>();
        eclipseColor = invisEclipse= eclipseSprite.color;
        invisEclipse.a = 0;
        moonSprite = moon.GetComponent<SpriteRenderer>();
        moonColor = invisMoon = moonSprite.color;
        invisMoon.a = 0;
        eclipseSprite.color = invisEclipse;
        moonSprite.color = invisMoon;

        StartCoroutine(Transition());
    }
    void Update()
    {
        
        eclipseColor.a= Mathf.Lerp(0.0f, 255.0f, Time.deltaTime/4);
        moonColor.a = Mathf.Lerp(0.0f, 255.0f, Time.deltaTime/4);
        eclipseSprite.color = eclipseColor;
        moonSprite.color = moonColor;
        if (transition)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
        }

    }
	
	
    IEnumerator Transition()
    {
        yield return new WaitForSeconds(waitTime);
        //get starting position of eclipse
        startPos = this.transform.position;
        //move it over based upon level
        //application.loaded level - 1 gets the levels proper level number (tutorial is lvl 1)
        //levelOffset += new Vector3((Application.loadedLevel - 1) + 5, 0, 0);
        //startPos += levelOffset;
        endPos = startPos + movementDist;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, endPos);
        transition = true;
        /*
        float t = 0.0f;
        float rate = 1.0f / 200.0f;
        //get starting position of eclipse
        startPos = this.transform.position;
        //move it over based upon level
        //application.loaded level - 1 gets the levels proper level number (tutorial is lvl 1)
        levelOffset += new Vector3((Application.loadedLevel - 1) + 5, 0, 0);
        startPos += levelOffset;
        endPos = startPos + movementDist;
        
        while (transform.position.x <= endPos.x)//t < 1f)
        {

            t += Time.deltaTime * rate;
            //Debug.Log(t);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
            
        }
    
        yield return null;
        */


    }
}
