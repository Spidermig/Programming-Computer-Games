using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode{
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // a private Singleton
    [Header("Inscribed")]
    public Text uitLevel; // The UIText_Level text
    public Text uitShots; // The UIText_Shots text
    public Vector3 castlePos; // The place to put the castles
    public GameObject[] castles; // An array of the castles

    [Header("Dynamic")]
    public int level; // The current level
    public int levelMax; // The number of levels
    public int shotsTaken;
    public GameObject castle; // The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode

    void Start()
    {
        S = this; // Define the slingshot

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel(){
        // Get rid of the old castle if one exists
        if (castle != null){
            Destroy(castle);
        }

        // Destroy all old projectiles if they exist
        Projectile.DESTROY_PROJECTILES();

        // Instantiate the new castles
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        // Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        // Zoom out to show both
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI(){
        // Show the data in the GUITexts
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken;
    }

    void Update()
    {
        UpdateGUI();

        // Check for level end
        if((mode==GameMode.playing) && Goal.goalMet){
            // Change the GameMode to stop checking for levelEnd
            mode = GameMode.levelEnd;
            // Zoom out to show both
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);
            // Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel(){
        level++;
        if (level == levelMax){
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    // Static method that allows code to incrememnt anywhere
    static public void SHOTS_FIRED(){
        S.shotsTaken++;
    }

    // Static method that allows code to get a reference to S.castle
    static public GameObject GET_CASTLE(){
        return S.castle;
    }

}
