using UnityEngine;
using System.Collections;

public class FxStreak : MonoBehaviour
{
    public SpriteRenderer body;
    Transform t;
    float x = 0;

    void Awake() { t = this.transform; }

    public void Activate(float x)
    {
        this.gameObject.SetActive(true);
        this.x = x;
        StopAllCoroutines();
        StartCoroutine(_Enter());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        StartCoroutine(_Exit());
    }

    IEnumerator _Enter()
    {
        // INIT
        t.position = new Vector3(x, 41, 0);
        body.color = Color.white;

        // EXEC
        while (t.position.y > 0)
        {
            t.position += new Vector3(0, -1, 0);
            yield return null;
        }
        t.position = new Vector3(x, 0, 0);

        while (body.color.a > .4f)
        {
            body.color += new Color(0, 0, 0, -.04f);
            yield return null;
        }
        body.color = new Color(1, 1, 1, .4f);
    }

    IEnumerator _Exit()
    {
        // INIT
        t.position = new Vector3(x, 0, 0);

        // EXEC
        while (t.position.y > -10)
        {
            t.position += new Vector3(0, -1, 0);
            yield return null;
        }
        t.position = new Vector3(x, -10, 0);
        this.gameObject.SetActive(false);
    }
}
