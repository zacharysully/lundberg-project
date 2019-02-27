using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript_2 : MonoBehaviour
{

    public static CameraScript_2 S;
    RaycastHit hit;

    public Scenemanager sceneMangRef;

    //gameObject camera is looking at
    private GameObject currentlyBeingstaredAt;
    public GameObject CurrentlyBeingstaredAt
    {
        get
        {
            return currentlyBeingstaredAt;
        }
    }

    
    public LayerMask layerMask;

    private void Awake()
    {
        S = this;

        //set up scenemanager ref
        /*if (this.gameObject.GetComponent<Scenemanager>() != null)
        {
            sceneMangRef = this.gameObject.GetComponent<Scenemanager>();
        }
        else
        {
            sceneMangRef = this.gameObject.AddComponent<Scenemanager>();
        }*/

        
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, layerMask) && !sceneMangRef.stopLooking)
        {
            //print(hit.collider.gameObject + "Camera2");
            //if camera ray hitting a "stareAt" GO
            if (hit.collider.gameObject.GetComponent<StarePoint>() != null)
            {
                //set currently being stared at to that GO
                currentlyBeingstaredAt = hit.collider.gameObject;
                //sceneMangRef.transitioning = true;

                //print(currentlyBeingstaredAt + " is currently being stared at.");

                // Activate Reticle Timer
                GameObject.Find("Loader_Image").GetComponent<CircularTimerScript>().isActive = true;
                return;
            }
            else
            {
                //print("not looking at starepoint");
                // Deactivate Reticle Timer
                GameObject.Find("Loader_Image").GetComponent<CircularTimerScript>().isActive = false;
            }
        }
            //if not looking at stuff set it to null
            currentlyBeingstaredAt = null;
        // Deactivate Reticle Timer
        GameObject.Find("Loader_Image").GetComponent<CircularTimerScript>().isActive = false;
        //sceneMangRef.transitioning = false;
    }
}
