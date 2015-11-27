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
    float killTimer;
    float incrementTime = 0.3f;
    GameObject cover;
    bool playerInContact;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        clickPosition = new Vector2(0f, 0f);
        trapAnim = this.GetComponent<Animator>();
        killTimer = defaultTime;
        //Time.timeScale = 1f;
    }
    void Update()
    {
        /*
        if(!playerInContact)
        {
            killTimer = defaultTime;
            
        }
        */
        if(playerInContact && player.hide)
        {
            //count down
            killTimer = (killTimer - Time.deltaTime);
        }
        else
        {
            killTimer = defaultTime;
            trapAnim.SetBool("Kill", false);
            StopCoroutine("KillPlayer");
        }
        if(killTimer < 0)
        {
           StartCoroutine("KillPlayer");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if player is hiding in this trap
        if (col.tag == "Player")
        {
            playerInContact = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerInContact = false;
        }
    }

    IEnumerator KillPlayer()
    {
       
        trapAnim.SetBool("Kill", true);
        yield return new WaitForSeconds(3);
        killTimer = defaultTime;
        //player dies
        //player.hide = false;
        player.isAlive = false;
        //prevents movement
        player.normalSpeed = 0f;
        trapAnim.SetBool("Hidden", false);
        yield return null;

    }
   
}
