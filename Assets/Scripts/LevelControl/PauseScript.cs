
using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{

    //public GameObject Canvas;
    bool pause = false;

    void Start()
    {
        //Canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == true)
            {
                Time.timeScale = 1.0f;
                //Canvas.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pause = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                //Canvas.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pause = true;
            }
        }
    }
   
}