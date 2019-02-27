using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSelector : MonoBehaviour {

	Quaternion ori;
	Vector3 fwd;
	RaycastHit hit;
	//public string name;
	public bool selecting;
	public GameObject progress_bar, barGO;
	private int value = 2;
	private Vector3 barScale;

	// Use this for initialization
	void Start () 
	{
		progress_bar = GameObject.Find ("ProgressBar");
	    barGO = GameObject.Find ("BarBackground");
		barGO.SetActive (false);
		barScale = progress_bar.transform.localScale;
	}

	IEnumerator SceneTransition(){
		int count = 0;
		barGO.SetActive (true);
		while (barScale.x <= 300) {
			barScale.x += value;
			progress_bar.transform.localScale = barScale;
			yield return new WaitForEndOfFrame ();
		}

		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (name);
		barScale.x = 0;
		progress_bar.transform.localScale = barScale;
		selecting = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			
			if (hit.transform.tag == "button") {
				Debug.Log (hit.collider.name);
				if (name != hit.collider.name) {
					StopCoroutine ("SceneTransition");
				}
				name = hit.collider.name;
				if (!selecting) {
					selecting = true;
					StartCoroutine ("SceneTransition");
				}
			} 
				
		}else {
			
			barScale.x = 0;
			progress_bar.transform.localScale = barScale;
			selecting = false;
			name = "nanashi";
			barGO.SetActive (false);
			StopCoroutine ("SceneTransition");
		}
			
	}
}
