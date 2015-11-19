using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ItemText : MonoBehaviour
{
    AudioHandlerScript audioHandler;
    public int dialogueToPlay;
    PlayerControl player;
    private GameObject itemTextPanel;
    Vector2 clickPosition;
    //CameraFollowScript cameraScript;
    float clickOffsetY = 1;
    float clickOffsetX = 1;
    bool textActive = false;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;//GameObject.Find("ItemTextPanel");
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        itemTextPanel.SetActive(false);
        clickPosition = new Vector2(0f, 0f);
        //cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }
    void Update()
    {
        float xNegPosition = transform.position.x - clickOffsetX;
        float xPosPosition = transform.position.x + clickOffsetX;
        float yPosPosition = transform.position.y + clickOffsetY;
        float yNegPosition = transform.position.y - clickOffsetY;

        ///get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition)) && Input.GetMouseButtonDown(0))
        {
            if (textActive == false)
            {
                itemTextPanel.SetActive(true);
                player.normalSpeed = 0f;
                textActive = true;
                audioHandler.PlayVoice(dialogueToPlay);
            }
            else if(textActive == true)
            {
                itemTextPanel.SetActive(false);
                player.normalSpeed = player.defaultSpeed;
                textActive = false;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if (textActive == true)
            {
                itemTextPanel.SetActive(false);
                player.normalSpeed = player.defaultSpeed;
                textActive = false;
            }
        }
      
    }
    
    void OnTriggerStay2D(Collider2D other)
    {


        if (Input.GetKeyDown(KeyCode.Space) && textActive == false)
        {
            if (other.GetComponent<PlayerControl>() == null)
            {
                return;
            }
            itemTextPanel.SetActive(true);
            textActive = true;
            player.normalSpeed = 0f;
            audioHandler.PlayVoice(dialogueToPlay);
        }
        else if ((Input.GetKeyDown(KeyCode.Space)))
        {
            itemTextPanel.SetActive(false);
            textActive = false;
            player.normalSpeed = player.defaultSpeed;
        }
    }
   

}