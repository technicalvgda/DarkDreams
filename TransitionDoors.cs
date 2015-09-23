//Alejandra Gonzalez
using UnityEngine;
using System.Collections;

public class TransitionDoors : MonoBehaviour {
    public Transform source; //door 1 
    public Transform destination; //door 2

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            GoThrough(other, destination);
        }
    }

    void GoThrough(Collider2D other, Transform end)
    {
        //Player is teleported to the other door
        other.transform.position = end.transform.position;
        
    }

}
