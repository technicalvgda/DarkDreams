using UnityEngine;
using System.Collections;

public class PatrolingMonster : MonoBehaviour {

    Vector3 startPos;
    public bool facingRight = false;
    public float movement;
    public float speed = 2.0f;
    public float counter = 0;
    public float distance;
    public int patrolDistance = 5;


	// Use this for initialization
	void Start () { 
	    startPos = gameObject.transform.position;
    }

	// Update is called once per frame
	void Update () {
        Vector3 currentPos = gameObject.transform.position;

        distance = currentPos.x - startPos.x;

        counter *= Time.deltaTime;

        if (!facingRight)
        {
            movement = speed * Time.deltaTime;
            transform.Translate(-movement, 0, 0);
        }

        else {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
        }

        Debug.Log(distance);

        if (distance > patrolDistance)
        {
            distance = 0;
            facingRight = false;
            startPos = gameObject.transform.position;
        }

        else if (distance < -patrolDistance) {
            distance = 0;
            facingRight = true;
            startPos = gameObject.transform.position;
        }
	}

}
