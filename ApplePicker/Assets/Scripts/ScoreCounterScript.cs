using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI classes like Text

public class ScoreCounterScript : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    private Text uiText;
    void Start()
    {
        uiText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = score.ToString();
     }
}
