using UnityEngine;
using System.Collections;
 

public class StationaryMonster : MonoBehaviour
{
    //tests whether enemy is looking for you
    public bool enemyActive = false;
    //tests whether enemy has seen you
    public bool canSee = false;
    private float seconds;
    private float rand;
    public Transform sightStart, sightEnd; // marks the two points of the line-of-sight
  
    void Start()
    {
        seconds = 10;             //Starts the timer at 10 seconds
        rand = Random.value* 10; //Generate a random number from 0 to 10 
    }
    void Update()
    {
        //if you are in the enemy's field of view & the enemy is active, you lose
        if(canSee && enemyActive)
        {
            Debug.Log("You lose");
        }
        // Draws the line so that you can see it in the scene and adjust the two points
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.cyan);
        //sets the monsters vision to true if the player crosses its vision
        // triggers boolean to true when the line crosses the player's layer and not the monster's own layer
        canSee = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

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
            if ((seconds<rand + 1 && seconds> rand - 1))
            {
                Debug.Log("Monster Timer is on!");
                enemyActive = true;
            }
            else
            {
                //Else it will remain off
                enemyActive = false;
            }
            //Prints the timer
           //Debug.Log("Current time is: " + seconds);
        }
    }
  
}
