using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileLine : MonoBehaviour
{
    static List <ProjectileLine> PROJ_LINES = new List<ProjectileLine>();
    private const float DIM_MULT = 0.75f;
    private LineRenderer _line;
    private bool _drawing = true;
    private Projectile _projectile;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _line.SetPosition(0, transform.position);

        _projectile = GetComponentInParent<Projectile>();
        ADD_LINE(this);
    }

    void FixedUpdate(){
        if(_drawing){
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, transform.position);
            // If the rigidbody is sleeping, stop drawing
            if (_projectile != null){
                if (!_projectile.awake){
                    _drawing = false;
                    _projectile = null;
                }
            }
        }
    }

    private void OnDestroy(){
        // Remove this from projectile line
        PROJ_LINES.Remove(this);
    }

    static void ADD_LINE(ProjectileLine newLine){
        Color col;
        // Iterate over all the old lines and dim them
        foreach(ProjectileLine pl in PROJ_LINES){
            col = pl._line.startColor;
            col = col * DIM_MULT;
            pl._line.startColor = pl._line.endColor = col;
        }
        // Add newlines to the list
        PROJ_LINES.Add(newLine);
    }
}
