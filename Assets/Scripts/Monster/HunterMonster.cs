using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HunterMonster : MonoBehaviour {

    private Vector2 currentPos;

    private bool facingRight = true;
    public float movement;
    public float speed = 10.0f;
    public float counter = 0;
    public float distance;
	public float visionSpeedMultiplier = 2.0f;
    public int patrolDistance = 5;

    // the X positions of the path ends
    private float leftEndPath = -36;
    private float rightEndPath = 62;

    // Position for the linecast
    private Vector2 startCast, endCast;

    //Variable to set distance of the monster's vision
    public float lineCastDistance = 5f;

    //LineRenderer to display line of sight to player
    public LineRenderer lineOfSight;

    PlayerControl player;
    Vector2 playerPos;
    GameObject spottedCue;
    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
        spottedCue = GameObject.Find("SpottedIndicator");
    }
    
    void Start ()
    {
        //spottedCue.SetActive(false);

        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        // set lineOfSight to this objects LineRenderer component and positions
        lineOfSight = GetComponent<LineRenderer>();
        lineOfSight.SetPosition(1, new Vector2(lineCastDistance, 0));
    }

	// Update is called once per frame
	void Update ()
    {
        playerPos = player.transform.position;

        // initialize the position of the linecast
        currentPos = myTransform.transform.position;
        startCast = endCast = currentPos;       

        counter *= Time.deltaTime;
       
        // movement of monster and changes the position of linecast
        if (facingRight)            // face right
        {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
            endCast.x += lineCastDistance;
        }
        else if (!facingRight)      // face left
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

        //Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.magenta);

        //Making the line cast
        RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(endCast, startCast);

        //check if the collider exists and if the collider is the player
        if (EnemyVisionTrigger.collider && EnemyVisionTrigger.collider.tag == "Player")
        {
            ///this code runs when player is seen
			if (!player.hide)
            {
                spottedCue.SetActive(true);

                //Tests which direction the monster is facing
                if (!facingRight) 
				{
					//Multiply the movement by the amount set in the inspector
					transform.Translate (-movement * visionSpeedMultiplier, 0, 0);
				}
				else 
				{
					//Multiply the movement by the amount set in the inspector
					transform.Translate (movement * visionSpeedMultiplier, 0, 0); 
				}
			}
            else
            {
                spottedCue.SetActive(false);
            }
            
		}

    }

    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    void FlipEnemy()
    {
       
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        spottedCue.SetActive(false);
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
			if((!facingRight && (myTransform.transform.position.x < player.transform.position.x))
			   	|| (facingRight && (myTransform.position.x > player.transform.position.x)))
			{
				//Flip the monster to face the player
				FlipEnemy();
			}
		}
	}

    void setDirection(bool x) {
        facingRight = x;
    }    
}