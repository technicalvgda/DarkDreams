using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
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
    const float wallOffset = 40;

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
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) ||
           ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) &&
            Input.GetMouseButtonDown(0))))
        {
            Debug.Log("Level completed");
            EndingCutscene();
        }
    }

    void EndingCutscene()
    {
        //setup
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
        yield return new WaitForSeconds(0.5f);

        // Lock the camera once it finishes positioning itself
        cam.GetComponent<CameraFollowScript>().enabled = false;

        yield return new WaitForSeconds(1);

        // Pan camera to left until it hits the wall
        while (cam.transform.position.x > wallMargin)
        {
            cam.transform.position += new Vector3(-0.1f, 0, 0);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        // pan back to player
        while (cam.transform.position.x < player.transform.position.x)
        {
            cam.transform.position += new Vector3(0.2f, 0, 0);
            yield return null;
        }
        cam.GetComponent<CameraFollowScript>().enabled = true;
        //prevent player from moving until end of cutscene
        playerScript.normalSpeed = playerScript.defaultSpeed;
        pause.busy = false;
        yield return null;
        LoadNewLevel();
    }

    void LoadNewLevel(/*Dummy Variable for next level*/)
    {
        //int x = nextLevel; Level+"x"
        Application.LoadLevel(Application.loadedLevel); //change loadedLevel to next level when appropiriate
    }
}