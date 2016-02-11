using UnityEngine;
using System.Collections;

public class TutorialHide2 : MonoBehaviour
{
    GameObject fake1;
    PlayerControl player;
    public GameObject monster;
    bool destroyWall = false;
    bool playerContact = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        //fake1 = GameObject.FindWithTag ("Fake1");

    }

    void Update()
    {
        if (player.hide == true && playerContact == true)
        {
            monster.SetActive(true);
        }
    }
    /*
	void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player" && player.hide == true)
        {
           
            
            if (destroyWall == false)
            {
                destroyWall = true;
                StartCoroutine("DestroyFake");
            }
            
			
			
		}
	}
*/
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            playerContact = true;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("Player not touching");
            playerContact = false;

        }
    }
    IEnumerator DestroyFake()
    {

        yield return new WaitForSeconds(3.5f);
        Destroy(fake1);


    }
}