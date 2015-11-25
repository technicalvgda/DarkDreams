using UnityEngine;
using System.Collections;

public class FxSwing : MonoBehaviour
{
    GameObject obj;
    const int max = 20;
    const float rate = 0.02f;
    float y = 0;

    void Start()
    {
        obj = this.gameObject;
    }
	
	void Update ()
    {
        y += rate;
        obj.transform.eulerAngles = new Vector3(0, 0, max * Mathf.Sin(y));
    }
}
