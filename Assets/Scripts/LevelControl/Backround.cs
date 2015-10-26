using UnityEngine;
using System.Collections;

public class Backround : MonoBehaviour {
    int here = 0;
    private SpriteRenderer sprite;
    public int orderlayering = 2;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (here % 2 == 0)
        {
            sprite.sortingOrder = orderlayering;
            sprite.sortingLayerName = "Default";
        }
        else {
            sprite.sortingOrder = -1;
            sprite.sortingLayerName = "backround";
        }
        if (here % 3 == 0) {
            here = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("HELLO");
        if (col.gameObject.tag == "Player") { 
            here++;
            Debug.Log(here);
        }
        
    }
}
