using UnityEngine;
using System.Collections;

public class FxSwing : MonoBehaviour
{
    public int max = 20;
    public float rate = 0.02f;
    float y = 0;
	
	void Update ()
    {
        y += rate;
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, max * Mathf.Sin(y));
    }
}
