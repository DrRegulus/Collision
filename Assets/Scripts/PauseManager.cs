﻿using UnityEngine;
using System.Collections;


public class PauseManager : MonoBehaviour {

	public Texture resumeBtn;
	public Texture quitBtn;

	private bool pause = false;
	private float buttonHeight = 30;
	private float numButtons = 3;
	private float menuWidth = Screen.width / 3;
	private float menuHeight;
	private float buttonWidth;

	private Rect menu;
	private Rect backGray;

	void Start()
	{
		menuHeight = buttonHeight / 2 + numButtons * (buttonHeight * 1.5f);
		buttonWidth = menuWidth - 20;
	}

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

			/*
			if (GUI.Button (new Rect (10, (buttonHeight + buttonHeight / 2), buttonWidth, buttonHeight),resumeBtn)) {
				pauseFunction ();
			}
			
			if (GUI.Button (new Rect (10, (buttonHeight + 4 * buttonHeight / 2), buttonWidth, buttonHeight), quitBtn)) {
				Application.LoadLevel("mainMenu");
			}*/

			if (GUI.Button (new Rect (10, (buttonHeight + buttonHeight / 2), buttonWidth, buttonHeight), "Resume")) {
				pauseFunction ();
			}
			
			if (GUI.Button (new Rect (10, (buttonHeight + 4 * buttonHeight / 2), buttonWidth, buttonHeight), "Quit")) {
				Time.timeScale = 1;
				Application.LoadLevel("MainMenu");
			}

			GUI.EndGroup ();

		}
	}
}
