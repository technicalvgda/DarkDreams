using UnityEngine;
using System.Collections;

public class PatrolWholeFloorScript : MonoBehaviour {
    //used to refrence the ChasingMonster script
    public ChasingMonster chase;
    //used for initilazation
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    //checks if the collider object is ChasingEnemy, is so flip it.
    void OnTriggerExit2D(Collider2D col)
    {
        
        if (col.gameObject.name == "ChasingEnemy")
        {
            chase = col.GetComponent<ChasingMonster>();
            chase.FlipEnemy();
        }
    }
}