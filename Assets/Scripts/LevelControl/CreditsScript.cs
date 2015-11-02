using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour
{


    public float timer = 10;
    public float speed = 5;
    void Start()
    {
        
        StartCoroutine(JumpToStart());
        
    }
    void Update()
    {
        
        //this.transform.Translate(Vector3.up * Time.deltaTime * speed);
        transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
       
    }

    IEnumerator JumpToStart()
    {
        yield return new WaitForSeconds(timer);
        Application.LoadLevel("TitleScreen");
    }


}



