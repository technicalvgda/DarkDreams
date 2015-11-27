using UnityEngine;
using System.Collections;
 

public class StationaryMonster : MonoBehaviour
{
    AudioSource sfx;
    //tests whether enemy is looking for you
    public bool enemyActive = false;
    //tests whether enemy has seen you
    public bool canSee = false;
    private float seconds;
    private float rand;
    //reference to player script
    PlayerControl player;

    //Vision code
    private Vector2 startCast; //Start position for the line cast
    private Vector2 endCast; //End position for the line cast
    public float lineCastDistance = 0f;//Variable to set distance of the monster's vision
    private RaycastHit2D hit; //holds info of which object was seen by linecast

    Animator anim;
    

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
        //set volume to player's setting
        sfx.volume = PlayerPrefs.GetFloat("SFX");
        anim = GetComponent<Animator>();
        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        seconds = 10;             //Starts the timer at 10 seconds
        rand = Random.value* 10; //Generate a random number from 0 to 10 
        Vector2 currentPos = gameObject.transform.position;
        //initialize the starting position of linecast every frame
        startCast = currentPos;
        //initialize the end position of linecast every frame
        endCast = currentPos;
        endCast.y -= lineCastDistance;
    }
    void Update()
    {
        if(player.isAlive == false)
        {
            //changes enemy to docile animation
            anim.SetBool("Alert", false);
            anim.ResetTrigger("Kill");
            //Else it will remain off
            enemyActive = false;
        }
        Vector2 currentPos = gameObject.transform.position;
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("ENEMY: "+ currentPos.y+ "\nPLAYER: " +playerPos.position.y); //debug purposes
        // If the player is on our floor, run the script. 
        if (playerPos.position.y - 10 <= currentPos.y && currentPos.y <= playerPos.position.y + 10)
        {
            //if you are in the enemy's field of view & the enemy is active, you lose
            if (canSee && enemyActive)
            {
                //kill player (variable is in player script)
                anim.SetTrigger("Kill");
                player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                player.normalSpeed = 0;
                canSee = false;
                enemyActive = false;

            }
            // Draws the line so that you can see it in the scene and adjust the two points
            Debug.DrawLine(startCast, endCast, Color.cyan);
            //sets the monsters vision to true if the player crosses its vision
            // triggers boolean to true when the line crosses the player's layer and not the monster's own layer

            hit = Physics2D.Linecast(endCast, startCast);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {

                    canSee = true;
                }
                else
                {
                    canSee = false;
                }
            }
            else
            {
                canSee = false;
            }

            /////////timer code for enemy
            //door will open for 1 second within a random interval of 10 seconds
            if (seconds <= 0)     //When the timer drops to zero, it will reset back to 10 seconds
            {
                seconds = 10;
            }
            else
            {
                seconds -= Time.deltaTime;

                //When timer is within a certain range of the random number, it will turn the timer on
                if ((seconds < rand + 1 && seconds > rand - 1))
                {
                    //changes enemy to alert animation
                    anim.SetBool("Alert", true);
                    //play awake sound
                    sfx.Play();
                    // Debug.Log("Monster Timer is on!");
                    //enemyActive = true;
                }
                /*
                else
                {
                    //changes enemy to docile animation
                    anim.SetBool("Alert", false);
                    //Else it will remain off
                    enemyActive = false;
                }
                */

            }
        }
    }
    public void Activate()
    {
        enemyActive = true;
    }
    public void Deactivate()
    {
        anim.SetBool("Alert", false);
        enemyActive = false;
    }
    public void KillPlayer()
    {
        player.isAlive = false;
    }

}
