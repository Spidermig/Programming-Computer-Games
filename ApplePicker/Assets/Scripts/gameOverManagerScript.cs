using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverManagerScript : MonoBehaviour
{
    public Text highScoreText;
    public Text finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 1000);

        finalScoreText.text = "Final Score: " + finalScore;
        highScoreText.text = "High Score: " + highScore;
    }

    // Restart the game
    public void PlayAgain()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
