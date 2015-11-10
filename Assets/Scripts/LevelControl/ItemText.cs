using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ItemText : MonoBehaviour
{
    //PlayerControl player;
    private GameObject itemTextPanel;
    Vector2 clickPosition;
    //CameraFollowScript cameraScript;
    float clickOffsetY = 1;
    float clickOffsetX = 1;
    void Start()
    {
       // player = GameObject.Find("Player").GetComponent<PlayerControl>();
        itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;//GameObject.Find("ItemTextPanel");
        itemTextPanel.SetActive(false);
        clickPosition = new Vector2(0f, 0f);
        //cameraScript = Camera.main.GetComponent<CameraFollowScript>();
    }
    void OnTriggerStay2D(Collider2D other)
    {

        float xNegPosition = transform.position.x - clickOffsetX;
        float xPosPosition = transform.position.x + clickOffsetX;
        float yPosPosition = transform.position.y + clickOffsetY;
        float yNegPosition = transform.position.y - clickOffsetY;

        ///get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetKeyDown(KeyCode.Space) || ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) && Input.GetMouseButtonDown(0)))
        {
            if (other.GetComponent<PlayerControl>() == null)
            {
                return;
            }
            itemTextPanel.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        itemTextPanel.SetActive(false);
    }

}