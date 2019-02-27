using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LookAtOrganizer : MonoBehaviour {

    //public GameObject LookAt_VideoSelect, LookAt_Left, LookAt_Right;
    public GameObject LookAt_TransformSelect;

    //public GameObject VideoSelectPoints;
    public GameObject stareAtSelectPointsGO;

    //list of all videos, with index 0 = videoselect
    //public List<VideoClip> videos;
    public List<GameObject> transforms;

    //public bool inVideoSelect;
    public bool inTransformSelect;

    private void Awake()
    {
        Transition(true);

        //On start all LookAtPrefabs in the VideoSelect Points are empty
        //Go through videos and assign videos to all the children of VideoSelectPoints
        //If there isn't a child make one?

        //FOR THIS TO WORK THE NUMBER OF LOOKATPREFABS HAS TO EQUAL THE NUMBER OF VIDEOS IN VIDEOS - 1



        /*StarePoint[] starePoints = VideoSelectPoints.GetComponentsInChildren<StarePoint>();
        for (int index = 0; index < starePoints.Length; index++)
        {
            starePoints[index].VideoScene = videos[index + 1];
        }

        //turn on video select so it can be added to the dictionary
        LookAt_VideoSelect.SetActive(true);
        //call function to add shit to dictionary
        this.gameObject.GetComponent<Scenemanager>().PutAllVideosIntoRefrenceDictionary();
        //turn on video select
        LookAt_VideoSelect.SetActive(false);
        */


        //FOR THIS TO WORK THE NUMBER OF LOOKATPREFABS HAS TO EQUAL THE NUMBER OF TRANSFORMS IN TRANSFORMS - 1

        StarePoint[] starePoints = stareAtSelectPointsGO.GetComponentsInChildren<StarePoint>();
        for (int index = 0; index < starePoints.Length; index++)
        {
            starePoints[index].TransformScene = transforms[index + 1];
        }

        //turn on video select so it can be added to the dictionary
        LookAt_TransformSelect.SetActive(true);
        //call function to add shit to dictionary
        this.gameObject.GetComponent<Scenemanager>().PutAllTransformPrefabsIntoRefrenceDictionary();
        //turn on video select
        LookAt_TransformSelect.SetActive(false);


    }

    /*public void Transition(bool inVidSelect)
    {
        print("transition");
        inVideoSelect = inVidSelect;

        if (inVideoSelect)
        {
            //make all startStarePoints appear
            VideoSelectPoints.SetActive(true);

            //reset all points
            foreach(StarePoint starePt in VideoSelectPoints.GetComponentsInChildren<StarePoint>())
            {
                starePt.SpawnInSetUp();
            }

            //make videoselect, left and right starePoints dissappear
            LookAt_VideoSelect.SetActive(false);
            LookAt_Left.SetActive(false);
            LookAt_Right.SetActive(false);
        }

        else
        {
            //make all startStarePoints disappear
            VideoSelectPoints.SetActive(false);
            //make videoselect, left and right starePoints appear
            LookAt_VideoSelect.SetActive(true);
            LookAt_VideoSelect.GetComponent<StarePoint>().SpawnInSetUp();
            LookAt_Left.SetActive(true);
            LookAt_Left.GetComponent<StarePoint>().SpawnInSetUp();
            LookAt_Right.SetActive(true);
            LookAt_Right.GetComponent<StarePoint>().SpawnInSetUp();

            //set the points videos according to what is needed
            //start position already set
            //before in videos array (left)
            int index = videos.IndexOf(this.gameObject.GetComponent<Scenemanager>().currentvideo);
            if (index > 1)
            index--;
            else if (index <= 1)
            {
                index = videos.Count - 1;
            }
            LookAt_Left.GetComponent<StarePoint>().VideoScene = videos[index];
            //after in videos array (right)
            index = videos.IndexOf(this.gameObject.GetComponent<Scenemanager>().currentvideo);
            if (index < videos.Count - 1)
                index++;
            else if (index == videos.Count - 1)
            {
                index = 1;
            }
            LookAt_Right.GetComponent<StarePoint>().VideoScene = videos[index];

        }
    }
    */

    public void Transition(bool inTranSelect)
    {
        //print("transition");
        inTransformSelect = inTranSelect;

        //move the _transformSelect point with user
        LookAt_TransformSelect.transform.position = this.gameObject.transform.position + new Vector3(0, -1.55f, 0);

        if (inTransformSelect)
        {
            /*
            //make all startStarePoints appear
            VideoSelectPoints.SetActive(true);

            //reset all points
            foreach (StarePoint starePt in VideoSelectPoints.GetComponentsInChildren<StarePoint>())
            {
                starePt.SpawnInSetUp();
            }

            //make videoselect, left and right starePoints dissappear
            LookAt_VideoSelect.SetActive(false);
            LookAt_Left.SetActive(false);
            LookAt_Right.SetActive(false);
            */

            //reset all points
            /*foreach (StarePoint starePt in stareAtSelectPointsGO.GetComponentsInChildren<StarePoint>())
            {
                starePt.SpawnInSetUp();
            }*/

        }

        else
        {
            /*
            //make all startStarePoints disappear
            VideoSelectPoints.SetActive(false);
            //make videoselect, left and right starePoints appear
            LookAt_VideoSelect.SetActive(true);
            LookAt_VideoSelect.GetComponent<StarePoint>().SpawnInSetUp();
            LookAt_Left.SetActive(true);
            LookAt_Left.GetComponent<StarePoint>().SpawnInSetUp();
            LookAt_Right.SetActive(true);
            LookAt_Right.GetComponent<StarePoint>().SpawnInSetUp();

            //set the points videos according to what is needed
            //start position already set
            //before in videos array (left)
            int index = videos.IndexOf(this.gameObject.GetComponent<Scenemanager>().currentvideo);
            if (index > 1)
                index--;
            else if (index <= 1)
            {
                index = videos.Count - 1;
            }
            LookAt_Left.GetComponent<StarePoint>().VideoScene = videos[index];
            //after in videos array (right)
            index = videos.IndexOf(this.gameObject.GetComponent<Scenemanager>().currentvideo);
            if (index < videos.Count - 1)
                index++;
            else if (index == videos.Count - 1)
            {
                index = 1;
            }
            LookAt_Right.GetComponent<StarePoint>().VideoScene = videos[index];
            */

        }
    }

}
