using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public string key; //holds user input
    public float speed = 10.0f;

	// Use this for initialization
	void Start () //what happens as soon as player is created
    {
        


	}
	
	// Update is called once per frame
	void Update () //happens every frame
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(xMovement,0,0);
    }
}
