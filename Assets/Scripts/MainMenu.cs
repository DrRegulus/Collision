using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI()
	{
		GUI.Box (new Rect (30, 30, 100, 100), "");
		GUI.BeginGroup (new Rect (30, 30, 100, 100));

		if(GUI.Button(new Rect(10,10,80,20), "Level 1")) {
			Application.LoadLevel("level1");
		}

		if(GUI.Button(new Rect(10,40,80,20), "Level 2")) {
			Application.LoadLevel("level2");
		}

		if(GUI.Button(new Rect(10,70,80,20), "Quit")) {
			Application.Quit ();
		}

		GUI.EndGroup ();
	}
}
