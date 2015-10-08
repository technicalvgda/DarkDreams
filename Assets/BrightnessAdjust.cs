using UnityEngine;
using System.Collections;

public class BrightnessAdjust : MonoBehaviour {




   public Transform Brightty;

	// Use this for initialization
	void Start () {


        //(Brightty)GetComponent<SpriteRenderer>().color = Color.green;
        //this.getComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        SpriteRenderer spRend = this.transform.GetComponent<SpriteRenderer>();
        spRend.color = new Color(1f, 1f, 1f, 0f);
    }
	
	// Update is called once per frame
	void Update () {


        //SpriteRenderer RearEnder = this.GetComponent<Transform>;
        //RearEnder.color = new Color(1f, 1f, 1f, 0f);// is about 100 % transparent(Cant be seen at all, but still active)



    }
}
