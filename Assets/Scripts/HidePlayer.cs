using UnityEngine;
using System.Collections;
//Jimmy Chao And Rone and Narith and my mom and google-senpai 
//We spent 4 hours 
public class HidePlayer : MonoBehaviour
{
    bool hideplayer = false;
    GameObject TempPlayer;
    private SpriteRenderer sprite;
    int sortingOrder = 0;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
            }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "TempCover") 
        {

            if (Input.GetKeyDown(KeyCode.H))
            {
                hideplayer = !hideplayer;
                Debug.Log("I pressed the hide buttion");
                if (hideplayer) {
                    sprite.sortingOrder = sortingOrder;
                    GameObject.Find("TempPlayer").GetComponent<PlayerMovement>().enabled = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.U)) {
                hideplayer = !hideplayer;
                sprite.sortingOrder = 1;
                GameObject.Find("TempPlayer").GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {  
    }
  
}