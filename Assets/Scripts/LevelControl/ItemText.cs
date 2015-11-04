using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ItemText : MonoBehaviour{
    PlayerControl player;
    private GameObject itemTextPanel;
    bool clickedItem = false;
    Vector2 clickPosition;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        clickPosition = new Vector2(0f, 0f);
        itemTextPanel = transform.Find("UICanvas/Overlay/ItemTextPanel").gameObject;//GameObject.Find("ItemTextPanel");
        itemTextPanel.SetActive(false);
    }
    void OnTriggerStay2D(Collider2D other){
        clickPosition.x = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        clickPosition.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if ((Input.GetKeyDown(KeyCode.Space) || clickedItem == true) || (other.OverlapPoint(clickPosition) && Input.GetMouseButtonDown(0)))
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
    void OnMouseDown()
    {
        clickedItem = true;
    }
    void OnMouseUp()
    {
        clickedItem = false;
    }
}