using UnityEngine;
using System.Collections;

public class DoubleDouble : MonoBehaviour {

    bool hRoomTrigger = false;
    Hallucination Room;
    public GameObject sprite1, sprite2, sprite3, sprite4, sprite5, sprite6, sprite7, sprite8, sprite9;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hRoomTrigger)
        {
        sprite1.SetActive(false);
        sprite2.SetActive(false);
        sprite3.SetActive(false);
        sprite4.SetActive(false);
        sprite5.SetActive(false);
        sprite6.SetActive(false);
        sprite7.SetActive(false);
        sprite8.SetActive(false);
        sprite9.SetActive(false);
          }
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
            hRoomTrigger = true;
            gameObject.SetActive(false);
            
        }
    }

}
