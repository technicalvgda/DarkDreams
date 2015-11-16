using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hunter : MonoBehaviour
{

    

   AudioSource sfx;
   // public AudioClip[] hunterClips;

    public bool facingRight = true;
    public bool direction;
    private bool isCaught = false;
    public float movement;
    public float speed = 5.0f;
    public float counter = 0;
    public float activeSpeed = 8.0f;

    // the X positions of the path ends
    private float leftEndPath;
    private float rightEndPath;

    // Position for the linecast
    private Vector2 startCast, endCast;

    //Variable to set distance of the monster's vision
    float lineCastDistance = 14f;
    float heightOffset = 12f;

    //LineRenderer to display line of sight to player
    //public LineRenderer lineOfSight;

    PlayerControl player;
    //GameObject spottedCue;

    void Awake()
    {
        //spottedCue = GameObject.Find("SpottedIndicator");  // BUGGED NULL REFERENCE

        leftEndPath = GameObject.Find("LeftWall").transform.position.x;
        rightEndPath = GameObject.Find("RightWall").transform.position.x;

        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
        //set volume to player's setting
        sfx.volume = PlayerPrefs.GetFloat("SFX"); 
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            FlipEnemy();
        }

        // set lineOfSight to this objects LineRenderer component and positions
        //lineOfSight = GetComponent<LineRenderer>();
        //lineOfSight.SetPosition(1, new Vector2(lineCastDistance, 0));
        sfx.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // initialize the position of the linecast
        startCast = endCast = gameObject.transform.position;

        counter *= Time.deltaTime;

        // movement of monster and changes the position of linecast
        if (facingRight)     // face right
        {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
            endCast.x += lineCastDistance;
            endCast.y -= heightOffset;
        }
        else                // face left
        {
            movement = speed * Time.deltaTime;
            transform.Translate(-movement, 0, 0);
            endCast.x -= lineCastDistance;
            endCast.y -= heightOffset;
        }

        if (gameObject.transform.position.x < leftEndPath
            || gameObject.transform.position.x > rightEndPath)
        {
            FlipEnemy();
        }

        // Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.magenta);

        // Making the line cast
        RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(startCast, endCast);

        // check if the collider exists and if the collider is the player
        if (EnemyVisionTrigger.collider && EnemyVisionTrigger.collider.tag == "Player" && !player.hide)
        {
            isCaught = true;
            // spottedCue.SetActive(true);  // BUGGED NULL REFERENCE
            // this code runs when player is seen

           
        }
        if(isCaught == true)
        {
            //Tests which direction the monster is facing
            if (facingRight)
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(movement * activeSpeed, 0, 0);
            }
            else
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(-movement * activeSpeed, 0, 0);
            }
        }
    }

    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    void FlipEnemy()
    {
        facingRight = !facingRight;
        direction = facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //When the player collides with the patrolling enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the player collides with the patrolling enemy and is not caught
        if (col.gameObject.tag == "Player" && isCaught)
        {
            //Monster stops moving
            speed = 0;
            //If monster is facing left and the player is behind the monster OR monster is facing
            //right and player is behind the monster
            if ((!facingRight && (gameObject.transform.position.x < player.transform.position.x))
                 || (facingRight && (gameObject.transform.position.x > player.transform.position.x)))
            {
                //Flip the monster to face the player
                FlipEnemy();
            }
        }
    }
}