using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ScoreScript : MonoBehaviour
{
    public static int scoreVal = 0;
    public static int highScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScoreText;
    void Start(){
        score = GameObject.FindWithTag("score").GetComponent<TextMeshProUGUI>();
        highScore = scoreVal;
        highScoreText.text = "High Score: " + highScore;
        scoreVal = 0;
        highScoreText = GameObject.FindWithTag("highscore").GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        score.text = "Score: " + scoreVal;
        if (scoreVal > highScore){
            highScore = scoreVal;
            highScoreText.text = "High Score: " + highScore;
        }
    }

}