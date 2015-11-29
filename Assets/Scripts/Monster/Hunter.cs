using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hunter : MonoBehaviour
{

   AudioSource sfx;
   public Animator anim;
   // public AudioClip[] hunterClips;

    public bool facingRight = true;
    public bool isCaught = false;
    public float movement;
    public float speed = 5.0f;
    public float counter = 0;
    public float activeSpeed = 8.0f;
	public float shakeTrigger = 25.0f;
   
    // the X positions of the path ends
    private float leftEndPath;
    private float rightEndPath;

    // Position for the linecast
    private Vector2 startCast, endCast;
	
	// Distance to player
	private Vector2 playerDistance;

	// Original position
	public Vector3 originalPosition;
    public Quaternion originalRotation;

    //Variable to set distance of the monster's vision
    float lineCastDistance = 14f;
    float heightOffset = 9f;

    //LineRenderer to display line of sight to player
    //public LineRenderer lineOfSight;

    PlayerControl player;
    //GameObject spottedCue;
	
	// Camera references
	private GameObject cam;
	private Vector3 camOriginalPos;
	private Vector3 camNewPos;
	private Transform camTransform;
	
	private float shakeIntensity = 0.07f;

    public GameObject fogLeft;
    public GameObject fogRight;

	//public Vector3 originalPosition;

    void Awake()
    {
        //spottedCue = GameObject.Find("SpottedIndicator");  // BUGGED NULL REFERENCE
        anim = this.GetComponent<Animator>();
        leftEndPath = GameObject.Find("LeftWall").transform.position.x;
        rightEndPath = GameObject.Find("RightWall").transform.position.x;

        //set player to the object with tag "Player"
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        //Get Position
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;

		// Initialize player distance
		playerDistance = new Vector2(0f, 0f); 
        
		//retry restarts movement
		//speed = 5.0f;

    }

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
        //set volume to player's setting
        sfx.volume = PlayerPrefs.GetFloat("SFX"); 
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            FlipEnemy();
        }

        // set lineOfSight to this objects LineRenderer component and positions
        //lineOfSight = GetComponent<LineRenderer>();
        //lineOfSight.SetPosition(1, new Vector2(lineCastDistance, 0));
        sfx.Play();
		
		// Initialize camera
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		camTransform = cam.GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        // initialize the position of the linecast
        startCast = endCast = gameObject.transform.position ;

        counter *= Time.deltaTime;

        // movement of monster and changes the position of linecast
        if (facingRight)     // face right
        {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
            endCast.x += lineCastDistance;
            startCast.y -= heightOffset;
            endCast.y -= heightOffset;
            fogLeft.SetActive(true);
            fogRight.SetActive(false);
        }
        else                // face left
        {
            movement = speed * Time.deltaTime;
            transform.Translate(-movement, 0, 0);
            endCast.x -= lineCastDistance;
            startCast.y -= heightOffset;
            endCast.y -= heightOffset;
            fogLeft.SetActive(false);
            fogRight.SetActive(true);
        }

        if (gameObject.transform.position.x < leftEndPath
            || gameObject.transform.position.x > rightEndPath)
        {
            FlipEnemy();
        }

        // Visually see the line cast in scene mode, NOT GAME
        Debug.DrawLine(startCast, endCast, Color.magenta);

        /////
        RaycastHit2D[] EnemyVisionTrigger;
        EnemyVisionTrigger = Physics2D.LinecastAll(startCast, endCast);

        for (int i = 0; i < EnemyVisionTrigger.Length; i++)
        {
            RaycastHit2D hit = EnemyVisionTrigger[i];
            if (hit.collider && hit.collider.tag == "Player" && !player.hide)
            {
                isCaught = true;
                // spottedCue.SetActive(true);  // BUGGED NULL REFERENCE
                // this code runs when player is seen


            }
        }
            // Making the line cast
           // RaycastHit2D EnemyVisionTrigger = Physics2D.Linecast(startCast, endCast);

        // check if the collider exists and if the collider is the player
       
        if(isCaught == true)
        {
            //Tests which direction the monster is facing
            if (facingRight)
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(movement * activeSpeed, 0, 0);
            }
            else
            {
                //Multiply the movement by the amount set in the inspector
                transform.Translate(-movement * activeSpeed, 0, 0);
            }
        }
		
		// Proximity camera shake
		
		// Update player distance
		playerDistance.x = player.transform.position.x - gameObject.transform.position.x;
		playerDistance.y = player.transform.position.y - gameObject.transform.position.y;
		
		// Make the values positive for calculation
		if (playerDistance.x < 0)
			playerDistance.x = -playerDistance.x;
		if (playerDistance.y < 0)
			playerDistance.y = -playerDistance.y;
    }
	
	void LateUpdate()
	{
		camOriginalPos = cam.transform.localPosition;
		camNewPos = camOriginalPos;
		
		camNewPos.z = -10f;
		
		//Debug.Log("Player Distance (Mag): " + playerDistance.magnitude);
		//Debug.Log("Shake Trigger: " + shakeTrigger);
		
		if (playerDistance.magnitude <= shakeTrigger && player.isAlive)
		{
            //cause vibration on mobile
#if UNITY_IPHONE || UNITY_ANDROID
            if (PlayerPrefs.GetFloat("Vibrate") == 1 && (playerDistance.magnitude <= shakeTrigger/2))
            {
                Handheld.Vibrate();
            }          
#endif

            //Calculate the X position of the next camera position
            camNewPos.x = camOriginalPos.x + (Random.insideUnitSphere.x*shakeIntensity);
			//Calculate the Y position of the next camera position
			camNewPos.y = camOriginalPos.y + (Random.insideUnitSphere.y*shakeIntensity);
			//Move the camera to the new location
			camTransform.localPosition = camNewPos;
		}
       
	}

        
    //Function to reverse enemy movemeny position, left or right, to 
    //test if line cast flips along with the monster
    void FlipEnemy()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //When the player collides with the patrolling enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the player collides with the patrolling enemy and is not caught
        if (col.gameObject.tag == "Player" && isCaught)
        {
            //stores instance of this in player
            anim.SetBool("Kill", true);
			player.hunterScript = this;
            //Monster stops moving
            speed = 0;
            player.normalSpeed = 0;
            player.SetInvisible();
            //anim.ResetTrigger("Kill");

            ////removed fip code, possibly needs an offset

            //If monster is facing left and the player is behind the monster OR monster is facing
            //right and player is behind the monster

            /*
            if ((!facingRight && (gameObject.transform.position.x < player.transform.position.x))
                 || (facingRight && (gameObject.transform.position.x > player.transform.position.x)))
            {
                //Flip the monster to face the player
                FlipEnemy();
            }
            */
        }
    }
    public void KillPlayer()
    {
        player.isAlive = false;
       
    }

}