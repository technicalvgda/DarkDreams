using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChasingMonster : MonoBehaviour
{
    // The monster patrols at speedNormal speed and shifts to speedChasing
    // when it spots the player
    public float speedNormal = 2;
    public float speedChasing = 4;
   //I commented this out since it is no longer needed for the script
   //but kept it for future reference if needed
   // public int patrolDistance = 20;      
    float accumulatedDistance;         
  
   
  

    // With periodicPause on, the object moves for moveDuration seconds and
    // pauses for pauseDuration seconds before moving again. Set in inspector
    public bool periodicPause;
    public float moveDuration;          
    public float pauseDuration;
    float moveTime;
    float pauseTime;
    public bool pause = false;
    // Direction is multiplied with any value that depends on the facing
    // direction (such as speed). 1 = right, -1 = left
    public bool facingRight = true;
    int direction = 1;

    private bool isChasing = false;

    // Length of the monster's vision
    public float lineCastDistance = 0;

    // LineRenderer to display line of sight to player
    public LineRenderer lineOfSight;

    Animator anim;
    PlayerControl player;
    Transform playerPos;
    //GameObject spottedCue;
   

    void Awake()
    {
        
        anim = GetComponent<Animator>();
        //spottedCue = GameObject.Find("SpottedIndicator");
    }

    // Use this for initialization
    void Start()
    {
       //transform.GetComponent<Collider2D>().attachedRigidbody.AddForce(0, 0);
        //spottedCue.SetActive(false);

        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        playerPos = player.GetComponent<Transform>();

        //set lineOfSight to this objects LineRenderer component
        lineOfSight = GetComponent<LineRenderer>();

        //set position of end node of line renderer to the same distance as its line cast
        lineOfSight.SetPosition(1, new Vector3(lineCastDistance, 0, 0));
    }
    

    void Update()
    {
        // If this object is not on the same elevation as the player, do nothing
        if (this.transform.position.y < playerPos.position.y - 10 ||
            this.transform.position.y > playerPos.position.y + 10)
        {
            anim.enabled = false;
            return;
        }
        else
        {
            anim.enabled = true;
        }
     
            


        // Check for collision with the player
        // If nothing detected, keep patrolling. Otherwise chase the player
        // If the object has reached the distance limit, it gives up on chasing the player
        
        if (!CheckForPlayer())
        {
            if (!pause)
            {
                
                MoveTurn(speedNormal);
            }
        }
        else
            MoveTurn(speedChasing);
      
    }

    // Checks for player collision with the line cast
    // Returns true if there is a collision (i.e. the player is spotted)
    bool CheckForPlayer()
    {
        // Start and end points of the line cast
        // Place the end point a distance in the direction the enemy's facing
        Vector2 startCast = this.transform.position + new Vector3(0, -1, 0);
        Vector2 endCast = startCast + new Vector2(lineCastDistance * direction, 0);

        // reinitialize caught detection
        bool isPlayerSeen = false;
        isChasing = false;
        //spottedCue.SetActive(false);

        //Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.green);
        //Making the line cast

        // An array is being returned every frame. Why don't we just use rectangle collision?
        RaycastHit2D[] EnemyVisionTrigger = Physics2D.LinecastAll(endCast, startCast);

        for (int i = 0; i < EnemyVisionTrigger.Length - 1; i++)
        {
            if (EnemyVisionTrigger[i].collider.tag == "Player")
            {
                isPlayerSeen = true;
                pause = false;
            }
            // How big is this array? Might want to break once a single collision is found
        }

        // Start chasing if the player is seen outside of a hiding spot
        isChasing = isPlayerSeen && !player.hide;

        //set caught animation to play if spotted
        anim.SetBool("Alert", isChasing);

        if (isChasing)
        {
            // Refreshes the enemy's patrol timers.
            // i.e. If the player walks into the enemy's vision DURING ITS 'PAUSED' STATE
            // and successfully outruns it, it will revert to the moving state instead of
            // the paused state

            moveTime = 0;
            pauseTime = 0;

            // Slows the player down
            player.slowMo = true;
            return true;
        }
        return false;
    }

    // The object moves at the given speed, then turns around when it reaches
    // the EXACT travel distance limit.
    void MoveTurn(float speed)
    {

        // Waits out the pause period
        if (pauseTime > 0)
        {
            pauseTime -= Time.deltaTime;
            return;
        }

        // Determines the distance traveled this frame and clamp it if it
        // happens to make the object go outside of its range.
      float  movement = speed * Time.deltaTime;
       // float movement = Mathf.Min
         //   (speed * Time.deltaTime,
           //  patrolDistance - accumulatedDistance);

         // Moves the enemy in the direction it's facing
        transform.Translate(movement * direction, 0, 0);

        // Moves the enemy and notes the distance traveled
      //  accumulatedDistance += movement;

        //I commented this out since it is no longer needed for the script
        //but kept it for future reference if needed 
        /*// Flips enemy once it has traveled the full distance
        if (accumulatedDistance >= patrolDistance)
        {
            accumulatedDistance = 0;
            FlipEnemy();
        }*/

        // After the enemy has moved for moveTime seconds, pause it
        moveTime += Time.deltaTime;
        if (periodicPause && moveTime >= moveDuration)
        {
            moveTime = 0;
            pauseTime = pauseDuration;
        }
    }

    void LateUpdate()
    {
        // If the player outruns the monster, return them to the normal speed
        // why is this running every frame tho
        if (isChasing == false)
            player.slowMo = false;
    }
    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    public void FlipEnemy()
    {
        pause = false;
        facingRight = !facingRight;
        direction *= -1;
        //checks true the first time it passes
       
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //player.slowMo = false;
        //spottedCue.SetActive(false);
       
    }

    //When the player collides with the patrolling enemy
    void OnTriggerEnter2D(Collider2D col)
    {
		
        //If the player collides with the patrolling enemy and not hiding
        if (col.gameObject.tag == "Player" && player.hide == false)
        {
            player.chasingMonsterScript = this;
            // Stop the monster; it will not be able to return to the normal speed.
            speedNormal = speedChasing = 0;

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
    public void PauseEnemy()
    {
        pause = true;

    }
    public void UnpauseEnemy()
    {

        pause = false;
    }

}






/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChasingMonster : MonoBehaviour
{

    Vector3 startPos;
    private bool facingRight = true;
    private bool isCaught = false;
    private bool isPlayer = false;
    public float movement;
    public float speed = 2.0f;
    public float counter = 0;
    public float distance;
	public float visionSpeedMultiplier = 2.0f;
    public int patrolDistance = 5;
    Transform playerPos;

    //Start position for the line cast
    private Vector2 startCast;
    //End position for the line cast
    private Vector2 endCast;
    //Variable to set distance of the monster's vision
    public float lineCastDistance = 0f;
    //LineRenderer to display line of sight to player
    public LineRenderer lineOfSight;
    Animator anim;
    PlayerControl player;
    //GameObject spottedCue;

    void Awake()
    {
        anim = GetComponent<Animator>();
        //spottedCue = GameObject.Find("SpottedIndicator");
    }
    // Use this for initialization
    void Start ()
    {
       
        //spottedCue.SetActive(false);
        startPos = gameObject.transform.position;
        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        //set lineOfSight to this objects LineRenderer component
        lineOfSight = GetComponent<LineRenderer>();
        //set position of end node of line renderer to the same distance as its line cast
        lineOfSight.SetPosition(1, new Vector3(lineCastDistance, 0, 0));
    }

	// Update is called once per frame
	void Update ()
	{
        Vector2 currentPos = gameObject.transform.position;
        Vector2 visionPos = new Vector2(currentPos.x, currentPos.y - 1);
        //initialize the starting position of linecast every frame
        startCast = visionPos;
        //initialize the end position of linecast every frame
        endCast = visionPos;
        distance = visionPos.x - startPos.x;

        counter *= Time.deltaTime;

        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script. 
        if (playerPos.position.y-10 <= currentPos.y && currentPos.y <= playerPos.position.y+10)
        {
            //enemy facing left change position of line cast to follow change of enemy position
            if (!facingRight)
            {
                movement = speed * Time.deltaTime;
                transform.Translate(-movement, 0, 0);
                //change x position to keep the line cast a certain distance. 
                //THIS IS FOR THE MONSTER FACING LEFT
                endCast.x -= lineCastDistance;
            }
            //enemy facing right change position of line cast to follow enemy change of position
            else
            {
                movement = speed * Time.deltaTime;
                transform.Translate(movement, 0, 0);
                //change x position to keep the line cast a certain distance. 
                //THIS IS FOR THE MONSTER FACING RIGHT
                endCast.x += lineCastDistance;
            }

            //Debug.Log(distance);

            if (distance > patrolDistance || distance < -patrolDistance)
            {
                distance = 0;
                //facingRight = !facingRight;
                FlipEnemy();
                startPos = gameObject.transform.position;
            }
            // reinitialize caught detection
            isCaught = false;
            isPlayer = false;
            //spottedCue.SetActive(false);

            //Visually see the line cast in scene mode, NOT GAME
            Debug.DrawLine(startCast, endCast, Color.green);
            //Making the line cast
            RaycastHit2D[] EnemyVisionTrigger = Physics2D.LinecastAll(endCast, startCast);

            for (int i = 0; i < EnemyVisionTrigger.Length - 1; i++)
            {
                if (EnemyVisionTrigger[i].collider.tag == "Player")
                    isPlayer = true;
            }
            //check if the collider exists and if the collider is the player
            if (isPlayer == true && player.hide == false)
            {
                isCaught = true;
                // Upon player collision with linecast/monster-vision, their speed is reduced
                player.slowMo = true;

                //Tests which direction the monster is facing
                if (!facingRight)
                {
                    //Multiply the movement by the amount set in the inspector
                    transform.Translate(-movement * visionSpeedMultiplier, 0, 0);
                }
                else
                {
                    //Multiply the movement by the amount set in the inspector
                    transform.Translate(movement * visionSpeedMultiplier, 0, 0);
                }
            }
        }
        else
        {
            //do nothing
        }
        //set caught animation to play if spotted
        anim.SetBool("Alert", isCaught);
    }

    void LateUpdate()
	{
        if (isCaught == false)
        {
            player.slowMo = false;
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
        //player.slowMo = false;
        //spottedCue.SetActive(false);
    }
	//When the player collides with the patrolling enemy
	void OnTriggerEnter2D(Collider2D col)
	{
		//If the player collides with the patrolling enemy and not hiding
		if (col.gameObject.tag == "Player" && player.hide == false)
		{
			//Monster stops moving
			speed = 0;
			//If monster is facing left and the player is behind the monster OR monster is facing
			//right and player is behind the monster
			if((!facingRight && (gameObject.transform.position.x < player.transform.position.x))
			   	|| (facingRight && (gameObject.transform.position.x > player.transform.position.x)))
			{
				//Flip the monster to face the player
				FlipEnemy();
			}
		}
	}    
}*/

