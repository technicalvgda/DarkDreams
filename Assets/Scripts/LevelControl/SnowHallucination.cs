using UnityEngine;
using System.Collections;

public class SnowHallucination : MonoBehaviour {

    bool hRoomTrigger = false;
    //the sprite objects in the hallucination
    public GameObject sprite1;
    // Use this for initialization
    void Start()
    {
        //sets object inactive 
        sprite1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hRoomTrigger)
        {
            sprite1.SetActive(true);

        }
        else
        {
            sprite1.SetActive(false);


        }
    }
    //checks if person is in the room
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hRoomTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hRoomTrigger = false;
        }
    }
}
