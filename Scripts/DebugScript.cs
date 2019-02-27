using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {

    public bool debug;

    public GameObject cameraRef;
    public GameObject roof;
    public GameObject hall;
    public GameObject transformSelect;

    // Use this for initialization
    void Start () {
        
        if (debug)
        {
            //if debugging put camera in debug spot
            cameraRef.transform.position = new Vector3(0, 2, 0);
            transformSelect.SetActive(true);


        }
        else if (!debug)
        {
            //turn hall option off
            hall.SetActive(false);
        }

        //make sure there is a roof/doors
        roof.SetActive(true);

	}
	
	
}
