using UnityEngine;
using System.Collections;

public class Eclipse : MonoBehaviour {
    AudioHandlerScript audioHandler;
    PlayerControl player;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 movementDist = new Vector3(3f, 0,0);
    float speed = 1.0f;
    bool goodnight = false;
   
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        if(Application.loadedLevelName == "Ending Level" || Application.loadedLevelName == "Nightmare")
        {
            audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
            player.normalSpeed = 0;
            
        }
        startPos = this.transform.position;
        //move it over based upon level
        //application.loaded level - 1 gets the levels proper level number (tutorial is lvl 1)
        //levelOffset += new Vector3((Application.loadedLevel - 1) + 5, 0, 0);
        //startPos += levelOffset;
        endPos = startPos + movementDist;
    }
    void Update()
    {
        if (this.transform.position.x < endPos.x)
        {
            transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
        }
        else
        {
            if ((Application.loadedLevelName == "Ending Level"|| Application.loadedLevelName == "Nightmare") && goodnight == false)
            {
                goodnight = true;
                audioHandler.PlayVoice(17);
                player.normalSpeed = player.defaultSpeed;
            }
        }

    }
	
	
   

   
}
