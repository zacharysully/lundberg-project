using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayText : MonoBehaviour {

    public Text description;

    public bool beingLookedAt;

    //When camera raycast hits the attached gameobjects collider
    //make display text visible
    //otherwise make invisible

    public float fadeSpeed = 1f;

    //solid color
    Color solidColor;
    //see through color
    Color transparentColor;

    //solid color
    Color solidColorParent;
    //see through color
    Color transparentColorParent;
    private Color currentColorParent;

    //for interpolation
    private Color currentColor;
    Color startColor, endColor;



    private float transitionTime;

    public bool makeTransparent;
    public bool makeSolid;

    public Image parentImage;

    void Start()
    {
        //IF YOU HAVE A TEXT UI GO WITH THE LABEL "<INSERT THIS GO NAME>" + "Text" THEN THIS SCRIPT WILL AUTOMATICALLY FIND IT 
        if (description == null && GameObject.Find(this.gameObject.name + "Text") != null)
        {
            description = GameObject.Find(this.gameObject.name + "Text").GetComponent<Text>();
        }
        else if (description == null)
        //if no descrtion return
        {
            return;
        }

        //see if text has image as parent
        //if so move it to same location as text
        if(description.gameObject.transform.parent.GetComponent<Image>() != null)
        {
            print("parent is image");
            parentImage = description.gameObject.transform.parent.GetComponent<Image>();

            //set up solid color and intital material color
            solidColorParent = parentImage.color;
            solidColorParent.a = 1;
            //set up transparent color
            transparentColorParent = parentImage.color;
            transparentColorParent.a = 0;

            //set start color
            parentImage.color = solidColorParent;
            currentColor = parentImage.color;

            //make image appear in front of this GO
            parentImage.transform.position = this.gameObject.transform.position;
            parentImage.transform.rotation = this.gameObject.transform.rotation;
        }
        else
        {
            //set up solid color and intital material color
            solidColor = description.color;
            solidColor.a = 1;
            //set up transparent color
            transparentColor = description.color;
            transparentColor.a = 0;

            //set start color
            description.color = solidColor;
            currentColor = description.color;

            //make text appear in front of this gameobject
            description.transform.position = this.gameObject.transform.position;
            description.transform.rotation = this.gameObject.transform.rotation;
            //description.transform.localScale = this.gameObject.transform.localScale;
        }



        transitionTime = 0;

        //set state
        makeTransparent = true;

        //chekc to see if parent is starepoint if so set text
        CheckIfStarePoint();

    }

    public void CheckIfStarePoint()
    {
        //see if parent starepoint
        if(this.gameObject.transform.parent != null && this.gameObject.transform.parent.GetComponent<StarePoint>() != null)
        {
            //if so set the text
            description.text = this.gameObject.transform.parent.GetComponent<StarePoint>().TransformScene.name;
        }
    }

    private void Update()
    {

        //Check to see if being looked at
        if(CameraTextDetectionScript.S.textBeingLookedAt == this)
        {
            beingLookedAt = true;
            //print(this.gameObject + " being looked at");
        }
        else
        {
            beingLookedAt = false;
        }

        //if camera array hitting this
        
        //being looked at
        //else not being looked at

        //if no text return
        if(description == null)
        {
            return;
        }

        //if image is parent
        if (parentImage != null)
        {
           

            //if being looked at make color 
            if (beingLookedAt && parentImage.color.a != 1 && endColor != solidColor)
            {
                FadeToSolidColor();
            }
            //if not make invisible
            else if (!beingLookedAt && parentImage.color.a != 0 && endColor != transparentColor)
            {
                FadeToTransparent();
            }

            //if color change completed end
            if (transitionTime > 1 && parentImage.color.a == endColor.a)
            {
                //print("transitionTime above 1");

                return;
            }

            // otherwise change color
            currentColor = Color.Lerp(startColor, endColor, transitionTime);
            parentImage.color = currentColor;
            transitionTime += Time.deltaTime * fadeSpeed;


            return;
        } else
        {
                //if being looked at make color 
                if (beingLookedAt && description.color.a != 1 && endColor != solidColor)
                {
                    FadeToSolidColor();
                }
                //if not make invisible
                else if (!beingLookedAt && description.color.a != 0 && endColor != transparentColor)
                {
                    FadeToTransparent();
                }
            //if color change completed end
            if (transitionTime > 1 && description.color.a == endColor.a)
            {
                //print("transitionTime above 1");

                return;
            }

            // otherwise change color
            currentColor = Color.Lerp(startColor, endColor, transitionTime);
            description.color = currentColor;
            transitionTime += Time.deltaTime * fadeSpeed;

            
        }





    }


    //method for fading to black
    public void FadeToSolidColor()
    {
        //print("FadeToSolidColorText");
        startColor = description.color;
        endColor = solidColor;
        transitionTime = 0;
    }

    //method for fading to transparent
    public void FadeToTransparent()
    {
        //print("FadeToTransparentText");
        startColor = description.color;
        endColor = transparentColor;
        transitionTime = 0;
    }


}
