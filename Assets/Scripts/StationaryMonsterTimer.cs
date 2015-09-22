using UnityEngine;
using System.Collections;

//Steven Lim and Michael Cassell  

public class StationaryMonsterTimer : MonoBehaviour
{

    public bool enemyActive = false;
    private float seconds;
    private float rand;

    void Start()
    {
        seconds = 10;             //Starts the timer at 10 seconds
        rand = Random.value* 10; //Generate a random number from 0 to 10 
    }
    void Update()
    {
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
                Debug.Log("Monster Timer is off!");
                enemyActive = false;
            }
            //Prints the timer
            Debug.Log("Current time is: " + seconds);
        }
    }
}
