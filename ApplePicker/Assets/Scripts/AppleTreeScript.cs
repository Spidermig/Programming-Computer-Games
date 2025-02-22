using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeScript : MonoBehaviour
{
    [Header("Inscribed")]

    // Prefab for Instantiating apples
    public GameObject applePrefab;

    public GameObject goldenApplePrefab;

    public GameObject poisonApplePrefab;

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float changeDirChance = 0.1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;


    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    void Start()
    {
        //start dropping apples
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        if(Random.Range(0,5) == 2){
            GameObject goldenApple = Instantiate<GameObject>(goldenApplePrefab);
            goldenApple.transform.position = transform.position;
        } else if (Random.Range(0,5) >= 3){
            GameObject poisonApple = Instantiate<GameObject>(poisonApplePrefab);
            poisonApple.transform.position = transform.position;
        } else {
            GameObject apple = Instantiate<GameObject>(applePrefab);
            apple.transform.position = transform.position;
        }
        Invoke("DropApple", appleDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge )
        {
            speed = Mathf.Abs(speed); //Move Right
        }
        else if (pos.x > leftAndRightEdge )
        {
            speed = -Mathf.Abs(speed); //Move left
        }
        // else if (Random.value < changeDirChance){
        //     speed *= -1; //CHange direction
        // }
        
    }
    void FixedUpdate(){
        //Random direction changes are now time-based tue to FixedUpdate()
        if (Random.value < changeDirChance) {
            speed *= -1; //change direction
        }
    }
}
