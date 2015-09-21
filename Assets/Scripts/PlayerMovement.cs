using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour 
{
    //speed variables
    
    public float playerSpeed;                     // final magnitude of speed, the player's speed
    public bool slowMo;                         //boolean that toggles slow motion
    public float normalSpeed = 10.0f;       //normal speed magnitude
    public float slowMoSpeed = 5.0f;        //speed magnitude when slowMo is activaed

   
    


    // Use this for initialization
    void Start() //what happens as soon as player is created
    {
        slowMo = false;  //slowMo starts out as false since the player hasn't hit the button yet
        

	}
	
	// Update is called once per frame
	void Update() //happens every frame
    {

        if (Input.GetKeyDown(KeyCode.E)) //when the player presses the "e" key, it toggles slowMo
        {
            slowMo = !slowMo;
            Debug.Log("toggle");  //so we can check how many times it toggles per keyhit, it is temporary
        }



        if(slowMo) //when slowMo is true, the player will move at half speed
        {
            playerSpeed = slowMoSpeed;
        }

        else  //when slowMo is false, the player will move normaly
        {
            playerSpeed = normalSpeed;
        }



        float xMovement = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        transform.Translate(xMovement,0,0);
    }
}