using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StarePoint : MonoBehaviour {

    //holds a video
    //[SerializeField]
    //private VideoClip videoScene;

    //holds a transform
    [SerializeField]
    private GameObject transformScene;

    //rate at which this object checks to see if being stared at
    [SerializeField]
    private float refreshRate = .2f;

    //bool to determine whether or not being stared at
    [SerializeField]
    private bool beingLookedAt;

    [SerializeField]
    private float beingStaredAt;

    //refrence to scenmanager
    private Scenemanager scenemanager;

    public bool stareAtToQuitGame;


    //gets/sets
    public bool BeingLookedAt
    {
        get
        {
            return beingLookedAt;
        }

        set
        {
            beingLookedAt = value;
        }
    }

    /*public VideoClip VideoScene
    {
        get
        {
            return videoScene;
        }
        set
        {
            videoScene = value;
        }
    }*/

    public GameObject TransformScene
    {
        get
        {
            return transformScene;
        }
        set
        {
            transformScene = value;
        }
    }

    private void Start()
    {
        scenemanager = GameObject.Find("OcculusCameraHolder").GetComponent<Scenemanager>();
        StartCoroutine(CheckBeingStaredAt());
    }


    /*public void SpawnInSetUp()
    {
        //reset beingstaredat/beinglookedat
        beingLookedAt = false;
        beingStaredAt = 0;

        //restart checkups
        StartCoroutine(CheckBeingStaredAt());
    }
    */

    //
    IEnumerator CheckBeingStaredAt()
    {
        //print("refreshing");
        //after "refreshRate" seconds
        yield return new WaitForSeconds(refreshRate);
        //check if being stared at
        if(CameraScript_2.S.CurrentlyBeingstaredAt == this.gameObject)
        {
            beingLookedAt = true;
            beingStaredAt += refreshRate;
        }
        else
        {
            beingLookedAt = false;
            beingStaredAt = 0;
            //Camera can tell if it is transitioning
        }

        //if being stared for 3 seconds start transitioning to new scene
        if (beingStaredAt > 3f && beingStaredAt < (3f + .1f) && !scenemanager.transitioning)
        {
            if (stareAtToQuitGame)
            {
                scenemanager.QuitGame();
            }
            else
            {
                //start transition
                scenemanager.ChangeTransformTo(transformScene.name);
            }
            
        }

        //repeat
        StartCoroutine(CheckBeingStaredAt());

    }

    

}
