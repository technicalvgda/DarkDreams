using UnityEngine;
using System.Collections;

public class TrapMonster : MonoBehaviour
{

    Animator trapAnim;
    PlayerControl player;
    Vector2 clickPosition;
    float clickOffsetY = 5;
    float clickOffsetX = 2;
    float defaultTime = 10.0f;
    float time;
    float incrementTime = 0.3f;
    GameObject cover;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        clickPosition = new Vector2(0f, 0f);
        trapAnim = this.GetComponent<Animator>();
        time = defaultTime;
        //Time.timeScale = 1f;
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
        ///click can is on door
        if (col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) ||
           ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) &&
            Input.GetMouseButtonDown(0))))
        {
            player.hide = true;
            if (player.hide == true)
            {
                while (time > 0 )
                {
                    time -= Time.deltaTime;
                    Debug.Log(time);
                    if (((Input.GetKeyDown(KeyCode.Space))))
                    {
                        player.hide = false;
                    }
                }
                if (time < 0)
                {
                    //play trap enemy animation
                    //trapAnim.SetTrigger("Kill");
                }
            }

            //play trap enemy animation
            //trapAnim.SetTrigger("Kill");
            time = 0;
        }

    }
    public void HidePlayer()
    {
        //make player invisible
        player.HidePlayer();
    }
    public void KillPlayer()
    {
        time = defaultTime;
        //player dies
        player.isAlive = false;
        //prevents movement
        player.normalSpeed = 0f;
        
    }
}
