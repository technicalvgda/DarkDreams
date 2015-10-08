using UnityEngine;
using System.Collections;

public class CeilingMonster : MonoBehaviour
{
    PlayerControl player;
    GameObject spottedCue;
    Vector3 startPos;
    float movement;
    public float speed = 20.0f;
    private float distance = 16.14f;
    //Start position for the line cast
    private Vector2 startCast;
    //End position for the line cast
    private Vector2 endCast;
    //Variable to set distance of the monster's vision
    public float lineCastDistance = 0f;
    // Use this for initialization
    private bool stunned = false;
    private int counter = 0;
    public float stunTime = 5f;
    float timeRemaining;
    void Awake()
    {
        spottedCue = GameObject.Find("SpottedIndicator");
    }
    void Start()
    {
        Vector2 currentPos = gameObject.transform.position;
        timeRemaining = stunTime;
        // set the hit linecast start and end
        startCast = endCast = currentPos;
        spottedCue.SetActive(false);
        startPos = gameObject.transform.position;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        endCast.y -= lineCastDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 find = gameObject.transform.position;
        Debug.DrawLine(startCast, endCast, Color.green);
        RaycastHit2D hit = Physics2D.Linecast(endCast, startCast);
        movement = speed * Time.deltaTime;
        if (hit.collider && hit.collider.tag == "Player" && stunned == false)
        {
            spottedCue.SetActive(true);
            //Multiply the movement by the amount set in the inspector
            transform.Translate(0, -movement, 0);
            counter++;
        }
        //if (gameObject.transform.position.x >= maxXPosition)
        else if (counter != 0)
        {
            stunned = true;
            transform.Translate(0, movement, 0);
            counter--;
        }
        ///counts the amount of time before stun wears off
        else if (stunned) {
            spottedCue.SetActive(false);
            timeRemaining -= Time.deltaTime;
            //print(timeRemaining);
            if (timeRemaining <= 0) {
                stunned = false;
                timeRemaining = stunTime;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if player colliders with an enemy and is not hidden
        if (col.gameObject.tag == "Player")
        {
            player.isAlive = false;
            //print("Game Over");
        }
    }
}
