using UnityEngine;
using System.Collections;


public class pauseManager : MonoBehaviour {
	public GUIText pauseText;
	public bool pause = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("p") && pause){
			pause = false;
			Time.timeScale = 1;
			pauseText.enabled = false;

		}
		else if(Input.GetKeyDown("p") && !pause){
			pause = true;
			Time.timeScale = 0;
			pauseText.enabled = true;

		}
	}
}
