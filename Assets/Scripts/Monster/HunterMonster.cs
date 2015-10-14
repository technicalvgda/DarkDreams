using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HunterMonster : MonoBehaviour
{

    private Vector2 currentPos;

    private bool facingRight = true;
    private bool isCaught = false;
    public float movement;
    public float speed = 5.0f;
    public float counter = 0;
    public float distance;
    public float activeSpeed = 10.0f;
    public int patrolDistance = 5;

    // the X positions of the path ends
    private float leftEndPath;
    private float rightEndPath;

    // Position for the linecast
    private Vector2 startCast, endCast;

    //Variable to set distance of the monster's vision
    public float lineCastDistance = 8f;

    //LineRenderer to display line of sight to player
    public LineRenderer lineOfSight;

    PlayerControl player;
    GameObject spottedCue;
    GameObject leftWall, rightWall;
    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
        spottedCue = GameObject.Find("SpottedIndicator");

        leftWall = GameObject.Find("LeftWall");
        rightWall = GameObject.Find("RightWall");

        leftEndPath = leftWall.transform.position.x;
        rightEndPath = rightWall.transform.position.x;
    }

    void Start()
    {
        //spottedCue.SetActive(false);

        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        // set lineOfSight to this objects LineRenderer component and positions
        lineOfSight = GetComponent<LineRenderer>();
        lineOfSight.SetPosition(1, new Vector2(lineCastDistance, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // initialize the position of the linecast
        startCast = endCast = myTransform.transform.position;

        counter *= Time.deltaTime;

        // movement of monster and changes the position of linecast
        if (facingRight)            // face right
        {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
            endCast.x += lineCastDistance;
        }
        else                        // face left
        {
            movement = speed * Time.deltaTime;
            transform.Translate(-movement, 0, 0);
            endCast.x -= lineCastDistance;
        }

        //Debug.Log(distance);

        if (gameObject.transform.position.x < leftEndPath
            || gameObject.transform.position.x > rightEndPath)
        {
            FlipEnemy();
        }

        isCaught = false;
        spottedCue.SetActive(false);

        // Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.magenta);

        // Making the line cast
        RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(endCast, startCast);

        // check if the collider exists and if the collider is the player
        if (EnemyVisionTrigger.collider && EnemyVisionTrigger.collider.tag == "Player" && !player.hide)
        {
            isCaught = true;
            ///this code runs when player is seen

            //Tests which direction the monster is facing
            if (!facingRight)
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(-movement * activeSpeed, 0, 0);
            }
            else
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(movement * activeSpeed, 0, 0);
            }
        }
    }

    void LateUpdate()
    {
        if (isCaught)
        {
            spottedCue.SetActive(true);
        }
    }

    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    void FlipEnemy()
    {
        facingRight = !facingRight;
        Vector2 theScale = myTransform.transform.localScale;
        theScale.x *= -1;
        myTransform.transform.localScale = theScale;
        // spottedCue.SetActive(false);
    }

    //When the player collides with the patrolling enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the player collides with the patrolling enemy and not hiding
        if (col.gameObject.tag == "Player" && !player.hide)
        {
            //Monster stops moving
            speed = 0;
            //If monster is facing left and the player is behind the monster OR monster is facing
            //right and player is behind the monster
            if ((!facingRight && (myTransform.transform.position.x < player.transform.position.x))
                 || (facingRight && (myTransform.position.x > player.transform.position.x)))
            {
                //Flip the monster to face the player
                FlipEnemy();
            }
        }
    }
}