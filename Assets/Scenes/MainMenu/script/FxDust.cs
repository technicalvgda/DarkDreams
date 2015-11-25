using UnityEngine;
using System.Collections;

public class FxDust : MonoBehaviour
{
    Transform t;
    SpriteRenderer sr;
    float yStarting;

	// Use this for initialization
	void Start ()
    {
        t = this.transform;
        sr = this.GetComponent<SpriteRenderer>();

        yStarting = t.position.y;

        StartCoroutine(_VFX());
	}

    void Update()
    {
        // continuously descends
        t.position += new Vector3(0, -.005f, 0);
        t.eulerAngles += new Vector3(0, 0, .05f);
    }
    
    IEnumerator _VFX()
    {
        while (true)
        {
            t.position = new Vector3(t.position.x, yStarting, t.position.z);
            t.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            // fades in
            while (sr.color.a < 1)
            {
                sr.color += new Color(0, 0, 0, 0.02f);
                yield return null;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            yield return new WaitForSeconds(3);

            // fades out
            while (sr.color.a > 0)
            {
                sr.color -= new Color(0, 0, 0, 0.02f);
                yield return null;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            yield return new WaitForSeconds(.5f);
        }
    }
}
