﻿using UnityEngine;
using System.Collections;

public class Halluscination : MonoBehaviour {
    bool hRoomTrigger = false;
    //the sprite objects in the hallucination
    public GameObject sprite1, sprite2, sprite3, sprite4, sprite5;
    // Use this for initialization
    void Start()
    {
        //sets object inactive 
        sprite1.SetActive(false);
        sprite2.SetActive(false);
        sprite3.SetActive(false);
        sprite4.SetActive(false);
        sprite5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hRoomTrigger)
        {
            sprite1.SetActive(true);
            sprite2.SetActive(true);
            sprite3.SetActive(true);
            sprite4.SetActive(true);
            sprite5.SetActive(true);
        }
        else
        {
            sprite1.SetActive(false);
            sprite2.SetActive(false);
            sprite3.SetActive(false);
            sprite4.SetActive(false);
            sprite5.SetActive(false);

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
