using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{
    GameObject player;
    private Transform playerTrans;//our player character
    private PlayerControl playerScript;
    public float transitionDuration = 2.0f;
    public Transform target;
    bool transition = false;
    public bool follow = true;
    public float yOffset = 7;
    void Awake()//on start up
    {
        //finds the gameobject with the player tag and stores its transform component

        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.GetComponent<Transform>();
        playerScript = player.GetComponent<PlayerControl>();
    }
    void Update()//for every frame:
    {
        if (follow == true && transition == false)
        {
            //stores a vector 2 which contains the player's x and y position
            Vector2 cameraPosition = new Vector2(playerTrans.position.x, playerTrans.position.y + yOffset);//define cameraPosition as player position
            //sets the position of this transform (the camera) to the x and y position of the stored position, does not change z position
            transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);//this transform (camera game object)
        }
        else
        {
            if (!transition) StartCoroutine(Transition());
           // StartCoroutine(Transition2());
        }
    }//position is camera position

    IEnumerator Transition()
    {
        transition = true;
        playerScript.normalSpeed = 0;
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        Vector3 endPos = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
        while (transform.position != endPos)//t < 1f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }
        playerScript.normalSpeed = playerScript.defaultSpeed;
        transition = false;
       
        
    }
    /*
    IEnumerator Transition2()
    {
        float t = 0.0f;
        Vector3 endPos = transform.position;
        while (t < 2.5f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            Vector3 playerPosition = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(endPos, playerPosition, t);
            yield return 0;
        }

    }
    */

}