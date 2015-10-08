using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitMenuScript : MonoBehaviour {


    public Button yesQuitText;
    public Button noQuitText;


    // Use this for initialization
    void Start () {

        yesQuitText = yesQuitText.GetComponent<Button>();
        noQuitText = noQuitText.GetComponent<Button>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}












    public void YesPress()

    {
        Application.Quit();
    }

    public void NoPress()

    {
        //quitMenu.enabled = false;
        yesQuitText.enabled = true;
        noQuitText.enabled = true;

    }



}
