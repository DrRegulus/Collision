using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public Texture[] mainBtns = new Texture[3];
	public Texture[] lvlBtns = new Texture[5];
	private bool levelMenu = false;
	private int mainIncs = 0;
	private int lvlIncs = 0;
	private int btnWidth;
	private int btnHeight;

	void Start()
	{
		PlayerPrefs.SetInt ("Lives", 3);
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;

		//Variables to scale menu buttons to window size
		mainIncs = Screen.width / (mainBtns.Length * 2);
		lvlIncs = Screen.width / (lvlBtns.Length * 2);
		btnWidth = Screen.width / 5;
		btnHeight = Screen.height / 5;

		if (levelMenu)
		{
			//Intro
			if(GUI.Button(new Rect(lvlIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight), lvlBtns[0])) {
				Application.LoadLevel("Comic0");
			}

			//Level1
			if(GUI.Button(new Rect(3 * lvlIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight), lvlBtns[1])) {
				Application.LoadLevel("Comic1");
			}

			//Level2
			if(GUI.Button(new Rect(5 * lvlIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight), lvlBtns[2])) {
				Application.LoadLevel("Level2");
			}

			//Level3
			if(GUI.Button(new Rect(7 * lvlIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight), lvlBtns[3])) {
				Application.LoadLevel("Level3");
			}

			//Previous
			if(GUI.Button(new Rect(9 * lvlIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight),  lvlBtns[4])) {
				levelMenu = false;
			}
		}
		else
		{
			//Level selection
			if(GUI.Button(new Rect(mainIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight),  mainBtns[0])) {
				levelMenu = true;
			}

			//Credits
			if(GUI.Button(new Rect(3 * mainIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight), mainBtns[1])) {
				Application.LoadLevel("Credits");
			}
			
			//Quit
			if(GUI.Button(new Rect(5 * mainIncs - btnWidth/2, Screen.height - btnHeight,
			                       btnWidth, btnHeight),  mainBtns[2])) {
				Application.Quit();
			}
		}
	}
}
