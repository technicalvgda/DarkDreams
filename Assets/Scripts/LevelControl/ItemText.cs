using UnityEngine;

public class ItemText : MonoBehaviour
{
    AudioHandlerScript audioHandler;
    public string dialogueToPlay;
    PlayerControl player;
    private GameObject itemTextPanel;
    Vector2 clickPosition;
    Vector2 textBoxPosition;
    // CameraFollowScript cameraScript;
    float clickOffsetY = 1;
    float clickOffsetX = 1;
    bool textActive = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;
        // GameObject.Find("ItemTextPanel");
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerScript>();
        itemTextPanel.SetActive(false);
        clickPosition = new Vector2(0f, 0f);
        // cameraScript = Camera.main.GetComponent<CameraFollowScript>();
        GetComponent<BoxCollider2D>().size = new Vector3(.5f, .2f, 0);
    }

    void Update()
    {
        float xNegPosition = transform.position.x - clickOffsetX;
        float xPosPosition = transform.position.x + clickOffsetX;
        float yPosPosition = transform.position.y + clickOffsetY;
        float yNegPosition = transform.position.y - clickOffsetY;

        // get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition)) && Input.GetMouseButtonDown(0))
        {
            if (!textActive)
            {
                // Moves the itemTextPanel to above the player
                moveTextPanel();

                itemTextPanel.SetActive(true);
                player.normalSpeed = 0f;
                textActive = true;
                audioHandler.PlayVoice(dialogueToPlay);
            }
            else if(textActive)
            {
                itemTextPanel.SetActive(false);
                player.normalSpeed = player.defaultSpeed;
                textActive = false;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if (textActive)
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

            // Moves the itemTextPanel to above the player
            moveTextPanel();

            itemTextPanel.SetActive(true);
            textActive = true;
            player.normalSpeed = 0f;
            audioHandler.PlayVoice(dialogueToPlay);
        }
        else if ((Input.GetKeyDown(KeyCode.Space)) && textActive == true)
        {
            itemTextPanel.SetActive(false);
            textActive = false;
            player.normalSpeed = player.defaultSpeed;
        }
    }

    // method to move the textPanel
    void moveTextPanel()
    {
        textBoxPosition.x = player.transform.position.x;
        textBoxPosition.y = player.transform.position.y + 8f;
        itemTextPanel.transform.position = textBoxPosition;
    }
}