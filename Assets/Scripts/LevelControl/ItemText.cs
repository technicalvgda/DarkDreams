using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ItemText : MonoBehaviour{
    PlayerControl player;
    private GameObject itemTextPanel;
    bool clickedItem = false;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        itemTextPanel = GameObject.Find("ItemTextPanel");
        itemTextPanel.SetActive(false);
    }
    void OnTriggerStay2D(Collider2D other){
        if (Input.GetKeyDown(KeyCode.Space) || clickedItem == true)
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