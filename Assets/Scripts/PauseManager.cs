﻿using UnityEngine;
using System.Collections;


public class PauseManager : MonoBehaviour {
	public Texture resumeBtn;
	public Texture quitBtn;
	private bool pause = false;
	static float buttonHeight = 30;
	static float numButtons = 3;
	static float menuWidth = Screen.width / 3;
	static float menuHeight = buttonHeight / 2 + numButtons * (buttonHeight * 1.5f);
	static float buttonWidth = menuWidth - 20;
	static Rect menu;
	static Rect backGray;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && pause){
			pauseFunction();
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && !pause){
			pause = true;
			Time.timeScale = 0;
		}
	}
	
	void pauseFunction(){
		pause = false;
		Time.timeScale = 1;
	}
	
	void OnGUI()
	{
		if (pause) {
			GUI.backgroundColor = Color.gray;
			menu = new Rect (Screen.width / 2 - menuWidth / 2, Screen.height / 2 - menuHeight / 2, menuWidth, menuHeight);
			backGray = new Rect(0, 0, Screen.width, Screen.height);
			GUI.Box (backGray, "");
			GUI.BeginGroup (backGray);
			GUI.EndGroup();
			GUI.Box (menu, "Collision Paused");
			GUI.BeginGroup (menu);
			
			if (GUI.Button (new Rect (10, (buttonHeight + buttonHeight / 2), buttonWidth, buttonHeight),resumeBtn)) {
				pauseFunction ();
			}
			
			if (GUI.Button (new Rect (10, (buttonHeight + 4 * buttonHeight / 2), buttonWidth, buttonHeight), quitBtn)) {
				Application.LoadLevel("mainMenu");
			}
			
			GUI.EndGroup ();

		}
	}
}