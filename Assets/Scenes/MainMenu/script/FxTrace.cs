using UnityEngine;
using System.Collections;

public class FxTrace : MonoBehaviour
{
    Transform t;

    void Awake() { t = this.transform; }

    public void Run()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(_Run());
    }

    IEnumerator _Run()
    {
        // Bottom left to top left
        t.position = new Vector3(-3.61f, -1, 0);
        t.eulerAngles = new Vector3(0, 0, 90);
        yield return null;
        while (t.position.y < 1.53f)
        {
            t.position += new Vector3(0, .65f, 0);
            yield return null;
        }
        t.position = new Vector3(-3.61f, 1.63f, 0);
        yield return null;

        // Top left to top right
        t.position = new Vector3(-2.8f, 2.6f, 0);
        t.eulerAngles = new Vector3(0, 0, 0);
        yield return null;
        while (t.position.x < 2.8f)
        {
            t.position += new Vector3(.7f, 0, 0);
            yield return null;
        }
        t.position = new Vector3(2.8f, 2.6f, 0);
        yield return null;

        // Top right to bottom right
        t.position = new Vector3(3.61f, 1.63f, 0);
        t.eulerAngles = new Vector3(0, 0, 90);
        yield return null;
        while (t.position.y > -0.9f)
        {
            t.position += new Vector3(0, -.65f, 0);
            yield return null;
        }
        t.position = new Vector3(3.61f, -1, 0);

        // Bottom right to bottom left
        t.position = new Vector3(2.8f, -1.95f, 0);
        t.eulerAngles = new Vector3(0, 0, 0);
        yield return null;
        while (t.position.x > -2.76f)
        {
            t.position += new Vector3(-.7f, 0, 0);
            yield return null;
        }
        t.position = new Vector3(-2.8f, -1.95f, 0);
        yield return null;

        gameObject.SetActive(false);
    }
}
