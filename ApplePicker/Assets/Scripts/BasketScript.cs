using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour
{
    public ScoreCounterScript scoreCounter;
    void Start()
    {
        // Find a GameObject named ScoreCounter in the Scene Hierarchy
        GameObject ScoreGO = GameObject.Find("ScoreCounter");
        // Get the ScoreCounter (Script) component of scoreGO
        scoreCounter = ScoreGO.GetComponent<ScoreCounterScript>();
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // The camera's z position sets how far to push the mouse into 3D
        // If this line causes a NullReferenceException, select the Main Camera
        // In the Hierarchy and set its tag to MainCamera in the Inspector
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

    }

    void OnCollisionEnter(Collision coll) {
        ApplePickerScript apScript = Camera.main.GetComponent<ApplePickerScript>();
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.CompareTag("Apple") || collidedWith.CompareTag("GoldenApple")){
            Destroy(collidedWith);
            // Increase the score
            AppleScript appleScript = collidedWith.GetComponent<AppleScript>();
            scoreCounter.score += appleScript.point;
            HighScoreScript.TRY_SET_HIGH_SCORE(scoreCounter.score);

        } else if (collidedWith.CompareTag("PoisonApple")){
            Destroy(collidedWith);
            apScript.AppleMissed();
        }
    }
}
