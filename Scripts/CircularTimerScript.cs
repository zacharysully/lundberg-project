using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularTimerScript : MonoBehaviour {

    public bool isActive;

    public static GameObject C;

    public float barProgress;

    // timer amount in seconds
    public float waitTime = 1.5f;

	// Use this for initialization
	void Start () {
        isActive = false;
        C = this.gameObject;

        // start at zero percent filled
        barProgress = 0f;
        C.GetComponent<Image>().fillAmount = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        //print(isActive);

		if (isActive){
            // begin progress filling
            barProgress += 1.0f / waitTime * Time.deltaTime;
            C.GetComponent<Image>().fillAmount = barProgress;
        }
        else
        {
            // reset to zero percent filled
            barProgress = 0f;
            C.GetComponent<Image>().fillAmount = barProgress;
        }
	}
}
