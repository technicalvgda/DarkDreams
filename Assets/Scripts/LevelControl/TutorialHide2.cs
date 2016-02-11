using UnityEngine;
using System.Collections;

public class TutorialHide2 : MonoBehaviour {
	GameObject fake1;
	PlayerControl player;
	public GameObject monster;
    bool destroyWall = false;
	void Start ()
    {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		//fake1 = GameObject.FindWithTag ("Fake1");

	}

	
	
	void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player" && player.hide == true)
        {
            Debug.Log("Monster coming");
            monster.SetActive(true);
            /*
            if (destroyWall == false)
            {
                destroyWall = true;
                StartCoroutine("DestroyFake");
            }
            */
			
			
		}
	}
    IEnumerator DestroyFake()
    {
       
        yield return new WaitForSeconds(3.5f);
        Destroy(fake1);


    }
}
