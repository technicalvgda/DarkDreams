using UnityEngine;
using System.Collections;

public class RoomBorder : MonoBehaviour {
    //used to refrence the ChasingMonster script
    ChasingMonster chase;
    //used for initilazation
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //checks if the collider object is ChasingEnemy, is so flip it.
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.name == "ChasingEnemy")
        {
            chase = col.GetComponent<ChasingMonster>();
            chase.FlipEnemy();
        }
    }
}