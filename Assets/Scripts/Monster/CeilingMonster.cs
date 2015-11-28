using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CeilingMonster : MonoBehaviour
{
    Transform myTransform;          // ceiling monster
    GameObject playerObj;           // the player
    PlayerControl player;           // the player
    public GameObject endCastObj, leftCastObj, rightCastObj;
    //GameObject spottedCue;          // indicator when spotted
    Transform playerPos;

    bool isActive = true;           // check if its active or dazed
    bool isFalling = false;         // check if its falling
    bool isClimbing = false;        // check if its rising
    public bool rotate = false;
    bool rotationSwitch = false;
    double rotationTimer;

    public float stunTime = 10f;    // stun time
    public float fallSpeed = 5f;    // fall speed
    public float climbSpeed = 2f;   // climb speed
    public float detectionWidth;    // width of the ceiling monster

    Vector2 startCast;              // start position of line
    Vector2 endCast;                // end position of line
    Vector2 leftCast;               // end position of visions
    Vector2 rightCast;              // end position of visions

    float lineCastDistance = 18.5f; // length of the line cast

    void Awake()
    {
        //spottedCue = GameObject.Find("SpottedIndicator");
        myTransform = transform;
    }

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerControl>();

        // grabs the width of the ceiling monster and divides by 2
        detectionWidth = myTransform.GetComponent<Renderer>().bounds.size.x / 2;

        // set the hit linecast start and end
        startCast = endCast = myTransform.position;
        leftCast.y = rightCast.y = endCast.y -= lineCastDistance;

        // sets the width of the linecasts to detect 
        leftCast.x = endCast.x - detectionWidth;
        rightCast.x = endCast.x + detectionWidth;
        //spottedCue.SetActive(false);
    }

    void FixedUpdate()
    {
        Debug.DrawLine(startCast, endCastObj.transform.position, Color.green);        // center trigger
        Debug.DrawLine(startCast, leftCastObj.transform.position, Color.yellow);      // left trigger
        Debug.DrawLine(startCast, rightCastObj.transform.position, Color.yellow);     // right trigger
    }

    void Update()
    {
        
        Vector2 currentPos = gameObject.transform.position;
        // Vision of Ceiling Monster
        RaycastHit2D centerTrigger = Physics2D.Linecast(endCastObj.transform.position, startCast);
        RaycastHit2D leftTrigger = Physics2D.Linecast(leftCastObj.transform.position, startCast);
        RaycastHit2D rightTrigger = Physics2D.Linecast(rightCastObj.transform.position, startCast);

        // Trigger for Dropping the Ceiling Monster
       
       
        // Disables the enemy if the player is on another floor
        playerPos = playerObj.GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script. 
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 25)
        {
            ///rotation code
            if (rotate == true)
            {
                //distance pingpongs between -20 and 20 (0-40 minus 20)
                transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 10, 40)-20);
            }
            if (leftTrigger.collider && leftTrigger.collider.tag == "Player" && isActive)
            {
               
                isFalling = true;
                isClimbing = false;
            }
            else if (rightTrigger.collider && rightTrigger.collider.tag == "Player" && isActive)
            {
               
                isFalling = true;
                isClimbing = false;
            }
            else if (centerTrigger.collider && centerTrigger.collider.tag == "Player" && isActive)
            {
               
                isFalling = true;
                isClimbing = false;
            }


            // Ceiling monster is falling
            if (isFalling)
            {
                if (myTransform.position.y > endCast.y)
                {
                    // falling speed equation
                    myTransform.position -= myTransform.up * fallSpeed * Time.deltaTime;
                }
                else
                {
                    //Debug.Log("not active or falling");
                    isFalling = false;
                    isActive = false;
                }
            }

            if (isClimbing)
            {
                if (myTransform.position.y < startCast.y)
                {
                    // falling speed equation
                    myTransform.position += myTransform.up * climbSpeed * Time.deltaTime;
                }
                else
                {
                    //Debug.Log("not climbing");
                    isClimbing = false;
                    isActive = true;
                }
            }

            // Ceiling monster is dazed for 'stunTime' seconds
            if (!isActive)
            {
                //spottedCue.SetActive(false);
                StartCoroutine(DazeTimer(stunTime));
            }
        }
        else
        {
            //reset position
            transform.position = startCast;
            //do nothing
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //if player colliders with an enemy and is active
        if (isActive && col.gameObject.tag == "Player")
        {
            
            //kill player
            player.isAlive = false;
            //print("Game Over");
        }
    }

    // daze timer
    IEnumerator DazeTimer(float x)
    {
        // Debug.Log(Time.time);
        yield return new WaitForSeconds(x);
        //isActive = true;
        isClimbing = true;
        // Debug.Log(Time.time);
    }
}
