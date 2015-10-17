using UnityEngine;
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
    GameObject spottedCue;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spottedCue = GameObject.Find("SpottedIndicator");
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
        //initialize the starting position of linecast every frame
        startCast = currentPos;
        //initialize the end position of linecast every frame
        endCast = currentPos;
        distance = currentPos.x - startPos.x;

        counter *= Time.deltaTime;
       
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
        spottedCue.SetActive(false);
        
        //Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.green);
        //Making the line cast
        RaycastHit2D[] EnemyVisionTrigger = Physics2D.LinecastAll(endCast, startCast);
		
		for (int i = 0; i < EnemyVisionTrigger.Length - 1; i++)
		{
			if(EnemyVisionTrigger[i].collider.tag == "Player")
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
					transform.Translate (-movement * visionSpeedMultiplier, 0, 0);
				}
				else 
				{
					//Multiply the movement by the amount set in the inspector
					transform.Translate (movement * visionSpeedMultiplier, 0, 0); 
				}
		}
        //set caught animation to play if spotted
        anim.SetBool("Alert", isCaught);
    }

    void LateUpdate()
	{
        if (isCaught == true)
        {
            spottedCue.SetActive(true);
        }
		else
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
}