using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineREnderCript : MonoBehaviour {

    public LineRenderer lineRenderer;

    public CameraScript cameraScript;

	// Use this for initialization
	void Start () {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, cameraScript.rayStartpoint);
        lineRenderer.SetPosition(0, cameraScript.rayEndpoint);
    }
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, cameraScript.rayStartpoint);
        lineRenderer.SetPosition(0, cameraScript.rayEndpoint);
    }

}
