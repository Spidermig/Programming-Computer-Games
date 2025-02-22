using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickerScript : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacing = 2f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>( basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacing * i);
            tBasketGO.transform.position = pos;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
