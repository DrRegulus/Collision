using UnityEngine;
using System.Collections;

public class LevelSplash : MonoBehaviour {

	public Texture nextBtn;
	public Texture quitBtn;
	public GUIStyle panelStyle = new GUIStyle();
	
	void Start()
	{
		panelStyle.alignment = TextAnchor.MiddleCenter;
		panelStyle.stretchHeight = true;
		panelStyle.stretchWidth = true;
	}
	
	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;

		if(nextBtn != null)
		{
			if(GUI.Button(new Rect(Screen.width - nextBtn.width, Screen.height/2 - nextBtn.height/2,
			                       nextBtn.width, nextBtn.height), nextBtn))
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}

		if(GUI.Button(new Rect(Screen.width - quitBtn.width, 3*Screen.height/4 - quitBtn.height/2,
		                       quitBtn.width, quitBtn.height), quitBtn))
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
