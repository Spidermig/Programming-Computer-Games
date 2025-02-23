using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public static float bottomY = -20f;
    public int point = 100;
    // Update is called once per frame
    void Update()
    {
        // Check if this is a Golden/Poison Apple and update points accordingly
        if (gameObject.CompareTag("GoldenApple")) {
            point = 200;
        } else if (gameObject.CompareTag("PoisonApple")){
            point = 0;
        }
        if (transform.position.y < bottomY) {
            Destroy(this.gameObject);

            if(!gameObject.CompareTag("PoisonApple")){
                // Get a reference to the ApplePicker component of Main Camera
                ApplePickerScript apScript = Camera.main.GetComponent<ApplePickerScript>();
                // Call the public AppleMissed() method of apScript
                apScript.AppleMissed();
            }
        }
    }
}
