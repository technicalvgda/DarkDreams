using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
	private float horzExtent;
	//The alpha for the light bulb
	private float bulbAlpha;
	//Bool variable so the player can't activate the cutscene more than once
	private bool cutsceneActivated;
	//The canvas used in the cutscene to make the camera black out
	public CanvasGroup blackOutImage;
	//The original color of the particle system glow
	public Color defaultGlowColor;
	//The speed to how fast the fading in/out is
	public float fadingSpeed = 0.25f;
	//The variable used to change the alpha of objects
	private float fadingAlpha;
	//The variable used to change the alpha of objects that are appearing
	private float appearingAlpha;
	//The array of items
	private GameObject[] itemsGlow;
	//The light bulb
	private GameObject bulb;
	//The attic room
	public GameObject atticRoom;
	//The glow on the light bulb
	private GameObject bulbGlow;
    public int nextLevel;
    Vector2 clickPosition;
    float clickOffsetY = 5;
    float clickOffsetX = 5;

    //variables for cutscene
    GameObject player;
    PlayerControl playerScript;
    GameObject cam;
    PauseScript pause;
    GameObject lightbulb;

    public GameObject wall;
    float wallMargin;
    const float wallOffset = 41;
    // Use this for initialization
    void Awake()
    {
        clickPosition = new Vector2(0f, 0f);
    }
    void OnTriggerStay2D(Collider2D col)
    {
		//used to make an offset that creates an area to click on, which can be increased/decreased by changing the constant.
		float xNegPosition = transform.position.x - clickOffsetX;
		float xPosPosition = transform.position.x + clickOffsetX;
		float yPosPosition = transform.position.y + clickOffsetY;
		float yNegPosition = transform.position.y - clickOffsetY;

		///get position of click
		clickPosition.x = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
		clickPosition.y = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;

		//Checks to see if the cutscene has been activated yet to prevent multiple instances of the cutscene
		if (cutsceneActivated == false) 
		{
			if (col.tag == "Player" && ((Input.GetKeyDown (KeyCode.Space)) ||
				((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
				(xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) &&
				Input.GetMouseButtonDown (0)))) {
				Debug.Log ("Level completed");
				EndingCutscene ();
			}
		}
	}

    void EndingCutscene()
    {
        //setup
		cutsceneActivated = false;		
		//Get the horizontal extent of the camera
		horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		//Get the glow objects on items
		itemsGlow = GameObject.FindGameObjectsWithTag ("ParticleGlow");
		//Find the light bulb
		bulb = GameObject.Find ("LightBulb_NOT_lit_1");
		//Find the glow attached to the bulb
		bulbGlow = GameObject.Find ("LightBulbParticleGlow");
        //Get the color of the glow
        if (itemsGlow.Length > 1)
        {
            defaultGlowColor = itemsGlow[itemsGlow.Length - 1].GetComponent<ParticleSystem>().startColor;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main.gameObject;
        pause = Camera.main.GetComponent<PauseScript>();
        pause.busy = true;
        //prevent player from moving until end of cutscene
        playerScript = player.GetComponent<PlayerControl>();
        playerScript.normalSpeed = 0f;
        // Figure out where the left wall is so the camera's panning can stop there
        wallMargin = wall.transform.position.x + wallOffset;
        StartCoroutine(_Cutscene());

        
    }

    IEnumerator _Cutscene()
    {
		//Set to true so player won't be able to spam the cutscene
		cutsceneActivated = true;
		//WAit 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Lock the camera once it finishes positioning itself
        cam.GetComponent<CameraFollowScript>().enabled = false;

		//Wait 1 second
        yield return new WaitForSeconds(1);

        // Pan camera to left until it hits the wall
        while (cam.transform.position.x > wallMargin)
        {
            cam.transform.position += new Vector3(-0.3f, 0, 0);
            yield return null;
        }

		//Get the alpha value of an item
		fadingAlpha = defaultGlowColor.a;

		//While the alpha of the item is greater than 0
		while (itemsGlow[itemsGlow.Length-1/*i*/].GetComponent<ParticleSystem>().startColor.a > 0f) 
		{
			//Decrease the alpha until it reaches 0
			fadingAlpha  -= Time.deltaTime * fadingSpeed;
			//Go through all the items so they fade away at the same rate
			for(int i = 0; i < itemsGlow.Length; i++)
			{
				itemsGlow[i].GetComponent<ParticleSystem>().startColor = 
					new Color(defaultGlowColor.r,defaultGlowColor.g,defaultGlowColor.b,fadingAlpha);
			}
			yield return null;
		}
		//Reset the fading alpha
		fadingAlpha = defaultGlowColor.a;

		//Move the bulb above the UI
		bulb.GetComponent<SpriteRenderer> ().sortingLayerName = "AboveUI";

		//Move the glow above the UI
		bulbGlow.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = "AboveUI";

		//Wait 3 seconds
        yield return new WaitForSeconds(3);

		// pan back to the right side of the wall
		while ((cam.transform.position.x+horzExtent) < 
		       (atticRoom.transform.position.x + atticRoom.GetComponent<Renderer> ().bounds.extents.x))
		{
            cam.transform.position += new Vector3(0.4f, 0, 0);
            yield return null;
        }
		//Set the correct R,G,B values for the glow but alpha to 0
		bulbGlow.GetComponent<ParticleSystem> ().startColor = 
			new Color (defaultGlowColor.r,defaultGlowColor.g,defaultGlowColor.b, 0);

		//Loop to make the glow on the bulb appear.
		while (bulbGlow.GetComponent<ParticleSystem>().startColor.a < defaultGlowColor.a) 
		{
			appearingAlpha += Time.deltaTime*fadingSpeed;
			bulbGlow.GetComponent<ParticleSystem>().startColor = 
				new Color(defaultGlowColor.r,defaultGlowColor.g,defaultGlowColor.b,appearingAlpha);
			yield return null;
		}

		//Loop to make the camera black out
		while (blackOutImage.alpha  < 1f) 
		{
			blackOutImage.alpha += Time.deltaTime*fadingSpeed;
			yield return null;
		}

		//Wait 1 second
		yield return new WaitForSeconds(1);

		//Different alpha needed since the bulb alpha and bulbGlow alpha are different
		bulbAlpha = bulb.GetComponent<Renderer> ().material.color.a;

		//While the bulb is still visible make it fade
		while (bulbGlow.GetComponent<ParticleSystem>().startColor.a > 0f) 
		{
			//Make the glow fade when the bulb alpha is smaller than the bulbGlow alpha
			if(bulb.GetComponent<Renderer>().material.color.a < bulbGlow.GetComponent<ParticleSystem>().startColor.a)
			{
				fadingAlpha  -= Time.deltaTime * fadingSpeed;
				bulbGlow.GetComponent<ParticleSystem>().startColor = 
					new Color(defaultGlowColor.r,defaultGlowColor.g,defaultGlowColor.b,fadingAlpha);
			}
			bulbAlpha -= Time.deltaTime*fadingSpeed;
			bulb.GetComponent<Renderer>().material.color = new Color(1,1,1,bulbAlpha);
			yield return null;
		}
		yield return new WaitForSeconds (3);
        cam.GetComponent<CameraFollowScript>().enabled = true;
        //prevent player from moving until end of cutscene
        playerScript.normalSpeed = playerScript.defaultSpeed;
        pause.busy = false;
        yield return null;
        LoadNewLevel();
    }

    void LoadNewLevel(/*Dummy Variable for next level*/)
    {
        int i = Application.loadedLevel;
        Application.LoadLevel(i + 1);
    }
}