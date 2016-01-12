using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScore : MonoBehaviour {

    int highScore = 0;
    public int currentScore = 1;
    Text scoreText;
    public Text currentText;
	// Use this for initialization
	void Start ()
    {
        scoreText = this.GetComponent<Text>();
        //track high score of max floor reached in nightmare tower
       
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        highScore = PlayerPrefs.GetInt("HighScore");
        scoreText.text = highScore.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if the players score on this run is higher than the high score
        currentText.text = currentScore.ToString();
        if(currentScore > highScore)
        {
            //set high score to the players current score
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
            scoreText.text = highScore.ToString();


        }
        
	
	}
}
