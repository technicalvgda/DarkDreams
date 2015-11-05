using UnityEngine;
using System.Collections;

public class Double : MonoBehaviour {
    bool hRoomTrigger;
    Hallucination Room;
    public bool setting = false;

    void Start()
    {
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
            gameObject.SetActive(false);
        }
    }
}
