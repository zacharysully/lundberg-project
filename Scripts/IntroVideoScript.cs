using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.PostProcessing;

public class IntroVideoScript : MonoBehaviour {

    VideoPlayer videoPlayerRef;
    VideoClip introVideo;

    FadeTransition fadeScriptRef;
    Scenemanager sceneMangRef;

    bool videoOver = false;

    private double waitTime;
    private double time = 0;

    public GameObject transformSelect;
    //public float timeTillBlackPlaneTurnsOff = 0.0f;
    //public GameObject blackPlane;

    private void Awake()
    {
        //get scenemanager script
        sceneMangRef = GameObject.Find("OcculusCameraHolder").GetComponent<Scenemanager>();
        //get fadescript ref
        fadeScriptRef = GameObject.Find("FadeGO").GetComponent<FadeTransition>();

        //turnoff post process
        GameObject.FindObjectOfType<CameraScript_2>().gameObject.GetComponent<PostProcessingBehaviour>().enabled = false;

        //turnoff transformSelect
        transformSelect = GameObject.Find("LookAtPrefab_TransformSelect");

        //get video
        videoPlayerRef = this.gameObject.GetComponent<VideoPlayer>();
        introVideo = videoPlayerRef.clip;

        //pause and st to first frame
        videoPlayerRef.Stop();

        waitTime = introVideo.length;

        time -= fadeScriptRef.fadeSpeed;

        

    }

    /*IEnumerator TurnOffBlackPlane()
    {
        yield return new WaitForSeconds(timeTillBlackPlaneTurnsOff);
        blackPlane.SetActive(false);
    }
    */

    private void Update()
    {
        if(time > 0 && time < waitTime && !videoPlayerRef.isPlaying)
        {
            //print("AAAAAAHHHH");
            
            videoPlayerRef.Play();

            //StartCoroutine(TurnOffBlackPlane());
        }


        //if video complete
        if(time > waitTime && !videoOver)
        {
            //print("change");

            videoPlayerRef.Stop();

            sceneMangRef.ChangeTransformTo("Map");

            videoOver = true;

            StartCoroutine(TurnPostOn());

        }

        time += Time.deltaTime;
    }

    private IEnumerator TurnPostOn()
    {
        yield return new WaitForSeconds(2f);
        //turn on TransformSelectPrefab
        transformSelect.SetActive(true);

        //turn on post processing
        //GameObject.FindObjectOfType<CameraScript_2>().gameObject.GetComponent<PostProcessingBehaviour>().enabled = true;
    }

}
