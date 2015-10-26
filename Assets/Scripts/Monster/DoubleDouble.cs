using UnityEngine;
using System.Collections;

public class DoubleDouble : MonoBehaviour {

    bool hRoomTrigger;
    Hallucination Room;

    void Start()
    {
        Room = GetComponent<Hallucination>();
    }

    // Update is called once per frame
    void Update()
    {

        // else
        //  {
        //sprite1.SetActive(false);
        //}
    }
    //checks if person is in the room
    void OnTriggerEnter2D(Collider2D col)
    {
 
        if (col.gameObject.tag == "Player")
        {      
            gameObject.SetActive(false);
            Room.hRoomTrigger = true;

        }
    }

}
