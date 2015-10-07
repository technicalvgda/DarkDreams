using UnityEngine;
using System.Collections;

public class CeilingMonster : MonoBehaviour
{
    PlayerControl player;
    GameObject spottedCue;
    Vector3 startPos;
    public float distance;
    public float counter = 0;
    //Start position for the line cast
    private Vector2 startCast;
    //End position for the line cast
    private Vector2 endCast;
    //Variable to set distance of the monster's vision
    public float lineCastDistance = 0f;
    //LineRenderer to display line of sight to player
    public LineRenderer lineRenderer;
    // Use this for initialization
    void Awake()
    {
        spottedCue = GameObject.Find("SpottedIndicator");
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        spottedCue.SetActive(false);
        startPos = gameObject.transform.position;
        lineRenderer.SetPosition(1, new Vector3(0,-lineCastDistance, 0));
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = gameObject.transform.position;
        //initialize the starting position of linecast every frame
        startCast = currentPos;
        //initialize the end position of linecast every frame
        endCast = currentPos;
        distance = currentPos.x - startPos.x;
        counter *= Time.deltaTime;
        Debug.DrawLine(startCast, endCast, Color.red);
        RaycastHit2D hit = Physics2D.Linecast(endCast, startCast);
        if (hit.collider && hit.collider.tag == "Player")
        {
            ///this code runs when player is seen
                spottedCue.SetActive(true);
                print("Seen");
        }
        //Making the line cast
        //RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(endCast, startCast);
        //check if the collider exists and if the collider is the player
    }
}
