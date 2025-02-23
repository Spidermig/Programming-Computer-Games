using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameScript : MonoBehaviour
{
    // Starts the game
    public void StartGame()
    {
        SceneManager.LoadScene("_Scene_0");
    }

}
