using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ApplePickerScript : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject basketPrefab;
    public List<GameObject> basketList;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacing = 2f;

    private ScoreCounterScript scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();
        for(int i=0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>( basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacing * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);

        }
        // Find ScoreCounterScript in the scene
        GameObject scoreCounterGO = GameObject.Find("ScoreCounter");
        if (scoreCounterGO != null)
        {
            scoreCounter = scoreCounterGO.GetComponent<ScoreCounterScript>();
        }
        else
        {
            Debug.LogError("ScoreCounter GameObject not found in the scene.");
        }

    }

    public void AppleMissed() {
        // Destroy all of the falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        GameObject[] GoldenAppleArray = GameObject.FindGameObjectsWithTag("GoldenApple");
        
        foreach(GameObject tempGO in appleArray){
            Destroy(tempGO);
        }
        
        foreach(GameObject tempGO in GoldenAppleArray){
            Destroy(tempGO);
        }
        
        // Destroy one of the Baskets
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        // Remove the Basket from the list and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // If there are no more Baskets left, restart the game
        if (basketList.Count == 0) {
            if (scoreCounter != null) {
                HighScoreScript.TRY_SET_HIGH_SCORE(scoreCounter.score); // Save high score
                PlayerPrefs.SetInt("FinalScoreText", scoreCounter.score);  // Save final score
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene("GameOverScreen"); // Load Game Over Scene
        }
    }
}
