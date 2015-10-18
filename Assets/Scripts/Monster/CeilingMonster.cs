using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CeilingMonster : MonoBehaviour
{
    Transform myTransform;          // ceiling monster
    PlayerControl player;           // the player
    GameObject spottedCue;          // indicator when spotted
    Transform playerPos;

    bool isActive = true;           // check if its active or dazed
    bool isFalling = false;         // check if its falling
    bool isClimbing = false;        // check if its rising
    bool isCaught = false;          // check to see if player is spotted

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
        spottedCue = GameObject.Find("SpottedIndicator");
        myTransform = transform;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

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
        Debug.DrawLine(startCast, endCast, Color.green);        // center trigger
        Debug.DrawLine(startCast, leftCast, Color.yellow);      // left trigger
        Debug.DrawLine(startCast, rightCast, Color.yellow);     // right trigger
    }

    void Update()
    {
        Vector2 currentPos = gameObject.transform.position;
        // Vision of Ceiling Monster
        RaycastHit2D centerTrigger = Physics2D.Linecast(endCast, startCast);
        RaycastHit2D leftTrigger = Physics2D.Linecast(leftCast, startCast);
        RaycastHit2D rightTrigger = Physics2D.Linecast(rightCast, startCast);
        //saves original location to keep inactive while climbing
        float originalLoc = myTransform.position.y;
        // Trigger for Dropping the Ceiling Monster
        isCaught = false;
        spottedCue.SetActive(false);
        // Disables the enemy if the player is on another floor
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script. 
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 25)
        {
            //only works if ceilling monster is not climbing. keeps the celling monster inactive if it is. 
            if (isClimbing == false) {
                if (leftTrigger.collider && leftTrigger.collider.tag == "Player")
                {
                    isCaught = true;
                    isFalling = true;
                    isClimbing = false;
                }
                else if (rightTrigger.collider && rightTrigger.collider.tag == "Player")
                {
                    isCaught = true;
                    isFalling = true;
                    isClimbing = false;
                }
                else if (centerTrigger.collider && centerTrigger.collider.tag == "Player")
                {
                    isCaught = true;
                    isFalling = true;
                    isClimbing = false;
                }
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
            //do nothing
        }
    }

    void LateUpdate()
    {
		if (isCaught == true)
		spottedCue.SetActive (true);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //if player colliders with an enemy and is active
        if (isActive && col.gameObject.tag == "Player")
        {
            player.isAlive = false;
            //print("Game Over");
        }
    }

    // daze timer
    IEnumerator DazeTimer(float x)
    {
        // Debug.Log(Time.time);
        yield return new WaitForSeconds(x);
        isActive = true;
        isClimbing = true;
        // Debug.Log(Time.time);
    }
}
