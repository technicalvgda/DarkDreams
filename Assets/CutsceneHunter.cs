using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneHunter : MonoBehaviour
{

    AudioSource sfx;
    public Animator anim;
    // public AudioClip[] hunterClips;

    public bool facingRight = true;
    public bool isCaught = false;
    public float movement;
    public float defaultSpeed = 5.0f;
    float speed;
    public float counter = 0;
    public float activeSpeed = 8.0f;
    public float shakeTrigger = 25.0f;
    public bool killPlayer = false;
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

    //public GameObject fogLeft;
    //public GameObject fogRight;

    //public Vector3 originalPosition;

    void Awake()
    {
      

    }

    void Start()
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
        ///////////////////
        //speed = defaultSpeed;
        sfx = this.GetComponent<AudioSource>();
        //set volume to player's setting
        sfx.volume = PlayerPrefs.GetFloat("SFX");
        

        // set lineOfSight to this objects LineRenderer component and positions
        //lineOfSight = GetComponent<LineRenderer>();
        //lineOfSight.SetPosition(1, new Vector2(lineCastDistance, 0));


        // Initialize camera
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camTransform = cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("HunterWalk") && speed == 0)
        {
            speed = defaultSpeed;
        }
        */
        // initialize the position of the linecast
        startCast = endCast = gameObject.transform.position;

        counter *= Time.deltaTime;

        // movement of monster and changes the position of linecast
        if (facingRight)     // face right
        {
            movement = speed * Time.deltaTime;
            transform.Translate(movement, 0, 0);
            endCast.x += lineCastDistance;
            startCast.y -= heightOffset;
            endCast.y -= heightOffset;
            //fogLeft.SetActive(true);
            //fogRight.SetActive(false);
        }
        



       
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
            /*
#if UNITY_IPHONE || UNITY_ANDROID
            if (PlayerPrefs.GetFloat("Vibrate") == 1 && (playerDistance.magnitude <= shakeTrigger/2))
            {
                Handheld.Vibrate();
            }          
#endif
            */
            //Calculate the X position of the next camera position
            camNewPos.x = camOriginalPos.x + (Random.insideUnitSphere.x * shakeIntensity);
            //Calculate the Y position of the next camera position
            camNewPos.y = camOriginalPos.y + (Random.insideUnitSphere.y * shakeIntensity);
            //Move the camera to the new location
            camTransform.localPosition = camNewPos;
        }

    }


    

   
    //activated at spawn, sets hunter to move, plays train whistle
    public void SetSpeed()
    {
        //sfx.Play();
        speed = defaultSpeed;


    }
    public void StopSpeed()
    {
        speed = 0;

    }

    public void PlayWhistle()
    {
        sfx.Play();

    }


}