using UnityEngine;
using System.Collections;

public class DarknessAI2 : MonoBehaviour {





    public Transform target;
    public float moveSpeed;

    private Transform myTransform;


    void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(target.position, myTransform.position, Color.blue);
    }

    // Update is called once per frame
    void Update()
    {

        if (target.position.x < myTransform.position.x)
        {
            myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
        }
        else
            if (target.position.x > myTransform.position.x)
        {
            myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
        }

    }
}


