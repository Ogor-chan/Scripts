using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}
