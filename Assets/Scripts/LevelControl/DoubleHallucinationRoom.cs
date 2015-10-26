using UnityEngine;
using System.Collections;

public class DoubleHallucinationRoom : MonoBehaviour
{

   public GameObject clone;
    GameObject player;
    PlayerControl playerScript;
    bool facingR, facingL;             //shows which direction the clone is pointing
	// Use this for initialization
	void Start ()
    {
        facingR = false;
        facingL = false;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        clone.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerPos = player.transform.position;
        Quaternion playerRot = player.transform.rotation;

        if (playerScript.facingRight)
        {
          
            
            if(facingL)
            {
                clone.transform.Translate(playerScript.movement.x * -1, 0, 0);
            }

            else
            {
                facingL = true;
                clone.transform.rotation = playerRot; //works because when the player faces Right it has 0 degrees, and when the double faces left (which we want) it has 0 degrees
                facingR = false;
            }
            
        }
        else if(!playerScript.facingRight)
        {

            

            if (facingR)
            {
                clone.transform.Translate(playerScript.movement.x, 0, 0);
            }

            else
            {
                facingR = true;
                playerRot.y = playerRot.y + 180;
                clone.transform.rotation = playerRot;
                facingL = false;
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
                facingL = true;
            }
            else if(!playerScript.facingRight)
            {
                playerPos.x = playerPos.x - 55;
                playerRot.y = playerRot.y + 180;
                clone.transform.position = playerPos;
                clone.transform.rotation = playerRot;
                facingR = true;
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
