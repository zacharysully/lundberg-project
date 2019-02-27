
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraScript : MonoBehaviour {

    public static CameraScript S;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

	public Camera cam;
    public Ray ray;
    private RaycastHit hit;

    private Scenemanager sceneMangRef;

    //endpoint to create line renderer
    public Vector3 rayStartpoint;
    public Vector3 rayEndpoint;

    //gameObject camera is looking at
    private GameObject currentlyBeingstaredAt;

    public GameObject point1, point2, point3, point4;

    public GameObject CurrentlyBeingstaredAt
    {
        get
        {
            return currentlyBeingstaredAt;
        }
    }

    private void Awake()
    {
        //create singleton
        S = this;

        rayStartpoint = Vector3.zero;
        ray = new Ray(Vector3.zero, S.transform.rotation * Vector3.forward);

        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

       

        gyroEnabled = EnableGyro();

        //set up scenemanager ref
        if(this.gameObject.GetComponent<Scenemanager>() != null)
        {
            sceneMangRef = this.gameObject.GetComponent<Scenemanager>();
        }
        else
        {
            sceneMangRef = this.gameObject.AddComponent<Scenemanager>();
        }

    }

	private void Start()
	{
		cam = GetComponent<Camera>();
	}

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }

       // Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		//ray.direction = transform.rotation;
		Debug.DrawRay(ray.origin, ray.direction, Color.green);
		ray.direction = transform.localRotation * Vector3.forward;

		//cam.fieldOfView = Camera.main.fieldOfView * XRSettings.eyeTextureWidth / XRSettings.eyeTextureHeight;
		//ray = cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));

        //linerenderer starting/endpoints
        //rayStartpoint = ray.GetPoint(0f);
       // rayEndpoint = ray.GetPoint(10f);

        point1.transform.position = ray.GetPoint(0f);
        point2.transform.position = ray.GetPoint(2.5f);
        point3.transform.position = ray.GetPoint(5f);
        point4.transform.position = ray.GetPoint(10f);

        if (Physics.Raycast (ray, out hit)) {
			print ("true");
			//if camera ray hitting a "stareAt" GO
			if (hit.collider.gameObject.GetComponent<StarePoint> () != null) {
				//set currently being stared at to that GO
				currentlyBeingstaredAt = hit.collider.gameObject;
				//sceneMangRef.transitioning = true;
				return;
			}
            

        } else {
			print ("false");
		}
        //if not looking at stuff set it to null
        currentlyBeingstaredAt = null;
        //sceneMangRef.transitioning = false;
    }
}
