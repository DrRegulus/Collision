using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public Texture[] mainBtns = new Texture[3];
	public Texture[] lvlBtns = new Texture[5];
	private bool levelMenu = false;
	private int mainIncs = 0;
	private int lvlIncs = 0;

	void Start()
	{
		mainIncs = Screen.width / (mainBtns.Length * 2);
		lvlIncs = Screen.width / (lvlBtns.Length * 2);
		PlayerPrefs.SetInt ("Lives", 3);
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;

		if (levelMenu)
		{
			if(GUI.Button(new Rect(lvlIncs - lvlBtns[0].width/2, Screen.height - lvlBtns[0].height,
			                       lvlBtns[0].width, lvlBtns[0].height), lvlBtns[0])) {
				Application.LoadLevel("Comic0");
			}
			
			if(GUI.Button(new Rect(3 * lvlIncs - lvlBtns[1].width/2, Screen.height - lvlBtns[1].height,
			                       lvlBtns[1].width, lvlBtns[1].height), lvlBtns[1])) {
				Application.LoadLevel("Comic1");
			}
			
			if(GUI.Button(new Rect(5 * lvlIncs - lvlBtns[2].width/2, Screen.height - lvlBtns[2].height,
			                       lvlBtns[2].width, lvlBtns[2].height), lvlBtns[2])) {
				Application.LoadLevel("Level2");
			}

			/*if(GUI.Button(new Rect(7 * lvlIncs - lvlBtns[3].width/2, Screen.height - lvlBtns[3].height,
			                       lvlBtns[3].width, lvlBtns[3].height), lvlBtns[3])) {
				//Application.LoadLevel("Comic1");
			}*/

			if(GUI.Button(new Rect(9 * lvlIncs - lvlBtns[4].width/2, Screen.height - lvlBtns[4].height,
			                       lvlBtns[4].width, lvlBtns[4].height), lvlBtns[4])) {
				levelMenu = false;
			}
		}
		else
		{
			if(GUI.Button(new Rect(mainIncs - mainBtns[0].width/2, Screen.height - mainBtns[0].height,
			                       mainBtns[0].width, mainBtns[0].height), mainBtns[0])) {
				levelMenu = true;
			}
			
			if(GUI.Button(new Rect(3 * mainIncs - mainBtns[1].width/2, Screen.height - mainBtns[1].height,
			                       mainBtns[1].width, mainBtns[1].height), mainBtns[1])) {
				Application.LoadLevel("Credits");
			}
			
			
			if(GUI.Button(new Rect(5 * mainIncs - mainBtns[2].width/2, Screen.height - mainBtns[2].height,
			                       mainBtns[2].width, mainBtns[2].height), mainBtns[2])) {
				Application.Quit();
			}
		}
	}
}
