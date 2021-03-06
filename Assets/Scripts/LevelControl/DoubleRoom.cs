﻿using UnityEngine;
using System.Collections;

public class DoubleRoom : MonoBehaviour {

    public GameObject clone;
    GameObject player;
    PlayerControl playerScript;
    bool facingR, facingL;
    private bool left = false;
    private bool right = false;
    private float negative = -1;
    private float negative2 = 1;
    // Use this for initialization
    void Start()
    {
        facingR = false;
        facingL = false;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        clone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 playerPos = player.transform.position;
        Quaternion playerRot = player.transform.rotation;

        if (facingR)
        {
            clone.transform.Translate(playerScript.movement.x * negative, 0, 0);
            if (!playerScript.facingRight && left == false)
            {

                clone.transform.rotation = playerRot;
                left = true;
                right = false;
                negative = negative * -1;
            }
            else if (playerScript.facingRight && right == false)
            {
                playerRot.y = playerRot.y + 180;
                clone.transform.rotation = playerRot;
                left = false;
                right = true;
                negative = negative * -1;
            }
        }
        else if (facingL)
        {
            clone.transform.Translate(playerScript.movement.x * negative2, 0, 0);
            if (!playerScript.facingRight && left == false)
            {
                clone.transform.rotation = playerRot;
                left = true;
                right = false;
                negative2 = negative2 * -1;
            }
            else if (playerScript.facingRight && right == false)
            {
                playerRot.y = playerRot.y + 180;
                clone.transform.rotation = playerRot;
                left = false;
                right = true;
                negative2 = negative2 * -1;
            }
        }
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 playerPos = player.transform.position;
            Quaternion playerRot = player.transform.rotation;

            clone.SetActive(true);

            if (playerScript.facingRight)
            {
                playerPos.x = playerPos.x + 55;
                playerRot = player.transform.rotation;
                clone.transform.position = playerPos;
                clone.transform.rotation = playerRot;
                facingR = true;
            }
            else if (!playerScript.facingRight)
            {
                playerPos.x = playerPos.x - 55;
                playerRot.y = playerRot.y + 180;
                clone.transform.position = playerPos;
                clone.transform.rotation = playerRot;
                facingL = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            clone.SetActive(false);
            facingR = false;
            facingL = false;
        }
    }
}
