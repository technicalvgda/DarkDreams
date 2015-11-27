using UnityEngine;
using System.Collections;

public class Double : MonoBehaviour {
   // bool hRoomTrigger;
    Hallucination roomScript;
    GameObject doubleRoom;
    public bool setting = false;

    void Start()
    {
        doubleRoom = GameObject.Find("HallucinationRoomMirror(Clone)");
        roomScript = doubleRoom.GetComponent<Hallucination>();
       
    }
    // Update is called once per frame
    void Update()
    {

    }
    //checks if person is in the room
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            roomScript.hRoomTrigger = false;
            gameObject.SetActive(false);
        }
    }
}
