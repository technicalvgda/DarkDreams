using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatrollingMonster : MonoBehaviour
{

    private bool facingRight = true;
    public float speed = 8;
    public float patrolDistance = 32;
    float accumulatedDistance = 0;

    //to access methods and variables from the spawner
    //private PatrollingMonsterSpawner obj;
    PlayerControl player;
    Transform playerPos;

    // Use this for initialization
    void Start()
    {
        //Grab the spawner and get its script component
        //obj = GameObject.Find("PatrollingEnemySpawner").GetComponent<PatrollingMonsterSpawner> ();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        playerPos = player.GetComponent<Transform>();
        //If the spawner is facing left...
        //if (!obj.facingRight)
        //{   //then flip the monsters it spawns to go left
        //	FlipEnemy();
        //}
    }

    public void Set(Vector3 position, bool facingRight)
    {
        // Place the object at the specified position, flip its sprite if it's
        // currently facing a different direction, reset some of its attributes
        this.transform.position = position;
        if (this.facingRight != facingRight)
            FlipEnemy();
        this.facingRight = facingRight;
        accumulatedDistance = 0;

        this.gameObject.SetActive(true);
    }

    // Update is called once per framed
    void Update()
    {
        //Update the enemy position every frame
        Vector2 currentPos = gameObject.transform.position;

        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script.
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
        {
            float movement = speed * Time.deltaTime;
            if (!facingRight)
                transform.Translate(-movement, 0, 0);
            else
                //Enemy facing and movement to the right
                transform.Translate(movement, 0, 0);
            accumulatedDistance += movement;

            //If the monster has reached its target distance...	
            if (accumulatedDistance >= patrolDistance)
            {   //then...
                // Disable the object
                this.gameObject.SetActive(false);

                //Decrement the spawners current pool
                //obj.currentPool -= 1;
                //Destroy the enemy once it reaches its target distance
                //Destroy(gameObject);
            }
        }
    }

    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    void FlipEnemy()
    {
        //facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the player collides with the patrolling enemy and not hiding
        if (col.gameObject.tag == "Player" && player.hide == false)
        {
            //Monster stops moving
            // speed = 0;
            //deactivate monster when colliding with player
            player.SetInvisible();
            StartCoroutine("SetInactive");
        }

    }
    IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        yield return null;
    }
}        


/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatrollingMonster : MonoBehaviour {
	
	Vector3 startPos;
	private bool facingRight = true;
	float movement;
	public float speed = 2.0f;
	float distance;
	public int patrolDistance = 5;
	//to access methods and variables from the spawner
	private PatrollingMonsterSpawner obj;
    PlayerControl player;
    Transform playerPos;

    // Use this for initialization
    void Start ()
	{
		//Get the starting position of the enemy
		startPos = gameObject.transform.position;
		//Grab the spawner and get its script component
		obj = GameObject.Find("PatrollingEnemySpawner").GetComponent<PatrollingMonsterSpawner> ();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        //If the spawner is facing left...
        if (!obj.facingRight)
        {   //then flip the monsters it spawns to go left
            FlipEnemy();
        }

    }
	
	// Update is called once per framed
	void Update () 
	{	

		//Update the enemy position every frame
		Vector2 currentPos = gameObject.transform.position;

		//Calculate the distance it has traveled
		distance = currentPos.x - startPos.x;
        //Enemy facing and movement to the left

        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script.
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
        {
            if (!facingRight)
            {
                movement = speed * Time.deltaTime;
                transform.Translate(-movement, 0, 0);
            }
            //Enemy facing and movement to the right
            else
            {
                movement = speed * Time.deltaTime;
                transform.Translate(movement, 0, 0);
            }
            //If the monster has reached its target distance...	
            if (distance > patrolDistance || distance < -patrolDistance)
            {   //then...
                //Decrement the spawners current pool
                obj.currentPool -= 1;
                //Destroy the enemy once it reaches its target distance
                Destroy(gameObject);
            }
        }
        else
        {
            //do nthing
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
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the player collides with the patrolling enemy and not hiding
        if (col.gameObject.tag == "Player" && player.hide == false)
        {
            //Monster stops moving
            speed = 0;
        }
    }
}*/
