using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Scenemanager : MonoBehaviour {

    //videoplayer refrence
    //private VideoPlayer videoPlayerRef;
    //user transform refrence
    private Transform userTransformRef;

    //fade GO refrence
    public GameObject fadeGO;
    private FadeTransition fadeScriptRef;


    //array for all videoclips
    //public StarePoint[] videoPoints;
    //private Dictionary<string, VideoClip> videosDictionary;

    //array/dictionary for all transforms
    public StarePoint[] transformPoints;
    private Dictionary<string, GameObject> transformDictionary;

    //TEMP, just so you can see video has been switched out for another
    //public VideoClip currentvideo;

    //so youi can see the players current Transform
    public Transform currentTransform;

    //public bool changeVideo;
    public bool changeTransform;

    //public float waitForVideoToLoadTime = 1f;
    public float waitForChangeTransfromTime = 1f;

    //boolean that determines whether or not a transition is occurring
    public bool transitioning;

    public LookAtOrganizer lookAtOrganizer;

    public bool stopLooking;

    private void Awake()
    {
        //get refrences
        //videoPlayerRef = this.gameObject.GetComponent<VideoPlayer>();
        userTransformRef = this.gameObject.GetComponent<Transform>();

        //print(this.gameObject.transform);

        fadeScriptRef = fadeGO.GetComponent<FadeTransition>();

        //set to not transitioning 
        transitioning = false;

        //for testing
        //currentvideo = videoPlayerRef.clip;
        currentTransform = userTransformRef;

        stopLooking = false;
    }

    /*public void PutAllVideosIntoRefrenceDictionary()
    {
        //get all videoclip points in scene
        //videoPoints = GameObject.FindObjectsOfType<StarePoint>();
        transformPoints = GameObject.FindObjectsOfType<StarePoint>();

        //set up dictionary
        /*videosDictionary = new Dictionary<string, VideoClip>();
        foreach (StarePoint vidPtScript in videoPoints)
        {
            //get video
            VideoClip vid = vidPtScript.VideoScene;
            //put in videoDictionary
            //if video doesn't already exist there
            if (!videosDictionary.ContainsKey(vid.name))
            {
                videosDictionary.Add(vid.name, vid);
            }

        }
    }*/

    public void PutAllTransformPrefabsIntoRefrenceDictionary()
    {

        //get all transformPts points in scene
        transformPoints = GameObject.FindObjectsOfType<StarePoint>();

        transformDictionary = new Dictionary<string, GameObject>();
        //get all transform prefabs in the scenes starepts
        foreach (StarePoint starePtScript in transformPoints)
        {
            //get transform prefab gameobject
            GameObject tranPt = starePtScript.TransformScene;
            //put in transformDictionary
            //if video doesn't already exist there
            //print(tranPt.name + " added to dictionary");
            if (!transformDictionary.ContainsKey(tranPt.name))
            {
                transformDictionary.Add(tranPt.name, tranPt);
            }

        }

    }

    /*public void Update()
    {
        //OBSELETE
        if (changeTransform)
        {
            ChangeTransformTo("");
            changeTransform = false;
        }
    }*/

    /*
/// <summary>
/// method transitions to new video, given video name
/// </summary>
/// <param name="newVideoName"></param>
public void ChangeVideoTo(string newVideoName)
{
    print("ChangeVideoTo");
    if (transitioning)
    {
        return;
    }
    else
    {
        print("changing video");
        transitioning = true;

        //fade to black
        fadeScriptRef.FadeToSolidColor();

        VideoClip newVideo = videosDictionary[newVideoName];

        //if videoSelect


        //check to make sure not already in this video scene
        /*if(newVideo = videoPlayerRef.clip)
        {
            return;
        }
        //*


        print("Before WaitForFadeOut transitioning is " + transitioning);
        StartCoroutine(WaitForFadeOut(newVideo, newVideoName));
    }



}
*/



    /// <summary>
    /// method transitions to new transfrom, given transfrom GO's name
    /// </summary>
    /// <param name="newTransformName"></param>
    public void ChangeTransformTo(string newTransformName)
    {
        //print("ChangeTransformTo");
        //print(newTransformName);
        if (transitioning)
        {
            return;
        }
        else
        {
            //print("changing transform");
            transitioning = true;
            
            //fade to black
            fadeScriptRef.FadeToSolidColor();

            Transform newTransform = transformDictionary[newTransformName].transform;

            //print("Before WaitForFadeOut transitioning is " + transitioning);
            StartCoroutine(WaitForFadeOut(newTransform, newTransformName));
        }

        

    }

    /*
    IEnumerator WaitForFadeOut(VideoClip newVideo, string newVideoName)
    {
        print("WaitForFadeOut");
        yield return new WaitForSeconds(.2f);
        print("After wait transitioning is " + transitioning);
        if(fadeScriptRef.TransitionTime > 1)
        {
            print("transitionTime is greater than 1");
            //done fading out

            //change video in video player
            videoPlayerRef.clip = newVideo;

            //TEMP, just so you can see it works
            currentvideo = videoPlayerRef.clip;

            //start coroutine that waits and then fades in
            StartCoroutine(WaitForVideoToLoad());

            //change layout of starepoints
            //NEED TO NAME VIDEO SELECT VIDEO "VideoSelect"
            if (newVideoName == "VideoSelect")
            {
                lookAtOrganizer.Transition(true);
            }
            else
            {
                lookAtOrganizer.Transition(false);
            }
        }
        else
        {
            print("transitionTime is less than 1");
            if (transitioning)
            {
                //not done fading out
                StartCoroutine(WaitForFadeOut(newVideo, newVideoName));
            }
            
        }
        
    }
    */

    IEnumerator WaitForFadeOut(Transform newTransform, string newTransformName)
    {
        //print("WaitForFadeOut");

        //stop looking for transitions
        stopLooking = true;

        yield return new WaitForSeconds(.2f);
        //print("After wait transitioning is " + transitioning);
        if (fadeScriptRef.TransitionTime > 1)
        {
            //print("transitionTime is greater than 1");
            //done fading out

            //print("New spot is " + newTransform);

            //change player position
            userTransformRef = newTransform;
            this.gameObject.transform.position = userTransformRef.position; //apply new transform (currently only using position)
            

            //store current transform
            currentTransform = userTransformRef;

            //start coroutine that waits and then fades in
            StartCoroutine(WaitForTransformToLoad());

            //change layout of starepoints
            //NEED TO NAME TRANSFORM SELECT POSITION "Map"
            if (newTransformName == "Map")
            {
                lookAtOrganizer.Transition(true);
            }
            else
            {
                lookAtOrganizer.Transition(false);
            }
        }
        else
        {
            //print("transitionTime is less than 1");
            if (transitioning)
            {
                //not done fading out
                StartCoroutine(WaitForFadeOut(newTransform, newTransformName));
            }

        }

    }

    /*
    IEnumerator WaitForVideoToLoad()
    {
        print("WaitForVideoToLoad");
        yield return new WaitForSeconds(waitForVideoToLoadTime);

        //fade to transparent
        fadeScriptRef.FadeToTransparent();
        transitioning = false; 
    }
    */

    IEnumerator WaitForTransformToLoad()
    {
        //print("WaitForTransformToLoad");
        yield return new WaitForSeconds(waitForChangeTransfromTime);

        //fade to transparent
        fadeScriptRef.FadeToTransparent();
        transitioning = false;

        //stop any transitions or UI transition info until faded to transparent complete
        StartCoroutine(WaitForFadeIn());


    }

    IEnumerator WaitForFadeIn()
    {
        yield return new WaitForSeconds(1f);
        //start looking again
        stopLooking = false;
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }


}
