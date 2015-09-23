using UnityEngine;
using System.Collections;

public class PatrollingMonster : MonoBehaviour {

    Vector3 startPos;
    private bool facingRight = true;
    public float movement;
    public float speed = 2.0f;
    public float counter = 0;
    public float distance;
    public int patrolDistance = 5;

    //Start position for the line cast
    private Vector2 startCast;
    //End position for the line cast
    private Vector2 endCast;
    //Variable to set distance of the monster's vision
    public float lineCastDistance = 0f;

    PlayerControl player;


    // Use this for initialization
    void Start ()
    { 
	    startPos = gameObject.transform.position;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

	// Update is called once per frame
	void Update () {
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
        //Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.green);
        //Making the line cast
        RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(endCast, startCast);
        //check if the collider exists and if the collider is the player
        if (EnemyVisionTrigger.collider && EnemyVisionTrigger.collider.tag == "Player")
        {
            if (player.hide == false)
            {
                //Currently set to flip the monster to see that the line cast does 
                //reverse when the patrolling monster flips and moves the other way. 
                //When content team decides what would happen when the the 
                //patrolling monster sees the player add the code in this if block

                //FlipEnemy();
                Debug.Log("You have been seen!");
                //CODE TO BE ADDED IN THE FUTURE
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
    }

}
