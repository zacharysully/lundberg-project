using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTextDetectionScript : MonoBehaviour {

    public static CameraTextDetectionScript S;

    RaycastHit hit;

    public DisplayText textBeingLookedAt;


    private void Awake()
    {
        S = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 15f, 1 << LayerMask.NameToLayer("Text")))
        {
            //print(hit.collider.gameObject);
            //if camera ray hitting a "text" GO
            if (hit.collider.gameObject.GetComponent<DisplayText>() != null)
            {
                //set text being looked at to text being hit by ray
                textBeingLookedAt = hit.collider.gameObject.GetComponent<DisplayText>();

                return;
            }
        }
        else
        {

            textBeingLookedAt = null;
            //print("false");
        }
    }
}
