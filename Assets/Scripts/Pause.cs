using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	static float buttonHeight = 30;
	static float numButtons = 2;
	static float menuWidth = Screen.width / 2;
	static float menuHeight = buttonHeight / 2 + numButtons * (buttonHeight * 1.5f);
	static float buttonWidth = menuWidth - 20;
	static Rect menu;
	
	
	void OnGUI()
	{
		GUI.backgroundColor = Color.gray;
		menu = new Rect(Screen.width / 2 - menuWidth / 2, Screen.height / 2 - menuHeight / 2, menuWidth, menuHeight);
		
		GUI.Box(menu, "");
		GUI.BeginGroup(menu);
		
		if(GUI.Button(new Rect(10,  buttonHeight / 2, buttonWidth, buttonHeight), "Return to Menu"))
		{
			Application.LoadLevel(0);
		}
		
		if(GUI.Button (new Rect(10,  (buttonHeight + 2 * buttonHeight / 2), buttonWidth, buttonHeight), "Quit"))
		{
			Application.Quit ();
		}
		
		GUI.EndGroup();
	}
}
