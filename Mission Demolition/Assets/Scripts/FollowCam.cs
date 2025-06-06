using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S; // Another private Slingshot
    static public GameObject POI; // The static point of interest

    [Header("Inscribed")]
    public float easing = 0.5f;
    public Vector2 minXY = Vector2.zero; // Vector2.zero is [0,0]
    public GameObject viewBothGO;

    public enum eView{none, slingshot, castle,both};

    [Header("Dynamic")]
    public float camZ; //The desired Z pos of the camera
    public eView nextView = eView.slingshot;

    void Awake(){
        S = this;
        camZ = this.transform.position.z;
    }

    void FixedUpdate(){
        
        Vector3 destination = Vector3.zero;

        if (POI != null){
            // If the POI has a Rigidbody, check to see if it is sleeping
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping()){
                POI = null;
            }
        }

        if (POI != null) {
            destination = POI.transform.position;
        }
        // Limit the destination values of destination.x & destination.y
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Interpolate from the current camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        // Set the orthographicSize of the Camera to keep Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }

    public void SwitchView(eView newView){
        if(newView == eView.none){
            newView = nextView;
        }
        switch(newView){
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                nextView = eView.slingshot;
                break;
        }
    }
    
    public void SwitchView(){
        SwitchView(eView.none);
    }

    static public void SWITCH_VIEW(eView newView){
        S.SwitchView(newView);
    }
}
