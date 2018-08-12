using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour {

   
    int bestHighScore;

    public TextMeshProUGUI bestScoreText, bestScoreText2;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("HighScore") != null)
        {
            bestHighScore = PlayerPrefs.GetInt("HighScore");
        }
	}


    private void Update()
    {
        bestScoreText.text = bestHighScore.ToString();
        bestScoreText2.text = bestHighScore.ToString();
    }



    public void CheckNewHighScore(int currentHighScore) {
        if (currentHighScore > bestHighScore) {
            bestHighScore = currentHighScore;
            PlayerPrefs.SetInt("HighScore", currentHighScore);
            Debug.Log("New High Score is " + currentHighScore);
        }


    }
	
	
}
