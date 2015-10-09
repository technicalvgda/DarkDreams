using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CeilingMonster : MonoBehaviour
{
    Transform myTransform;          // ceiling monster
    PlayerControl player;           // the player
    GameObject spottedCue;          // indicator when spotted

    bool isActive = true;           // check if its active or dazed
    bool isFalling = false;         // check if its falling
    bool isClimbing = false;        // check if its rising

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
        // Vision of Ceiling Monster
        RaycastHit2D centerTrigger = Physics2D.Linecast(endCast, startCast);
        RaycastHit2D leftTrigger = Physics2D.Linecast(leftCast, startCast);
        RaycastHit2D rightTrigger = Physics2D.Linecast(rightCast, startCast);

        // Trigger for Dropping the Ceiling Monster

       
       if (leftTrigger.collider && leftTrigger.collider.tag == "Player")
       {
                spottedCue.SetActive(true);
                isFalling = true;
                isClimbing = false;
       }
       else if (rightTrigger.collider && rightTrigger.collider.tag == "Player")
       {
                spottedCue.SetActive(true);
                isFalling = true;
                isClimbing = false;
       }
       else if (centerTrigger.collider && centerTrigger.collider.tag == "Player")
       {
                spottedCue.SetActive(true);
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
            }
        }

        // Ceiling monster is dazed for 'stunTime' seconds
        if (!isActive)
        {
            spottedCue.SetActive(false);
            StartCoroutine(DazeTimer(stunTime));
        }
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
