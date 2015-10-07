using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CeilingMonster : MonoBehaviour {
    Transform myTransform;          // ceiling monster
    PlayerControl player;           // the player

    bool isActive = true;
    bool isFalling = false;                  
    bool isCollide = false;         // check if collision
    bool isDaze = false;
    bool isClimbing = false;

    float stunTime = 10f;           // stun time
    float fallSpeed = 5f;
    float climbSpeed = 2f;

    Vector2 startCast;              // start position of line cast
    Vector2 endCast;                // end position of line cast

    float lineCastDistance = 18.5f;   // length of the line cast

    void Awake() {
        myTransform = transform;
    }

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        // set the hit linecast start and end
        startCast = endCast = myTransform.position;
        endCast.y -= lineCastDistance;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(startCast, endCast, Color.yellow);
    }

    void Update()
    {
        // Vision of Ceiling Monster
        RaycastHit2D trigger = Physics2D.Linecast(endCast, startCast);

        // Trigger for Dropping the Ceiling Monster
        if (trigger.collider && trigger.collider.tag == "Player")
        {
            isFalling = true;
            isClimbing = false;

        }

        // Ceiling monster is falling
        if (isFalling == true) {
            if (myTransform.position.y > endCast.y)
            {
                // falling speed equation
                myTransform.position -= myTransform.up * fallSpeed * Time.deltaTime;
            }
            else
            {
                Debug.Log("not active or falling");
                isFalling = false;
                isActive = false;
                isDaze = true;
            }
        }

        if (isClimbing == true) {
            if (myTransform.position.y < startCast.y)
            {
                // falling speed equation
                myTransform.position += myTransform.up * climbSpeed * Time.deltaTime;
            }
            else
            {
                Debug.Log("not climbing");
                isClimbing = false;
            }
        }

        // Ceiling monster is dazed for 'stunTime' seconds
        if (isDaze == true) {
            StartCoroutine(DazeTimer(stunTime));
        }

        // Game Over
        if (isActive == true && isCollide == true) {
            Debug.Log("Game Over");
        }
    }

    // daze timer
    IEnumerator DazeTimer(float x) {
        Debug.Log(Time.time);
        isDaze = false;
        yield return new WaitForSeconds(x);
        isActive = true;
        isClimbing = true;
        Debug.Log(Time.time);
    }
}
