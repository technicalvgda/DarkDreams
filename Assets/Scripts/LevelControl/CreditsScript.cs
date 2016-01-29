using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour
{

    AudioHandlerScript audioHandler;
    public GameObject creditStopper;
    public GameObject skipButton;
    float timer =136;
    public float speed = 5;
    void Start()
    {
        skipButton.gameObject.SetActive(false);
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        audioHandler.LoopMusic(false);
        //play credits music
        audioHandler.PlayMusic(2);
        StartCoroutine(JumpToStart());
        
    }
    void Update()
    {
        if (creditStopper.transform.position.y < 0)
        {
            //this.transform.Translate(Vector3.up * Time.deltaTime * speed);
            transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
        }
        if(Input.GetButton("Fire1"))
        {
            skipButton.gameObject.SetActive(true);
        }
    }

    IEnumerator JumpToStart()
    {
        yield return new WaitForSeconds(timer);
        //load title
        Application.LoadLevel(1);
    }

    public void ClickedSkip()
    {
        Application.LoadLevel(1);
    }


}



