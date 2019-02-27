using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour {

    /// <summary>
    /// multiplied by time passed in a frame to determine how quickly fade in/out transition occurs
    /// </summary>
    [Tooltip("Smaller number equals slower transition")]
    public float fadeSpeed = 2f;

    //material
    public Material fadeMaterial;
    private Material currentMaterial;

    //solid color
    Color solidColor;
    //see through color
    Color transparent;

    Color startColor, endColor;

    private float transitionTime;

    public bool makeTransparent;
    public bool makeSolid;

    public float TransitionTime
    {
        get
        {
            return transitionTime;
        }
    }


    void Awake()
    {
        
        //set up solid color and intital material color
        solidColor = fadeMaterial.color;
        solidColor.a = 1;
        fadeMaterial.color = solidColor;

        //set up transparent color
        transparent = fadeMaterial.color;
        transparent.a = 0;
        

        fadeMaterial.color = solidColor;
        currentMaterial = fadeMaterial;

        transitionTime = 0;

        //set state
        makeTransparent = true;

    }

    void Update()
    {

        //ISSUE: THERE IS ANOTHER INSTANCE OF THE STAREPOINT SCRIPT TELLING THE SCENEMANAGER THAT YOU AREN'T TRANSITIONING
        //check to stop transition if user looks away too soon
        //if not transitioning and not transitioning to tranparent and transition time is not greater than 1
        if ((!(CameraScript_2.S.gameObject.transform.parent.gameObject.GetComponent<Scenemanager>().transitioning)) && endColor != transparent && transitionTime < 1f)
        {
            print("Not transitioning or transition time is less than 1 or endColor isn't transparent");
            print("Transitioning is " + CameraScript_2.S.gameObject.GetComponent<Scenemanager>().transitioning);
            print("endColor is " + endColor);
            print("transition time is " + transitionTime);

            makeTransparent = true;
        }


        //temp
        if (makeSolid)
        {
            FadeToSolidColor();
            //stop telling program to start transition to solid color
            makeSolid = false;
        }
        else if (makeTransparent)
        {
            FadeToTransparent();
            //stop telling program to start transition to transparent color
            makeTransparent = false;
        }
        //

        if(transitionTime > 1 && currentMaterial.color.a == endColor.a)
        {
            return;
        }
        currentMaterial.color = Color.Lerp(startColor, endColor, transitionTime);
        fadeMaterial = currentMaterial;
        transitionTime += Time.deltaTime * fadeSpeed;

    }


    //method for fading to black
    public void FadeToSolidColor()
    {
        //print("FadeToSolidColor");
        startColor = fadeMaterial.color;
        endColor = solidColor;
        transitionTime = 0;
    }

    //method for fading to transparent
    public void FadeToTransparent()
    {
        //print("FadeToTransparent");
        startColor = fadeMaterial.color;
        endColor = transparent;
        transitionTime = 0;
    }

}
