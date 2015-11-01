using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
    public int nextLevel;
    Vector2 clickPosition;
    float clickOffsetY = 5;
    float clickOffsetX = 5;

    // Use this for initialization
    void Awake()
    {
        clickPosition = new Vector2(0f, 0f);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        //used to make an offset that creates an area to click on, which can be increased/decreased by changing the constant.
        float xNegPosition = transform.position.x - clickOffsetX;
        float xPosPosition = transform.position.x + clickOffsetX;
        float yPosPosition = transform.position.y + clickOffsetY;
        float yNegPosition = transform.position.y - clickOffsetY;

        ///get position of click
        clickPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        clickPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (col.tag == "Player" && ((Input.GetKeyDown(KeyCode.Space)) ||
           ((yNegPosition < clickPosition.y && clickPosition.y < yPosPosition) &&
            (xNegPosition < clickPosition.x && clickPosition.x < xPosPosition) &&
            Input.GetMouseButtonDown(0))))
        {
            Debug.Log("Level completed");
            LoadNewLevel();
        }
    }

    void LoadNewLevel(/*Dummy Variable for next level*/)
    {
        //int x = nextLevel; Level+"x"
        Application.LoadLevel(Application.loadedLevel); //change loadedLevel to next level when appropiriate
    }
}