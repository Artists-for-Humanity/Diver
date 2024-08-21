using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMesh Pro namespace

public class ScoreScript : MonoBehaviour
{
    public static int scoreVal = 0;
    public TextMeshProUGUI score; // Change the type to TextMeshProUGUI

    void Start(){
        // Get the TextMeshProUGUI component from the GameObject with the tag "score"
        score = GameObject.FindWithTag("score").GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        score.text = "Score: " + scoreVal;
    }

}