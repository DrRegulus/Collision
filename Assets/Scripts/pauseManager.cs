using UnityEngine;
using System.Collections;


public class pauseManager : MonoBehaviour {
	public GUIText pauseText;
	public bool pause = false;
	static float buttonHeight = 30;
	static float numButtons = 2;
	static float menuWidth = Screen.width / 2;
	static float menuHeight = buttonHeight / 2 + numButtons * (buttonHeight * 1.5f);
	static float buttonWidth = menuWidth - 20;
	static Rect menu;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && pause){
			pauseFunction();
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && !pause){
			pause = true;
			Time.timeScale = 0;
			pauseText.enabled = true;

		}
	}
	void pauseFunction(){
				pause = false;
				Time.timeScale = 1;
				pauseText.enabled = false;
		}

	void OnGUI()
	{
				if (pause) {
						GUI.backgroundColor = Color.gray;
						menu = new Rect (Screen.width / 2 - menuWidth / 2, Screen.height / 2 - menuHeight / 2, menuWidth, menuHeight);
		
						GUI.Box (menu, "");
						GUI.BeginGroup (menu);
		
						if (GUI.Button (new Rect (10, buttonHeight / 2, buttonWidth, buttonHeight), "Resume")) {
								pauseFunction ();
						}
		
						if (GUI.Button (new Rect (10, (buttonHeight + 2 * buttonHeight / 2), buttonWidth, buttonHeight), "Quit")) {
								Application.LoadLevel("mainManu");
						}
		
						GUI.EndGroup ();
				}
		}
}
