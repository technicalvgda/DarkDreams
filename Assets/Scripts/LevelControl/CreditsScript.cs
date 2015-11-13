using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour
{

    AudioHandlerScript audioHandler;
    public float timer = 10;
    public float speed = 5;
    void Start()
    {
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        audioHandler.LoopMusic(false);
        //play credits music
        audioHandler.PlayMusic(2);
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



